using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIElement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Image mIconImage;
    [SerializeField]
    private TextMeshProUGUI mTitleText, mLevelText, mContentsText, mCostText, mTenCostText;
    [SerializeField]
    private Button mButton, mTenUpButton;
#pragma warning restore 0649
    private Coroutine mButtonPopRoutine;
    private int mClickCount;

    private int mID;
    public void Init(int id,
                     Sprite icon, 
                     string title, 
                     string level,
                     string contents,
                     string cost,
                     string tenCost,
                     Delegates.TwoIntInVoidCallback callback)
    {
        mID = id;
        mIconImage.sprite = icon;
        mTitleText.text = title;
        Refresh(level, contents, cost, tenCost);

        mButton.onClick.AddListener(() => 
        {
            if(mButtonPopRoutine == null)
            {
                mClickCount = 0;
                mButtonPopRoutine = StartCoroutine(ButtonPop());
            }
            mClickCount++;
            callback(mID, 1);
        });
        mTenUpButton.onClick.AddListener(() => 
        {
            mClickCount++;
            callback(mID, 10);
        });
    }

    public void Refresh(string level, string contents, string cost, string tenCost)
    {
        mLevelText.text = level;
        mContentsText.text = contents;
        mCostText.text = cost;
        mTenCostText.text = tenCost;
    }

    public void SetButtonActive(bool isActive)
    {
        mButton.interactable = isActive;
    }

    public void SetTenButtonActive(bool isActive)
    {
        mTenUpButton.interactable = isActive;
    }

    private IEnumerator ButtonPop()
    {
        WaitForSeconds pointOne = new WaitForSeconds(.1f);
        float time = 3;
        bool activeButton = false;
        while(time > 0)
        {
            yield return pointOne;
            time -= .1f;
            if(mClickCount == 3)
            {
                activeButton = true && mTenUpButton.interactable;
                mTenUpButton.gameObject.SetActive(activeButton);
                break;
            }
        }

        if(activeButton)
        {
            mClickCount = 0;
            time = 3;
            while (time > 0)
            {
                if (mClickCount > 0)
                {
                    time = 3;
                    mClickCount = 0;
                }
                yield return pointOne;
                time -= .1f;
            }
        }

        mTenUpButton.gameObject.SetActive(false);
        mButtonPopRoutine = null;
    }
}
