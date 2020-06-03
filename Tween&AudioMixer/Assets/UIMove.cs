using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class UIMove : MonoBehaviour
{
    [SerializeField]
    private Transform[] mMovingObjArr;
    [SerializeField]
    private Transform mDest;
    [SerializeField]
    private float mInterval = .5f;
    [SerializeField]
    private Text mText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void StartMove()
    {
        for (int i = 0; i < mMovingObjArr.Length; i++)
        {
            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(i * mInterval).
                Append(mMovingObjArr[i].DOMove(mDest.position, 2)).
                Join(mMovingObjArr[i].DOScale(Vector3.one * 2, 1)).
                AppendCallback(mMovingObjArr[i].SetAsLastSibling).
                Append(mMovingObjArr[i].DOScale(Vector3.one, 1));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartMove();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            string data = "hahahdododhfqwerewrwqerqwerqwerwqer\nqwerqwer\nqrweqr";
            mText.text = "123123123123123213123213213123123123213123213213123213213";
            mText.DOText(data, 2, scrambleMode: ScrambleMode.None).SetEase(Ease.Linear);
        }
    }
}
