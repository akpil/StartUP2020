using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUIController : MonoBehaviour
{
    [SerializeField]
    private Text mCoinText;
    [SerializeField]
    private Button mButton;
    private void Awake()
    {
        mButton.onClick.AddListener(ShowLog);
    }

    public float Coin
    {
        set
        {
            mCoinText.text = value.ToString();
        }
    }

    public void ShowLog()
    {
        Debug.Log("Button clicked");
    }

    public void ShowLogString(string value)
    {
        Debug.Log(value);
    }
    
    public void ShowLogTransform(Transform t)
    {

    }

    public void ShowCoin(float value)
    {

        mCoinText.text = value.ToString();
    }
}
