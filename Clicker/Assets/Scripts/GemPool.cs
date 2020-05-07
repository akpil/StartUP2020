using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPool : ObjectPool<Gem>
{
    private void Awake()
    {
        mOriginArr = Resources.LoadAll<Gem>(Paths.GEM_PREFAB);
        PoolSetup();
    }
}
