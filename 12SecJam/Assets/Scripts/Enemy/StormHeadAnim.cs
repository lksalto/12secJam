using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormHeadAnim : MonoBehaviour
{

    [SerializeField] Animator anim;
    [SerializeField] float atkCD = 3f;
    [SerializeField] float detectionRadius;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] int dmg = 1;
    void Start()
    {
        InvokeRepeating("Attack", 0, atkCD);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    public void Attack()
    {
        anim.SetTrigger("attack");
    }
    
    public void Damage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                collider.transform.gameObject.GetComponentInParent<PlayerLife>().TakeDmg(dmg);
            }
        }
    }
}
