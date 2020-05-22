using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : ObjectPool<Timer>
{
    private void Awake()
    {
        PoolSetup();
    }
}
