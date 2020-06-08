using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryText
{
    public int ID;
    public string Title;
    public string Story;
    public string[] Selection;
}

[System.Serializable]
public class Item
{
    public int ID;
    public eItemType ItemType;
    public eRankType RankType;
}

public enum eRankType
{
    Normal,
    Magic,
    Epic,
    Legend
}
public enum eItemType
{
    Weapon,
    Armor,
    Ring,
    Amulet
}

