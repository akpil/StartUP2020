using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCharacter : MonoBehaviour, Character
{
    public void Attack()
    {
        Debug.Log("Range attack");
    }

    public void Hit(int damage)
    {
        Debug.Log("Range Character hit " + damage);
    }
}
