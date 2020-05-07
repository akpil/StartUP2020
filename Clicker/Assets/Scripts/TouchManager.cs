using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private Camera mMainCamera;
    [SerializeField]
    private GameObject mDummy;
    // Start is called before the first frame update
    void Start()
    {
        mMainCamera = Camera.main;
    }

    private Ray GenerateRay(Vector3 screenPos)
    {
        screenPos.z = mMainCamera.nearClipPlane;
        Vector3 origin = mMainCamera.ScreenToWorldPoint(screenPos);
        screenPos.z = mMainCamera.farClipPlane;
        Vector3 dest = mMainCamera.ScreenToWorldPoint(screenPos);

        return new Ray(origin, dest - origin);
    }

    private bool CheckTouch(out Vector3 vec)
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = GenerateRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (gameObject == hit.collider.gameObject)
                        {
                            vec = hit.point;
                            return true;
                        }
                    }
                }
            }
        }
        vec = Vector3.zero;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = GenerateRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(gameObject == hit.collider.gameObject)
                {
                    GameObject gameObj = Instantiate(mDummy);
                    gameObj.transform.position = hit.point;
                    GameController.Instance.Touch();
                }
            }
            //LayerMask aa = LayerMask.NameToLayer("Building") + LayerMask.NameToLayer("Enemy");
            //LayerMask mask = (1 << LayerMask.NameToLayer("Building")) +
            //                 (1 << LayerMask.NameToLayer("Enemy"));
            //int layerMask = 256 + 512;
            //layerMask = (1 << 8) + (1 << 9);
            //RaycastHit2D hit2D = Physics2D.Raycast(Vector2.one, Vector2.zero, 0, layerMask);
        }
        Vector3 pos;
        if(CheckTouch(out pos))
        {
            GameObject gameObj = Instantiate(mDummy);
            gameObj.transform.position = pos;
            GameController.Instance.Touch();
        }
        
    }
}
