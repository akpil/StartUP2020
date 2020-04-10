using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Text mScoreText, mClearText;
    private Rigidbody mRB;
    [SerializeField]
    private float mSpeed = 5;
    [SerializeField]
    private int mScore;
    // Start is called before the first frame update
    void Start()
    {
        mScore = 0;
        mRB = GetComponent<Rigidbody>();
        mScoreText.text = "Score: " + mScore.ToString();
        mClearText.text = "";
    }

    public void AddScore()
    {
        mScore++;
        mScoreText.text = "Score: " + mScore.ToString();
        if (mScore >= 4)
        {
            mClearText.text = "Game Clear!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 axis = new Vector3(horizontal, 0, vertical);
        axis = axis.normalized * 5;
        axis.y = mRB.velocity.y;

        //mRB.AddForce(axis);
        mRB.velocity = axis;
        
    }
}