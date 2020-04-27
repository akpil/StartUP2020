using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private float mDamage;
    public void SetDamage (float value)
    {
        mDamage = value;
        EnteredArr = new List<GameObject>();
    }

    List<GameObject> EnteredArr;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Hit(mDamage);

        }
    }
}
