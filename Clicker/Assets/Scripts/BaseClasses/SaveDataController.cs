using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveDataController : MonoBehaviour
{
    [SerializeField]
    protected SaveData mUser;

    protected void LoadGame()
    {
        //string location = Application.streamingAssetsPath + "/SaveData";
        if (true)//File.Exists(location))
        {
            //StreamReader reader = new StreamReader(location);
            string data = PlayerPrefs.GetString("SaveData");//reader.ReadToEnd();
            if (string.IsNullOrEmpty(data))
            {
                CreateNewSaveData();
            }
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream stream = new MemoryStream(Convert.FromBase64String(data));
                mUser = (SaveData)formatter.Deserialize(stream);
            }
            FixSaveData();
            //reader.Close();
        }
        //else
        //{
        //    CreateNewSaveData();
        //}
    }

    protected void FixSaveData()
    {         
        if(mUser.PlayerItemLevelArr == null)
        {
            mUser.PlayerItemLevelArr = new int[Constants.PLAYER_ITEM_COUNT];
            mUser.PlayerItemLevelArr[0] = 1;
        }
        else if (mUser.PlayerItemLevelArr.Length != Constants.PLAYER_ITEM_COUNT)
        {
            int[] temp = new int[Constants.PLAYER_ITEM_COUNT];
            int count = Mathf.Min(Constants.PLAYER_ITEM_COUNT, mUser.PlayerItemLevelArr.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.PlayerItemLevelArr[i];
            }
            mUser.PlayerItemLevelArr = temp;
        }

        if(mUser.SkillCooltimeArr == null)
        {
            mUser.SkillCooltimeArr = new float[Constants.SKILL_COUNT];
        }
        else if(mUser.SkillCooltimeArr.Length != Constants.SKILL_COUNT)
        {             
            float[] temp = new float[Constants.SKILL_COUNT];
            int count = Mathf.Min(Constants.SKILL_COUNT, mUser.SkillCooltimeArr.Length);
            for(int i =0; i < count; i++)
            {
                temp[i] = mUser.SkillCooltimeArr[i];
            }
            mUser.SkillCooltimeArr = temp;
        }
    }

    protected void CreateNewSaveData()
    {
        mUser = new SaveData();
        mUser.Gold = 0;

        mUser.Stage = 0;
        mUser.LastGemID = -1;
        mUser.Progress = 0;

        mUser.PlayerItemLevelArr = new int[Constants.PLAYER_ITEM_COUNT];
        mUser.PlayerItemLevelArr[0] = 1;
        mUser.SkillCooltimeArr = new float[Constants.SKILL_COUNT];
    }

    protected void Save()
    {
        //string location = Application.streamingAssetsPath + "/SaveData";
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();
        //StreamWriter writer = new StreamWriter(location);

        formatter.Serialize(stream, mUser);
        string data = Convert.ToBase64String(stream.GetBuffer());
        PlayerPrefs.SetString("SaveData", data);
        //writer.Write(data);
        //writer.Close();
    }
}
