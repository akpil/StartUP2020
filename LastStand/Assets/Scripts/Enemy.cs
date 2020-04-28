using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEnemyState
{
    Idle,
    Walk,
    Attack,
    Die
}

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform mHPBarPos;
    private Animator mAnim;
    private Rigidbody2D mRB2D;
    [SerializeField]
    private float mSpeed;
    [SerializeField]
    private int mAtk;
    [SerializeField]
    private float mMaxHP;
    private float mCurrentHP;
    private eEnemyState mState;
    private int mDelayCount;

    [SerializeField]
    private float mReward;

    private Player mTarget;

    private IngameController mController;
    private void Awake()
    {
        mAnim = GetComponent<Animator>();
        mRB2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        mAnim.SetBool(AnimHash.Dead, false);
        mCurrentHP = mMaxHP;
        mState = eEnemyState.Idle;
        mDelayCount = 0;
    }

    public void SetIngameController(IngameController controller)
    {
        mController = controller;
    }

    public void StartMoving()
    {
        StartCoroutine(StateMachine());
    }

    public void Hit(float amount)
    {
        mCurrentHP -= amount;
        //show HPBar
        if(mCurrentHP <= 0)
        {
            mState = eEnemyState.Die;
            mDelayCount = 0;
            mController.AddCoin(mReward);
            TextEffect textEffect = mController.GetTextEffect();
            textEffect.ShowText(mReward);
            //textEffect.transform.position = mHPBarPos.position;
            textEffect.transform.position = Camera.main.WorldToScreenPoint(mHPBarPos.position);
        }
    }

    public void Attack()
    {
        mTarget.Hit(mAtk);
    }
    public void AttackFinish()
    {
        mAnim.SetBool(AnimHash.Attack, false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(mTarget == null)
            {
                mTarget = collision.gameObject.GetComponent<Player>();
            }
            mState = eEnemyState.Attack;
            mDelayCount = 0;
            mAnim.SetBool(AnimHash.Walk, false);
            //collision.gameObject.SendMessage("Hit", 1, SendMessageOptions.DontRequireReceiver);
        }
    }

    private IEnumerator StateMachine()
    {
        WaitForSeconds pointOne = new WaitForSeconds(.1f);
        while (true)
        {
            switch(mState)
            {
                case eEnemyState.Idle:
                    if (mDelayCount >= 20)
                    {
                        mState = eEnemyState.Walk;
                        mAnim.SetBool(AnimHash.Walk, true);
                        mRB2D.velocity = transform.right * mSpeed;
                        mDelayCount = 0;
                    }
                    else
                    {
                        mDelayCount++;
                    }
                    break;
                case eEnemyState.Walk:
                    if (mDelayCount >= 10)
                    {
                        mState = eEnemyState.Idle;
                        mAnim.SetBool(AnimHash.Walk, false);
                        mRB2D.velocity = Vector2.zero;
                        mDelayCount = 0;
                    }
                    else
                    {
                        mDelayCount++;
                    }
                    break;
                case eEnemyState.Attack:
                    if(mDelayCount >= 30)
                    {
                        mAnim.SetBool(AnimHash.Attack, true);
                        mDelayCount = 0;
                    }
                    else
                    {
                        mDelayCount++;
                    }
                    break;
                case eEnemyState.Die:
                    if(mDelayCount == 0)
                    {
                        mAnim.SetBool(AnimHash.Dead, true);
                        mDelayCount++;
                        //포인트 획득 이펙트
                    }
                    else if(mDelayCount >= 10)
                    {
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        mDelayCount++;
                    }
                    break;
                default:
                    Debug.LogError("wrong state: " + mState);
                    break;
            }
            yield return pointOne;
        }
    }
}
