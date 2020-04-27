using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUIController : MonoBehaviour
{
    [SerializeField]
    private Text mCoinText;
    public float Coin
    {
        set
        {
            mCoinText.text = value.ToString();
        }
    }
    public void ShowCoin(float value)
    {

        mCoinText.text = value.ToString();
    }
}
