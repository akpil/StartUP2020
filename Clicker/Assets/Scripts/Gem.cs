using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private SpriteRenderer mRenderer;
    [SerializeField]
    private Sprite[] mSprites;
#pragma warning restore 0649

    private float mShiftGap;
    private int mCurrentImageIndex;

    // Start is called before the first frame update
    void Awake()
    {
        mShiftGap = 1f / mSprites.Length;
    }

    private void OnEnable()
    {
        mCurrentImageIndex = 0;
        mRenderer.sprite = mSprites[0];
    }

    public void SetProgress(float progress)
    {
        int index = (int)(progress / mShiftGap);
        if(index < mSprites.Length)
        {
            mRenderer.sprite = mSprites[index];
            if (mCurrentImageIndex != index)
            {
                mCurrentImageIndex = index;
                //TODO image shift effect
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
