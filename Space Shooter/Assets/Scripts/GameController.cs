using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int mScore;
    private bool mbGameOver;
    [SerializeField]
    private Player mPlayer;
    [SerializeField]
    private UIController mUIController;
    [Header("EnemySpawn")]
    [SerializeField]
    private AsteroidPool mAstPool;
    [SerializeField]
    private EnemyPool mEnemyPool;
    [SerializeField]
    private float mSpawnXMin, mSpawnXMax, mSpawnZ;
    [SerializeField]
    private float mSpawnRate;
    private float mCurrentSpawnRate;
    private Coroutine mHazardRoutine;
    // Start is called before the first frame update
    void Start()
    {
        mUIController.ShowScore(mScore);
        mUIController.ShowMessagetext("");
        mUIController.ShowRestart(false);
        mHazardRoutine = StartCoroutine(SpawnHazard());
    }
    
    public void AddScore(int amount)
    {
        mScore += amount;
        mUIController.ShowScore(mScore);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        //mbGameOver = false;
        //mScore = 0;
        //mPlayer.transform.position = Vector3.zero;
        //mPlayer.gameObject.SetActive(true);
        //if (mHazardRoutine == null)
        //{
        //    mHazardRoutine = StartCoroutine(SpawnHazard());
        //}
        //mUIController.ShowScore(mScore);
        //mUIController.ShowMessagetext("");
        //mUIController.ShowRestart(false);
    }

    public void PlayerDie()
    {
        StopCoroutine(mHazardRoutine);
        mHazardRoutine = null;
        mbGameOver = true;
        //최종 스코어 표시(게임오버 표시 밑에)

        mUIController.ShowMessagetext("Game Over");
        mUIController.ShowRestart(true);
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
        if(mbGameOver && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }
}
