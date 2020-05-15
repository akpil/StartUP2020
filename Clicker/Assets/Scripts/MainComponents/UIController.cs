using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    private static readonly int Movehash = Animator.StringToHash("Move");
    [SerializeField]
    private GaugeBar mGaugeBar;

    [SerializeField]
    private Animator[] mWindowAnimArr;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowWindow(int id)
    {
        mWindowAnimArr[id].SetTrigger(Movehash);
    }

    public void ShowGaugeBar(double current, double max)
    {
        string progressStr = string.Format("{0} / {1}",
                                            UnitSetter.GetUnitStr(current),
                                            UnitSetter.GetUnitStr(max));
        float progress = (float)(current / max);
        mGaugeBar.ShowGaugeBar(progress, progressStr);
    }

    
}
