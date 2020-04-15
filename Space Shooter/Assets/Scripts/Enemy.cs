using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody mRB;
    [SerializeField]
    private float mSpeed, mHoriSpeed;
    [SerializeField]
    private Transform mBoltPos;
    [SerializeField]
    private BoltPool mBoltPool;
    [SerializeField]
    private float mFireRate;

    private void Awake()
    {
        mRB = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        mRB.velocity = Vector3.back * mSpeed;
        StartCoroutine(Movement());
        //InvokeRepeating("Fire", mFireRate, mFireRate);
        StartCoroutine(AutoFire());
        Random.Range(0, 100);
    }


    private IEnumerator AutoFire()
    {
        WaitForSeconds fireRate = new WaitForSeconds(mFireRate);
        while(true)
        {
            yield return fireRate;
            Bolt bolt = mBoltPool.GetFromPool();
            bolt.transform.position = mBoltPos.position;
            bolt.transform.rotation = mBoltPos.rotation;
        }
    }

    private void Fire()
    {
        Bolt bolt = mBoltPool.GetFromPool();
        bolt.transform.position = mBoltPos.position;
        bolt.transform.rotation = mBoltPos.rotation;
    }

    private IEnumerator Movement()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(.5f, 1));
            float direction;
            if (transform.position.x < 0)
            {
                //오른쪽
                direction = Random.Range(2f, 3f);
                mRB.velocity += Vector3.right * direction;
            }
            else
            {
                //왼쪽
                direction = Random.Range(-3f, -2f);
                mRB.velocity += Vector3.right * direction;
            }
            yield return new WaitForSeconds(Random.Range(.5f, 1));
            //직진
            mRB.velocity -= Vector3.right * direction;
        }
    }
}
