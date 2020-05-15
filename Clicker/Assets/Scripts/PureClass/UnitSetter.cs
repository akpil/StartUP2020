using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitSetter
{
    private static readonly string[] UnitArr = {"", "K", "M", "B", "T", "aa", "ab", "ac"};
    public static string GetUnitStr(double value)
    {
        string baseStr = value.ToString("N0");
        string[] splitedArr = baseStr.Split(',');
        if(splitedArr.Length > 1)
        {
            char[] subSplitedArr = splitedArr[1].ToCharArray();
            return string.Format("{0}.{1}{2} {3}", 
                           splitedArr[0],
                           subSplitedArr[0],subSplitedArr[1],
                           UnitArr[splitedArr.Length - 1] );
        }
        else
        {
            return string.Format("{0} {1}", splitedArr[0], UnitArr[splitedArr.Length -  1]);
        }
    }
}
