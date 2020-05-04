using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElement : MonoBehaviour
{
    private int mID;
    [SerializeField]
    private Image mIconImage;
    [SerializeField]
    private Text mTitleText, mContentsText, mLevelText, mCostText;
    [SerializeField]
    private Button mPurchaseButton;

    public void Init(Sprite icon, int id, string name, string contents, int level, double cost, Delegates.IntInVoidReturn callback)
    {
        mIconImage.sprite = icon;
        mID = id;
        mTitleText.text = name;
        mContentsText.text = contents;
        mLevelText.text = level.ToString();
        mCostText.text = cost.ToString();
        mPurchaseButton.onClick.AddListener(() => { callback(mID); });
    }
}
