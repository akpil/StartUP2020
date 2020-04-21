using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eItemType
{
    Life, Homing, Multi
}

public class ItemController : MonoBehaviour
{
    [SerializeField]
    private Player mPlayer;
    public void ItemFunction(eItemType id)
    {
        switch(id)
        {
            case eItemType.Life:
                AddLife();
                break;
            case eItemType.Homing:
                SetHoming();
                break;
            case eItemType.Multi:
                AddMoreBolt();
                break;
        }
    }
    private void AddLife()
    {
        mPlayer.AddLife();
    }
    private void SetHoming()
    {
        mPlayer.StartHoming(3);
    }
    private void AddMoreBolt()
    {
        mPlayer.AddBoltCount();
    }
}
