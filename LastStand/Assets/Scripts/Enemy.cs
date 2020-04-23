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
        StartCoroutine(StateMachine());
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
