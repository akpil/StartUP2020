using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGTextureScroll : MonoBehaviour
{
    private Material mMat;
    [SerializeField]
    private float mSpeed;
    private float mOffsetPerFrame;
    private Vector2 mOffset;
    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        mMat = renderer.material;
        mOffsetPerFrame = mSpeed * Time.fixedDeltaTime;
        mOffset = Vector2.zero;
    }

    private void FixedUpdate()
    {
        //Vector2 offset = mMat.GetTextureOffset("_MainTex");
        //offset.y += mOffsetPerFrame;
        mOffset.y += mOffsetPerFrame;
        mMat.SetTextureOffset("_MainTex", mOffset);
    }
}
