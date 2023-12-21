using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartLevel : MonoBehaviour
{
    [SerializeField] private string text;
    [SerializeField] private List<AudioClip> audioClip;
    [SerializeField] private bool hasMonsterAudio;
    public void StartGame()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
    }

    public void PlayAudio()
    {
        if(audioClip != null)
            FindObjectOfType<GameManager>().DestroySubtitle(text, audioClip, hasMonsterAudio);
    }

}
