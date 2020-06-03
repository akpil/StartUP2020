using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Move : MonoBehaviour
{
    [SerializeField]
    private Transform mDest;
    [SerializeField]
    private Transform[] mWaypointArr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.DOMove(mDest.position, 1).
                OnPlay(() => {
                                    Debug.Log("Start!");
                             }).
                SetEase(Ease.Linear).
                OnComplete(()=> {
                                    Debug.Log("Finish!");
                                });
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            Vector3 origin = transform.position;
            List<Vector3> path = new List<Vector3>();
            foreach (Transform t in mWaypointArr)
            {
                path.Add(t.position);
            }
            path.Add(origin);
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOPath(path.ToArray(), 4, PathType.CatmullRom)).
                SetEase(Ease.OutBounce).
                Join(transform.DOScale(Vector3.one * 2, 2)).
                AppendCallback(() =>
                {
                    Debug.Log("Scale finish!");
                })
                .AppendInterval(2)
                .Append(transform.DOScale(Vector3.one, 2));
        }
    }
}
