using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
    [SerializeField]
    private Image mGauge;
    [SerializeField]
    private Text mText;

    public void SetGauge(double current, double max)
    {
        float fillAmount = (float)(current / max);
        mGauge.fillAmount = fillAmount;
        mText.text = string.Format("{0}/{1}", current.ToString(), max.ToString());
    }
}
