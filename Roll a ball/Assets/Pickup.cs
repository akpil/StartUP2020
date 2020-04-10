using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private Vector3 mTumble;
    private Vector3 mRealTumble;
    private Player mPlayer;

    private void Start()
    {
        mRealTumble = mTumble * Time.fixedDeltaTime;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        mPlayer = playerObj.GetComponent<Player>();
        //mPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        transform.Rotate(mRealTumble);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    transform.Rotate(mTumble * Time.deltaTime);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mPlayer.AddScore();
            gameObject.SetActive(false);
        }
        
    }
}
