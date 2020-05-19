using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcterToUnicode : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private string mOrigin;
#pragma warning restore 0649
    // Start is called before the first frame update
    void Start()
    {
        char[] charArr = mOrigin.ToCharArray();
        for (int i = 0; i < charArr.Length; i++)
        {
            int charToInt = (int)charArr[i];
            Debug.LogFormat("{0}: {1} // {2}", charArr[i], charToInt, charToInt.ToString("X4"));
        }
    }
}