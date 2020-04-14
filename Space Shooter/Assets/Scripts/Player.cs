using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Fire bolt")]
    [SerializeField]
    private BoltPool mBoltPool;
    [SerializeField]
    private Transform mBoltPos;
    [SerializeField]
    private float mFireLate;
    private float mCurrentFireLate;

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
        mRB = GetComponent<Rigidbody>();
        mCurrentFireLate = mFireLate;
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
            Bolt bolt = mBoltPool.GetFromPool();
            bolt.gameObject.transform.position = mBoltPos.position;
            mCurrentFireLate = 0;
        }
        else
        {
            mCurrentFireLate += Time.deltaTime;
        }
    }
}