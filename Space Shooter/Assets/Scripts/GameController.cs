using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private AsteroidPool mAstPool;
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
    

    private IEnumerator SpawnHazard()
    {
        while(true)
        {
            for (int i = 0; i < 5; i++)
            {
                AsteroidMovement ast = mAstPool.GetFromPool(Random.Range(0, 3));
                ast.transform.position = new Vector3(Random.Range(mSpawnXMin, mSpawnXMax),
                                                     0,
                                                     mSpawnZ);
                yield return new WaitForSeconds(0.3f);
            }
            yield return new WaitForSeconds(mSpawnRate);
        }
    }


    private void Update()
    {
        new WaitForSeconds(5);
    }
}
