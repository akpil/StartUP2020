using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCoworkerState { Idle, Move }

public class Coworker : MonoBehaviour
{
    private static readonly int AnimWalkHash = Animator.StringToHash("IsWalk");
    private Animator mAnim;
    private Rigidbody2D mRB2D;
    private eCoworkerState mState;

#pragma warning disable 0649
    [SerializeField]
    private Transform mTextEffectPos;
#pragma warning restore 0649
    [SerializeField]
    private int mID;
    [SerializeField]
    private float mWorkPeriod;
    [SerializeField]
    private float mCurrentTime;

    // Start is called before the first frame update
    void Start()
    {
        mRB2D = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
        mState = eCoworkerState.Idle;
        StartCoroutine(CoworkerMove());
    }

    public void StartWork(int id, float period)
    {
        mID = id;
        mWorkPeriod = period;
        mCurrentTime = 0;
    }

    private void Update()
    {
        if(mWorkPeriod > 0)
        {
            mCurrentTime += Time.deltaTime;
            if(mCurrentTime >= mWorkPeriod)
            {
                CoworkerController.Instance.JobFinish(mID, mTextEffectPos.position);
                mCurrentTime = 0;
            }
        }
    }

    private IEnumerator CoworkerMove()
    {
        WaitForSeconds oneSec = new WaitForSeconds(1);
        while(true)
        {
            yield return oneSec;
            mState = (eCoworkerState)Random.Range(0, 2);
            if (Random.Range(0, 2) == 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            switch(mState)
            {
                case eCoworkerState.Idle:
                    mRB2D.velocity = Vector2.zero;
                    mAnim.SetBool(AnimWalkHash, false);
                    break;
                case eCoworkerState.Move:
                    mRB2D.velocity = transform.right * -1;
                    mAnim.SetBool(AnimWalkHash, true);
                    break;
                default:
                    Debug.LogError("wrong move state " + mState);
                    break;
            }
        }
    }
}
