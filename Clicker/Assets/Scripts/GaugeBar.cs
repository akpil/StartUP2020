using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI mText;
    [SerializeField]
    private Image mGauge;
    public void ShowGaugeBar(float progress, string data)
    {
        mGauge.fillAmount = progress;
        mText.text = data;
    }
}
