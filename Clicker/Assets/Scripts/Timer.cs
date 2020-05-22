using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    float mTime;
#pragma warning restore 0649

    private void OnEnable()
    {
        StartCoroutine(TimeOut());
    }
    private IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(mTime);
        gameObject.SetActive(false);
    }
}
