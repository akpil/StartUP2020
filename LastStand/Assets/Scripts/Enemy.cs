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
    private Animator mAnim;
    private Rigidbody2D mRB2D;
    [SerializeField]
    private float mSpeed;
    [SerializeField]
    private int mAtk;
    [SerializeField]
    private int mMaxHP;
    private int mCurrentHP;
    private eEnemyState mState;
    private int mDelayCount;

    private Player mTarget;

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
        
    }

    public void StartMoving()
    {
        StartCoroutine(StateMachine());
    }

    public void Attack()
    {
        mTarget.Hit(1);
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
                    break;
                default:
                    Debug.LogError("wrong state: " + mState);
                    break;
            }
            yield return pointOne;
        }
    }
}
