using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private TextMeshProUGUI mText;
    [SerializeField]
    private Image mIcon;
#pragma warning restore 0649

    public void SetText(string text)
    {
        mText.text = text;
    }

    public void SetIcon(Sprite sprite)
    {
        mIcon.sprite = sprite;
    }
}
