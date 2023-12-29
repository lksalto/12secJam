using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySeeker : MonoBehaviour
{
    private EnemyAI parentAI;
    private EnemyAudio parentAudio;
    CapsuleCollider2D parentCollider;

    private Animator animator;
    CircleCollider2D circleCollider2D;
    
    // Start is called before the first frame update
    void Start()
    {
        parentAudio = GetComponentInParent<EnemyAudio>();
        parentAI = GetComponentInParent<EnemyAI>();
        parentAI.isAlive = false;
        parentCollider = GetComponentInParent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            WakeUp();
    }
    
    void WakeUp()
    {
        circleCollider2D.enabled = false;
        parentAudio.enabled = true;
        animator.enabled = true;
        Debug.Log("Wake");

    }

    private void StartMoving()
    {
        Debug.Log("Move");
        parentAI.enabled = true;
        parentAI.isAlive = true;
        parentCollider.enabled = true;
        this.enabled = false;
    }
}
