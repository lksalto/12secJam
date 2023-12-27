using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    private EnemyAI enemyAI;
    private EnemyAudio enemyAudio;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        enemyAI = GetComponentInParent<EnemyAI>();
        enemyAudio = GetComponentInParent<EnemyAudio>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.enabled = true;
            enemyAI.enabled = true;
            enemyAudio.enabled = true;
            this.enabled = false;
            FindObjectOfType<GameManager>().FlickerLight();
            GetComponent<CircleCollider2D>().enabled = false;
            enemyAI.StartCoroutine("WakeUp");
        }
    }
}
