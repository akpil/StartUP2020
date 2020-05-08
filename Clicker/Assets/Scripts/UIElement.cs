using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIElement : MonoBehaviour
{
    [SerializeField]
    private Image mIconImage;
    [SerializeField]
    private TextMeshProUGUI mTitleText, mLevelText, mContentsText, mCostText;
    [SerializeField]
    private Button mButton, mTenUpButton;

    private int mID;
    public void Init(int id,
                     Sprite icon, 
                     string title, 
                     string level,
                     string contents,
                     string cost,
                     Delegates.TwoIntInVoidCallback callback)
    {
        mID = id;
        mIconImage.sprite = icon;
        mTitleText.text = title;
        Refresh(level, contents, cost);

        mButton.onClick.AddListener(() => { callback(mID, 1); });
        mTenUpButton.onClick.AddListener(() => { callback(mID, 10); });
    }

    public void Refresh(string level, string contents, string cost)
    {
        mLevelText.text = level;
        mContentsText.text = contents;
        mCostText.text = cost;
    }
}
