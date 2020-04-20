using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text mScoreText, mMessageText, mRestartText;
    
    public void ShowScore(int amout)
    {
        mScoreText.text = "Score: " + amout.ToString();
    }

    public void ShowMessagetext(string data)
    {
        mMessageText.text = data;
    }

    public void ShowRestart(bool isActive)
    {
        mRestartText.gameObject.SetActive(isActive);
    }
}
