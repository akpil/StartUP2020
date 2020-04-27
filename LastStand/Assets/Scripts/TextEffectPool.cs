using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffectPool : ObjectPool<TextEffect>
{
    [SerializeField]
    private Transform mCanvas;
    protected override TextEffect CreateNewObj(int id)
    {
        TextEffect newEffect = Instantiate(mOriginArr[id], mCanvas);
        mPools[id].Add(newEffect);
        return newEffect;
    }
}
