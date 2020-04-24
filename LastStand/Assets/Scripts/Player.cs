﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator mAnim;
    [SerializeField]
    private float mJumpForce;
    [SerializeField]
    private AttackArea mAttackArea;
    [SerializeField]
    private float mAtk;
    private float mMaxHP;
    private float mCurrentHP;

    // Start is called before the first frame update
    void Start()
    {
        mAnim = GetComponent<Animator>();
        mAttackArea.SetDamage(mAtk);
    }

    public void Hit(float damage)
    {
        Debug.Log("Hit " + damage);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.identity;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            mAnim.SetBool(AnimHash.Attack, true);
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            mAnim.SetBool(AnimHash.Attack, false);
        }
    }
}