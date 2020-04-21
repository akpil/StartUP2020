using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int mMaxLife, mCurrentLife;
    private EffectPool mEffectPool;
    private GameController mGameController;
    private SoundController mSoundController;
    private UIController mUIController;

    [Header("Fire bolt")]
    [SerializeField]
    private BoltPool mBoltPool;
    [SerializeField]
    private Transform mBoltPos;
    [SerializeField]
    private float mFireLate;
    private float mCurrentFireLate;
    private int mBoltIndex;
    [SerializeField]
    private int mMaxBoltCount;
    [SerializeField]
    private float mBoltXGap;
    [SerializeField]
    private int mCurrentBoltCount;
    private Coroutine mBoltChangeRoutine;

    private Rigidbody mRB;
    [Header("Movement")]
    [SerializeField]
    private float mSpeed;
    [SerializeField]
    private float mXMax, mXMin, mZMax, mZMin;
    [SerializeField]
    private float mTilted = 30;
    
    // Start is called before the first frame update
    void Start()
    {
        mEffectPool = GameObject.FindGameObjectWithTag("EffectPool").GetComponent<EffectPool>();
        mSoundController = GameObject.FindGameObjectWithTag("SoundController").
                                     GetComponent<SoundController>();
        mRB = GetComponent<Rigidbody>();   
    }

    public void Init(GameController gameController, UIController uiController)
    {
        mGameController = gameController;
        mUIController = uiController;
        mCurrentFireLate = mFireLate;
        mCurrentBoltCount = 1;
        mUIController.ShowLife(mCurrentLife);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction = direction.normalized;

        mRB.velocity = direction * mSpeed;
        //transform.Translate(direction * Time.deltaTime);

        mRB.rotation = Quaternion.Euler(0, 0, horizontal * - mTilted);

        mRB.position = new Vector3(Mathf.Clamp(mRB.position.x, mXMin, mXMax), 
                                   0,
                                   Mathf.Clamp(mRB.position.z, mZMin, mZMax));

        if(Input.GetButton("Fire1") && mCurrentFireLate >= mFireLate)
        {
            float currentXStart = - mBoltXGap * ((mCurrentBoltCount - 1) / 2);
            Vector3 XPos = new Vector3(currentXStart, 0, 0);

            for(int i = 0; i < mCurrentBoltCount; i++)
            {
                Bolt bolt = mBoltPool.GetFromPool(mBoltIndex);
                bolt.gameObject.transform.position = mBoltPos.position + XPos;
                bolt.gameObject.transform.rotation = mBoltPos.rotation;
                bolt.ReSetDir();
                XPos.x += mBoltXGap;
            }
            
            mCurrentFireLate = 0;
            mSoundController.PlayEffectSound((int)eSFXType.FirePlayer);
        }
        else
        {
            mCurrentFireLate += Time.deltaTime;
        }
    }
    
    public void StartHoming(float time)
    {
        if(mBoltChangeRoutine != null)
        {
            StopCoroutine(mBoltChangeRoutine);
        }
        mBoltChangeRoutine = StartCoroutine(ChangeBoltID(1, time));
    }

    private IEnumerator ChangeBoltID(int id, float gap)
    {
        mBoltIndex += id;
        yield return new WaitForSeconds(gap);
        mBoltIndex -= id;
        mBoltChangeRoutine = null;
    }

    public void AddBoltCount()
    {
        if(mCurrentBoltCount < mMaxBoltCount)
        {
            mCurrentBoltCount++;
        }
    }

    public void AddLife()
    {
        if (mCurrentLife < mMaxLife)
        {
            mCurrentLife++;
            mUIController.ShowLife(mCurrentLife);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy") || 
           other.gameObject.CompareTag("EnemyBolt"))
        {
            mCurrentLife--;
            mUIController.ShowLife(mCurrentLife);
            if (mCurrentLife <= 0)
            {
                gameObject.SetActive(false);
                mGameController.PlayerDie();
                Timer effect = mEffectPool.GetFromPool((int)eEffectType.ExpPlayer);
                effect.transform.position = transform.position;
                mSoundController.PlayEffectSound((int)eSFXType.ExpPlayer);
            }
        }
    }
}