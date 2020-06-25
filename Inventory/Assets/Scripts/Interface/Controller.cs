using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]
    private GameObject mCharacterObj;
    private Character mControlTarget;
    // Start is called before the first frame update
    void Start()
    {
        mControlTarget = mCharacterObj.GetComponent<Character>();
        mControlTarget.Attack();
        mControlTarget.Hit(100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
