using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormHeadAnim : MonoBehaviour
{

    [SerializeField] Animator anim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        anim.SetTrigger("attack");
    }
    
    public void Damage()
    {

    }
}
