using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWarp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerMovement>().Stop();
            // Don't let player die            
            FindObjectOfType<GameManager>().NextScene();
        }
    }
}
