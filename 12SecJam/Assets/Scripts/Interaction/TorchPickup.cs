using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorchPickup : MonoBehaviour
{
    private bool canInteract;
    private Animator playerAnim;
    [SerializeField] GameObject interactionImage;
    [SerializeField] private GameObject torch;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            //Change player sprite
            //Destroy torch
            torch.SetActive(true);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionImage.SetActive(true);
            playerAnim = other.GetComponentInChildren<Animator>();
            canInteract = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionImage.SetActive(false);
            canInteract = false;
        }
    }
}
