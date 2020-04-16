using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int mScore;
    [SerializeField]
    private AsteroidPool mAstPool;
    [SerializeField]
    private EnemyPool mEnemyPool;
    [SerializeField]
    private float mSpawnXMin, mSpawnXMax, mSpawnZ;
    [SerializeField]
    private float mSpawnRate;
    private float mCurrentSpawnRate;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnHazard());
    }
    
    public void AddScore(int amount)
    {
        mScore += amount;
        //UI
    }

    private IEnumerator SpawnHazard()
    {
        WaitForSeconds pointThree = new WaitForSeconds(0.3f);
        WaitForSeconds spawnRate = new WaitForSeconds(mSpawnRate);
        int enemyCount = 3;
        int astCount = 10;
        int currentAstCount;
        int currentEnemyCount;
        float ratio = 1f / 3;
        while (true)
        {
            currentAstCount = astCount;
            currentEnemyCount = enemyCount;
            while (currentAstCount > 0 && currentEnemyCount > 0)
            {
                float rand = Random.Range(0, 1f);
                if (rand < ratio)
                {
                    Enemy enemy = mEnemyPool.GetFromPool();
                    enemy.transform.position = new Vector3(Random.Range(mSpawnXMin, mSpawnXMax),
                                                         0,
                                                         mSpawnZ);
                    currentEnemyCount--;
                }
                else
                {
                    AsteroidMovement ast = mAstPool.GetFromPool(Random.Range(0, 3));
                    ast.transform.position = new Vector3(Random.Range(mSpawnXMin, mSpawnXMax),
                                                         0,
                                                         mSpawnZ);
                    currentAstCount--;
                }
                yield return pointThree;
            }
            for (int i = 0; i < currentAstCount; i++)
            {
                AsteroidMovement ast = mAstPool.GetFromPool(Random.Range(0, 3));
                ast.transform.position = new Vector3(Random.Range(mSpawnXMin, mSpawnXMax),
                                                     0,
                                                     mSpawnZ);
                yield return pointThree;
            }
            for(int i = 0; i < currentEnemyCount; i++)
            {
                Enemy enemy = mEnemyPool.GetFromPool();
                enemy.transform.position = new Vector3(Random.Range(mSpawnXMin, mSpawnXMax),
                                                     0,
                                                     mSpawnZ);
                yield return pointThree;
            }
            yield return spawnRate;
        }
    }


    private void Update()
    {
        new WaitForSeconds(5);
    }
}
