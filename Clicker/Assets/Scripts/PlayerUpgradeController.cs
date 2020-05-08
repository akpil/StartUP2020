using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerUpgradeController : MonoBehaviour
{
    private List<UIElement> mElementList;
    [SerializeField]
    private UIElement mElementPrefab;
    [SerializeField]
    private Transform mElementArea;
    // Start is called before the first frame update
    void Start()
    {
        //세이브데이터 불러오기
        mElementList = new List<UIElement>();
        UIElement elem = Instantiate(mElementPrefab, mElementArea);
        elem.Init(0, null, "test1", "1", "power up", "2", LevelUP);
        mElementList.Add(elem);
    }

    public void LevelUP(int id, int amount)
    {
        GameController.Instance.GoldCallback = LevelUpCallback;
        GameController.Instance.Gold -= 2;        
    }

    public void LevelUpCallback()
    {
        GameController.Instance.TouchPower++;
        UIElement elem = Instantiate(mElementPrefab, mElementArea);
        elem.Init(0, null, "test1", "1", "power up", "2", LevelUP);
        mElementList.Add(elem);
    }

}
