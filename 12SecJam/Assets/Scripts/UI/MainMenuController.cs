using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject howToPlayMenu;
    [SerializeField] GameObject creditsMenu;
    GameObject currentMenu;
    [SerializeField] private GameObject firstText;
    
    [SerializeField] AudioClip mainMenuMusic;
    [SerializeField] AudioClip gameplayMusic;
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        currentMenu = mainMenu;
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        firstText.SetActive(true);
        audioSource.clip = gameplayMusic;
        audioSource.Play();
        currentMenu = firstText;
    }

    public void HowToPlayMenu()
    {
        currentMenu.SetActive(false);
        currentMenu = howToPlayMenu;
        currentMenu.SetActive(true);
    }

    public void CreditsMenu()
    {
        currentMenu.SetActive(false);
        currentMenu = creditsMenu;
        currentMenu.SetActive(true);
    }
    
    public void BackToMainMenu()
    {
        currentMenu.SetActive(false);
        mainMenu.SetActive(true);
        currentMenu = mainMenu;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
