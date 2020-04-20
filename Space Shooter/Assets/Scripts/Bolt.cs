using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    protected Rigidbody mRB;
    [SerializeField]
    protected float mSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        mRB = GetComponent<Rigidbody>();
        ReSetDir();
    }

    public void ReSetDir()
    {
        mRB.velocity = transform.forward * mSpeed;
    }
}
