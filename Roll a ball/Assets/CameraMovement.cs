using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject mPlayerObj;
    private Vector3 mOffset;
    // Start is called before the first frame update
    void Start()
    {
        mPlayerObj = GameObject.FindGameObjectWithTag("Player");
        mOffset = transform.position - mPlayerObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mPlayerObj.transform.position + mOffset;
    }
}
