﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameController : MonoBehaviour
{
    [SerializeField]
    private EnemyPool mEnemyPool;
    [SerializeField]
    private Transform mLeftPos, mRightPos;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        WaitForSeconds three = new WaitForSeconds(3);
        while(true)
        {
            yield return three;
            bool isLeft = Random.value < .5f;
            bool isMale = Random.value < .5f;
            Transform spawnPos;
            if (isLeft)
            {
                spawnPos = mLeftPos;
            }
            else
            {
                spawnPos = mRightPos;
            }
            Enemy enemy;
            if(isMale)
            {
                enemy = mEnemyPool.GetFromPool(0);
            }
            else
            {
                enemy = mEnemyPool.GetFromPool(1);
            }
            enemy.transform.position = spawnPos.position;
            enemy.transform.rotation = spawnPos.rotation;
            enemy.StartMoving();
        }
    }
}