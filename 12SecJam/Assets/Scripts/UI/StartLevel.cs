using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartLevel : MonoBehaviour
{
    [SerializeField] private string text;
    [SerializeField] private AudioClip audioClip;
    public void StartGame()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
    }

    public void PlayAudio()
    {
        FindObjectOfType<GameManager>().DestroySubtitle(text, audioClip.length, audioClip);
    }

}
