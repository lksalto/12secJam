using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPackInteraction : Interaction
{
    private bool opened;
    [SerializeField] List<AudioClip> audioClip;
    [SerializeField] string subtitleText;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            if (isImageOpen && !canOpen && canClose)
            {
                gm.CloseInteractImage();
                opened = true;
                canInteract = false;
                interactionImage.SetActive(false);
                gm.DestroyFast();
            }
            else if (canOpen && !isImageOpen)
            {
                gm.PlayCLipAtPlayer(audioClip[0]);
                gm.OpenInteractImage(interactionObject, this);
                gm.DestroySubtitle(subtitleText, audioClip, false);
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
