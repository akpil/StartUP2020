using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    protected float mTime;
    private void OnEnable()
    {
        StartCoroutine(Timing());
    }
    protected IEnumerator Timing()
    {
        yield return new WaitForSeconds(mTime);
        gameObject.SetActive(false);
    }
}
