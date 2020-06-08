﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CSVLoader : MonoBehaviour
{
    [SerializeField]
    StoryText[] InfoArr;
    private void Start()
    {
        LoadCSV(out InfoArr, "CsvFiles/StoryPage");
        //Dictionary<int, int> dic = new Dictionary<int, int>();
        //for (int i = 0; i < InfoArr.Length; i++)
        //{
        //    dic[InfoArr[i].ID] = i;
        //}
        //int[] keyArr = dic.Keys.ToArray();
        //for(int i = 0; i < keyArr.Length; i++)
        //{
        //    Debug.LogFormat("key {0} // value{1}", keyArr[i], dic[keyArr[i]]);
        //}

        //Item[] itemArr = new Item[10];
        //Dictionary<eRankType, List<int>> rankDic = new Dictionary<eRankType, List<int>>();
        //for(int i = 0; i < itemArr.Length; i++)
        //{
        //    if(!rankDic.ContainsKey(itemArr[i].RankType))
        //    {
        //        rankDic[itemArr[i].RankType] = new List<int>();
        //    }
        //    rankDic[itemArr[i].RankType].Add(i);
        //}
        //List<int> list = rankDic[eRankType.Epic];
        //int pickedID = list[UnityEngine.Random.Range(0, list.Count)];
        //int pickedID2 = rankDic[eRankType.Epic][UnityEngine.Random.Range(0, rankDic[eRankType.Epic].Count)];
    }

    public void LoadCSV<T>(out T[] OutputArr, string path) where T : new()
    {
        TextAsset asset = Resources.Load<TextAsset>(path);
        if (asset == null)
        {
            Debug.LogError("wrong path " + path);
            OutputArr = null;
            return;
        }
        string data = asset.text;
        Debug.Log(data);

        #region Split Line by Line
        const string WINDOWS_DELIMITER = "\n";
        const string UNIX_DELIMITER = "\r\n";
        const string MAC_DELIMITER = "\r";

        if (data.Contains(UNIX_DELIMITER))
        {
            data = data.Replace(UNIX_DELIMITER, WINDOWS_DELIMITER);
        }
        else if (data.Contains(MAC_DELIMITER))
        {
            data = data.Replace(MAC_DELIMITER, WINDOWS_DELIMITER);
        }

        string[] lineData = data.Split('\n');
        for(int i = 0; i < lineData.Length;i++)
        {
            Debug.Log(lineData[i]);
        }
        #endregion

        OutputArr = new T[lineData.Length - 2];
        string[] fieldNameArr = lineData[0].Split(',');

        for (int i = 0; i < lineData.Length - 2; i++)
        {
            lineData[i + 1] = lineData[i + 1].Replace("\"\"", "\"");
            lineData[i + 1] = lineData[i + 1].Replace("\"[", "[");
            lineData[i + 1] = lineData[i + 1].Replace("]\"", "]");
            lineData[i + 1] = lineData[i + 1].Replace("\"{", "{");
            lineData[i + 1] = lineData[i + 1].Replace("}\"", "}");
            string[] currentLineSplited = GenerateLineSplit(lineData[i + 1]);

            OutputArr[i] = new T();
            Type type = typeof(T);
            System.Reflection.FieldInfo[] fields = type.GetFields();
            int fieldID = 0;

            for(int j = 0;  j < fields.Length; j++)
            {
                Type propertyType = fields[j].FieldType;

                if (fieldNameArr.Length > j && fields[j].Name == fieldNameArr[j])
                {
                    fieldID = j;
                }
                else
                {
                    bool skipFlag = true;
                    for (int k = 0; k < fieldNameArr.Length; k++)
                    {
                        if (fields[j].Name == fieldNameArr[k])
                        {
                            fieldID = k;
                            skipFlag = false;
                            break;
                        }
                    }
                    if (skipFlag)
                    {
                        continue;
                    }
                }

                if(propertyType.IsArray)
                {
                    string[] strList = GenerateLineSplit(currentLineSplited[fieldID]);
                    Type arrayFieldType = propertyType.GetElementType();

                    Array newArray = Array.CreateInstance(arrayFieldType, strList.Length);

                    if (strList != null)
                    {
                        newArray = Array.CreateInstance(arrayFieldType, strList.Length);

                        for (int k = 0; k < newArray.Length; ++k)
                        {
                            if (arrayFieldType.IsGenericType &&
                                arrayFieldType.GetGenericTypeDefinition() == typeof(List<>))
                            {
                                string[] listArr = GenerateLineSplit(strList[k]);
                                Type baseType = typeof(List<>);
                                Type itemType = arrayFieldType.GetGenericArguments()[0];
                                IList list = (IList)Activator.
                                    CreateInstance(baseType.MakeGenericType(arrayFieldType.GetGenericArguments()[0]));

                                for (int l = 0; l < listArr.Length; l++)
                                {
                                    if (itemType.IsEnum)
                                    {
                                        list.Insert(l, Enum.Parse(itemType, listArr[l]));
                                    }
                                    else if (itemType == typeof(int))
                                    {
                                        list.Insert(l, int.Parse(listArr[l]));
                                    }
                                    else if (itemType == typeof(float))
                                    {
                                        list.Insert(l, float.Parse(listArr[l]));
                                    }
                                    else if (itemType == typeof(bool))
                                    {
                                        list.Insert(l, bool.Parse(listArr[l]));
                                    }
                                    else if (itemType == typeof(double))
                                    {
                                        list.Insert(l, double.Parse(listArr[l]));
                                    }
                                    else if (itemType == typeof(string))
                                    {
                                        list.Insert(l, listArr[l].Replace("\\n", "\n"));
                                    }
                                    else
                                    {
                                        list.Insert(l, null);
                                    }
                                }
                                newArray.SetValue(list, k);
                            }
                            else if (arrayFieldType.IsEnum)
                            {
                                newArray.SetValue(Enum.Parse(arrayFieldType, strList[k]), k);
                            }
                            else if (arrayFieldType == typeof(int))
                            {
                                newArray.SetValue(int.Parse(strList[k]), k);
                            }
                            else if (arrayFieldType == typeof(float))
                            {
                                newArray.SetValue(float.Parse(strList[k]), k);
                            }
                            else if (arrayFieldType == typeof(bool))
                            {
                                newArray.SetValue(bool.Parse(strList[k]), k);
                            }
                            else if (arrayFieldType == typeof(double))
                            {
                                newArray.SetValue(double.Parse(strList[k]), k);
                            }
                            else if (arrayFieldType == typeof(string))
                            {
                                newArray.SetValue(strList[k].Replace("\\n", "\n"), k);
                            }
                            else
                            {
                                newArray.SetValue(null, k);
                            }
                        }
                    }
                    fields[j].SetValue(OutputArr[i], newArray);
                }
                else if(propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    string[] strList = GenerateLineSplit(currentLineSplited[fieldID]);
                    Type baseType = typeof(List<>);
                    Type itemType = propertyType.GetGenericArguments()[0];
                    
                    IList list = (IList)Activator.
                        CreateInstance(baseType.MakeGenericType(propertyType.GetGenericArguments()[0]));

                    for (int k = 0; k < strList.Length; k++)
                    {
                        if (itemType.IsEnum)
                        {
                            list.Insert(k, Enum.Parse(itemType, strList[k]));
                        }
                        else if (itemType == typeof(int))
                        {
                            list.Insert(k, int.Parse(strList[k]));
                        }
                        else if (itemType == typeof(float))
                        {
                            list.Insert(k, float.Parse(strList[k]));
                        }
                        else if (itemType == typeof(bool))
                        {
                            list.Insert(k, bool.Parse(strList[k]));
                        }
                        else if (itemType == typeof(double))
                        {
                            list.Insert(k, double.Parse(strList[k]));
                        }
                        else if (itemType == typeof(string))
                        {
                            list.Insert(k, strList[k].Replace("\\n", "\n"));
                        }
                        else
                        {
                            list.Insert(k, null);
                        }
                    }
                    fields[j].SetValue(OutputArr[i], list);
                }
                else if (propertyType.IsEnum)
                {
                    fields[j].SetValue(OutputArr[i], Enum.Parse(propertyType, currentLineSplited[fieldID]));
                }
                else if (propertyType == typeof(int) ||
                         propertyType == typeof(float) ||
                         propertyType == typeof(bool) ||
                         propertyType == typeof(double))
                {
                    Debug.LogFormat("{0}//{1}//{2}", fields[j].Name, currentLineSplited[fieldID], propertyType);
                    fields[j].SetValue(OutputArr[i], Convert.ChangeType(currentLineSplited[fieldID], propertyType));
                }
                else
                {
                    if (currentLineSplited[fieldID].Equals("NONE"))
                    {
                        currentLineSplited[fieldID] = string.Empty;
                    }

                    fields[j].SetValue(OutputArr[i], currentLineSplited[fieldID].Replace("\\n", "\n"));
                }
            }
        }
    }

    private string[] GenerateLineSplit(string currentLine)
    {
        Debug.Log("----------------line---------------");
        string[] currentLineSplited;
        char[] MarkArr = { '\"', '[', ']', '{', '}' };
        //{ "", "[, ]", "{, }" }

        if (currentLine.IndexOfAny(MarkArr) >= 0)
        {
            
            Debug.Log(currentLine);
            List<string> result = new List<string>();

            int startIndex = 0;
            int commaIndex = currentLine.IndexOf(',', startIndex);
            int markIndex = currentLine.IndexOfAny(MarkArr, startIndex);
            bool isQuot, isCurly, isSquare;

            while (true)
            {
                Debug.LogFormat("start {0}", startIndex);
                Debug.LogFormat("comma {0}", commaIndex);
                Debug.LogFormat("mark {0}", markIndex);

                // 구분시켜야 할 문자열 덩어리 판단
                if (commaIndex < markIndex && startIndex < commaIndex)
                {
                    string block = currentLine.Substring(startIndex, commaIndex - startIndex);
                    Debug.Log(block);
                    result.Add(block);
                    startIndex = commaIndex + 1;
                }
                else
                {
                    startIndex = markIndex;

                    if (markIndex < 0)
                    {
                        break;
                    }

                    isQuot = currentLine[markIndex].Equals(MarkArr[0]);
                    isCurly = currentLine[markIndex].Equals(MarkArr[3]);
                    isSquare = currentLine[markIndex].Equals(MarkArr[1]);

                    if (isSquare)
                    {
                        markIndex = currentLine.IndexOf(MarkArr[2], startIndex);
                    }
                    else if (isCurly)
                    {
                        markIndex = currentLine.IndexOf(MarkArr[4], startIndex);
                    }
                    else
                    {
                        markIndex = currentLine.IndexOf(MarkArr[0], startIndex);
                        while (markIndex < currentLine.Length - 1 &&
                               !currentLine[markIndex + 1].Equals(','))
                        {
                            markIndex = currentLine.IndexOf(MarkArr[0], markIndex + 1);
                        }
                    }

                    string block = currentLine.Substring(startIndex, markIndex + 1 - startIndex);
                    block = block.Remove(block.Length - 1, 1);
                    block = block.Remove(0, 1);
                    Debug.Log(block);
                    result.Add(block);
                    startIndex = markIndex + 1;

                    markIndex = currentLine.IndexOfAny(MarkArr, startIndex);
                }

                commaIndex = currentLine.IndexOf(',', startIndex);
                if (startIndex == commaIndex && startIndex < currentLine.Length)
                {
                    startIndex++;
                    commaIndex = currentLine.IndexOf(',', startIndex);
                }
                // 1. commaIndex < markIndex
                // 2. !(commaIndex < markIndex)

                // result에 구분된 문자열 덩어리 추가
                // 작업 종료 판별
            }
            currentLineSplited = result.ToArray();
        }
        else
        {
            currentLineSplited = currentLine.Split(',');
        }

        Debug.Log("=====================================");
        return currentLineSplited;
    }
}
