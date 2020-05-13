using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delegates
{
    public delegate void VoidCallback();
    public delegate void TwoIntInVoidCallback(int value1, int value2);
}

[Serializable]
public class PlayerStat
{
    public int ID;
    public int CurrentLevel;
    public int MaxLevel;

    public eCostType CostType;
    public double CostBase;
    public double CostWeight;
    public double CostCurrent;
    public double CostTenWeight;

    public bool IsPercent;
    public double ValueBase;
    public double ValueWeight;
    public double ValueCurrent;

    public float Cooltime;
    public float Duration;
}

[Serializable]
public class PlayerStatText
{
    public int ID;
    public string Title;
    public string ContentsFormat;
}