using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVLoader : MonoBehaviour
{
    private void Start()
    {
        LoadCSV("CsvFiles/StoryPage");
    }

    public void LoadCSV(string path)
    {
        const string WINDOWS_DELIMITER = "\n";
        const string UNIX_DELIMITER = "\r\n";
        const string MAC_DELIMITER = "\r";

        TextAsset asset = Resources.Load<TextAsset>(path);
        if(asset == null)
        {
            Debug.LogError("wrong path " + path);
            return;
        }
        string data = asset.text;
        Debug.Log(data);

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

        for (int i = 1; i < lineData.Length - 1; i++)
        {
            Debug.Log("----------------line---------------");
            string currentLine = lineData[i];
            string[] currentLineSplited;
            char[] MarkArr = { '\"', '[', ']', '{', '}' };
            //{ "", "[, ]", "{, }" }
            

            Debug.Log(currentLine);
            
            if(currentLine.IndexOfAny(MarkArr) >= 0)
            {
                currentLine = currentLine.Replace("\"\"", "\"");
                currentLine = currentLine.Replace("\"\"", "\"");
                currentLine = currentLine.Replace("\"[", "[");
                currentLine = currentLine.Replace("]\"", "]");
                currentLine = currentLine.Replace("\"{", "{");
                currentLine = currentLine.Replace("}\"", "}");

                List<string> result = new List<string>();

                int startIndex = 0;
                int commaIndex = currentLine.IndexOf(',', startIndex);
                int markIndex = currentLine.IndexOfAny(MarkArr, startIndex);
                bool isQuot, isCurly, isSquare;

                Debug.Log(currentLine[startIndex] + "/" + startIndex);
                Debug.Log(currentLine[commaIndex] + "/" + commaIndex);
                Debug.Log(currentLine[markIndex] + "/" + markIndex);
                Debug.Log(currentLine.Substring(startIndex, commaIndex));
                while(true)
                {

                    // 구분시켜야 할 문자열 덩어리 판단
                    if (commaIndex < markIndex) 
                    {
                        string block = currentLine.Substring(startIndex, commaIndex - startIndex);
                        Debug.Log(block);
                        result.Add(block);
                    }
                    else
                    {
                        startIndex = markIndex;

                        isQuot = currentLine[markIndex].Equals(MarkArr[0]);
                        isCurly = currentLine[markIndex].Equals(MarkArr[3]);
                        isSquare = currentLine[markIndex].Equals(MarkArr[1]);

                        if(isSquare)
                        {
                            markIndex = currentLine.IndexOf(MarkArr[2], startIndex);
                        }
                        else if(isCurly)
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

                        string block = currentLine.Substring(startIndex, markIndex - startIndex);
                        Debug.Log(block);
                        result.Add(block);
                    }

                    startIndex = commaIndex + 1;
                    commaIndex = currentLine.IndexOf(',', startIndex);
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
        }
    }
}
