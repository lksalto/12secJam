using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [SerializeField] protected GameObject interactionImage;
    [SerializeField] protected Sprite interactionObject;
    public bool canInteract;
    public bool isImageOpen;
    public bool canOpen = true;
    public bool canClose = false;
    protected GameManager gm;
    
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            if(isImageOpen && !canOpen && canClose)
                gm.CloseInteractImage();
            else if(canOpen && !isImageOpen)
                gm.OpenInteractImage(interactionObject, this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
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
