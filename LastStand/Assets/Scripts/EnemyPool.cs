using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool<Enemy>
{
    [SerializeField]
    IngameController mIngameController;

    private void Awake()
    {
        mOriginArr = Resources.LoadAll<Enemy>("Prefab");
        PoolSetup();
    }

    protected override Enemy CreateNewObj(int id)
    {
        Enemy newEnemy = Instantiate(mOriginArr[id]);
        newEnemy.SetIngameController(mIngameController);
        mPools[id].Add(newEnemy);
        return newEnemy;
    }
}
