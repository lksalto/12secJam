using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPackInteraction : Interaction
{
    private bool opened;
    [SerializeField] AudioClip audioClip;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            if (isImageOpen && !canOpen && canClose)
            {
                gm.CloseInteractImage();
                opened = true;
            }
            else if (canOpen && !isImageOpen)
            {
                gm.OpenInteractImage(interactionObject, this);
                gm.PlayCLipAtPlayer(audioClip);
                canInteract = false;
                interactionImage.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !opened)
        {
            interactionImage.SetActive(true);
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
