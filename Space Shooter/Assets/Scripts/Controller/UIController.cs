using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text mScoreText, mMessageText, mRestartText;

    [SerializeField]
    private GameObject[] mLifeObjArr;

    public void ShowLife(int life)
    {
        for (int i = 0; i < mLifeObjArr.Length; i++)
        {
            if(i < life)
            {
                mLifeObjArr[i].SetActive(true);
            }
            else
            {
                mLifeObjArr[i].SetActive(false);
            }
        }
    }

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
