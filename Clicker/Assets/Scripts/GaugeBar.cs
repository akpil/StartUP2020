using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private TextMeshProUGUI mText;
    [SerializeField]
    private Image mGauge;
#pragma warning restore 0649

    public void ShowGaugeBar(float progress, string data)
    {
        mGauge.fillAmount = progress;
        mText.text = data;
    }
}
