using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;

    [SerializeField] private GameObject firstText;
    
    [SerializeField] AudioClip mainMenuMusic;
    [SerializeField] AudioClip gameplayMusic;
    [SerializeField] AudioSource audioSource;
    public void StartGame()
    {
        mainMenu.SetActive(false);
        firstText.SetActive(true);
        audioSource.clip = gameplayMusic;
        audioSource.Play(); 
    }
}
