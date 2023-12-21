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
        enemyAI = GetComponent<EnemyAI>();
        enemyAudio = GetComponent<EnemyAudio>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.enabled = true;
            enemyAI.enabled = true;
            enemyAudio.enabled = true;
        }
    }
}
