using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    private Rigidbody mRB;
    [SerializeField]
    private float mTorque, mSpeed;

    private void Awake()
    {
        mRB = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        mRB.angularVelocity = Random.insideUnitSphere * mTorque;
        mRB.velocity = Vector3.back * mSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //mRB.angularVelocity = Random.onUnitSphere;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bolt") ||
            other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            //Add score
            //Add effect
            //Add sound
            other.gameObject.SetActive(false);
        }
    }
}
