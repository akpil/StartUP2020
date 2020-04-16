using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float mTime;
    private void OnEnable()
    {
        StartCoroutine(Time());
    }

    private IEnumerator Time()
    {
        yield return new WaitForSeconds(mTime);
        gameObject.SetActive(false);
    }
}
