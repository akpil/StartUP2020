using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffectPool : ObjectPool<TextEffect>
{
    public static TextEffectPool Instance;
#pragma warning disable 0649
    [SerializeField]
    private Transform mCanvas;
#pragma warning restore 0649
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            PoolSetup();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override TextEffect CreateNewObj(int id)
    {
        TextEffect newObj = Instantiate(mOriginArr[id], mCanvas);
        mPools[id].Add(newObj);
        return newObj;
    }

}
