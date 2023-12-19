using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] int life = 1;
    
    public void TakeDmg(int x)
    {
        life -= x;
        if(life <= 0)
        {
            Debug.Log("MORREU");
            Die();
        }
    }

    public void Die()
    {

    }
}
