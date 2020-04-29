using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShopController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI mText;
    [SerializeField]
    private Button[] mButtonArr;
    [SerializeField]
    private UIElement[] mElementArr;

    private Delegates.IntInVoidReturn call;
    private Delegates.VoidCallback mCallBack;
    [SerializeField]
    private GameController mController;

    // Start is called before the first frame update
    void Start()
    {
        mText.text = "aaaa";
        mElementArr[0].Init(0, "공격증가", "공격력이 1 증가합니다.", 0, 10, levelUP);
        mElementArr[1].Init(1, "방어증가", "방어력이 0.1 증가합니다.", 0, 15, levelUP);
        mElementArr[2].Init(2, "체력증가", "체력이 1 증가합니다.", 0, 20, levelUP);
    }

    public void ButtonCall()
    {
        if(mCallBack != null)
        {
            mCallBack();
        }
    }

    public void ButtonCallAdd()
    {
        mCallBack += () =>
                      {
                          Debug.Log("test!!");
                      };
    }

    public void levelUP(int id)
    {
        Debug.Log("level up: " + id);
    }

    public void GoldSpend1()
    {
        mController.UseGold(1);
    }

    public void GoldSpend2()
    {
        mController.UseGold(150, () => { Debug.Log("Use 150 gold"); });
    }

    public void GoldSpend3()
    {
        mController.UseGold(10, () => { Debug.Log("Spend 10 gold"); });
    }

    bool isClicked;

    public bool OpenPopup()
    {
        isClicked = false;
        while (!isClicked)
        {
            //
        }
        return true;
    }
    private IEnumerator routine(bool isClicked)
    {
        while (!isClicked)
        {
            yield return new WaitForFixedUpdate();
        }
    }


    Delegates.VoidCallback mdelegates;
    public void OpenPopup(Delegates.VoidCallback callback)
    {
        mdelegates = callback;
    }

    public void PopupButtonClicked()
    {
        mdelegates();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(OpenPopup())
            {
                Debug.Log(3);
            }
        }
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            OpenPopup(() => { Debug.Log(3); });
        }
    }
}
