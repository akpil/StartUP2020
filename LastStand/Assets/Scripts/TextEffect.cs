using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : Timer
{
    [SerializeField]
    private Text mText;
    public void ShowText(float value)
    {
        mText.text = value.ToString();
    }
}
