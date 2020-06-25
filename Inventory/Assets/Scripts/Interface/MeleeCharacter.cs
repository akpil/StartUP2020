using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCharacter : MonoBehaviour, Character
{
    public void Attack()
    {
        Debug.Log("Melee attack");
    }

    public void Hit(int damage)
    {
        Debug.Log("Melee Character hit " + damage);
    }
}
