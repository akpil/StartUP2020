using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : CSVLoader
{
    [SerializeField]
    private ItemData[] mInfoArr;
    [SerializeField]
    private Sprite[] mSpriteArr;
    // Start is called before the first frame update
    void Awake()
    {
        LoadCSV(out mInfoArr, "CSVFiles/ItemTable");
        mSpriteArr = Resources.LoadAll<Sprite>("Item");
    }

    public ItemData GetItem(int id)
    {
        return mInfoArr[id].GetClone();
    }

    public Sprite GetItemSprite(int id)
    {
        return mSpriteArr[id];
    }
}

[System.Serializable]
public class ItemData
{
    public int ID;
    public eItemType ItemType;
    public int Level;
    public string Name;
    public string Contents;

    public ItemData GetClone()
    {
        return MemberwiseClone() as ItemData;
    }
}

public enum eItemType
{
    Armor,
    Pants,
    Weapon,
    Others
}