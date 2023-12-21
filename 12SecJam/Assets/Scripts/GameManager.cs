using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private Image interactionImage;

    [SerializeField] private GameObject interactionZoom;
    [SerializeField] private Animator lightSourceAnim;
    // [SerializeField] private Animator staminaImageAnim;
    [SerializeField] private Animator staminaContainerAnim;
    private Interaction interactionScript;

    [SerializeField] private Image startLevelImage;
    
    [Header("Subtitles")]
    [SerializeField] private GameObject initialSubtitleObject;
    [SerializeField] private float subtitleTime = 10f;
    [SerializeField] private string subtitle;
    [SerializeField] private List<AudioClip> audioClips;
     private bool hasMonsterAudio;
    [SerializeField] private AudioSource playerAudioSource;
    
    [Header("EndGame")]
    [SerializeField] private GameObject endGameMenu;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void PlayCLipAtPlayer(AudioClip audioClip)
    {
        playerAudioSource.clip = audioClip;
        playerAudioSource.Play();
    }
    public void DestroySubtitle(string subtitle, List<AudioClip> audioClips, bool hasMonsterAudio)
    {
        this.hasMonsterAudio = hasMonsterAudio;
        if ((audioClips.Count > 0 && !hasMonsterAudio) || (audioClips.Count > 1 && hasMonsterAudio))
        {
            this.audioClips = audioClips;
            this.subtitle = subtitle;
            AudioClip audioClip = audioClips[0];
            PlayCLipAtPlayer(audioClip);
            subtitleTime = audioClip.length;
            initialSubtitleObject.SetActive(true);
            initialSubtitleObject.GetComponentInChildren<TextMeshProUGUI>().text = subtitle;
            StartCoroutine("DisableSubtitle");
        }
        else if(audioClips.Count == 1 && hasMonsterAudio)
        {
            this.audioClips = audioClips;
            this.subtitle = subtitle;
            AudioClip audioClip = audioClips[0];
            PlayCLipAtPlayer(audioClip);
            subtitleTime = audioClip.length;
            initialSubtitleObject.SetActive(false);
            // 
            StartCoroutine("DisableSubtitle");
            
        }
        audioClips.RemoveAt(0);
    }

    public void EndGame()
    {
        endGameMenu.SetActive(true);
    }

    public IEnumerator DisableSubtitle()
    {
        yield return new WaitForSeconds(subtitleTime);
        initialSubtitleObject.SetActive(false);
        DestroySubtitle(subtitle, audioClips, hasMonsterAudio);
    }
    
    public IEnumerator DisableSubtitleFast()
    {
        yield return new WaitForSeconds(2f);
        initialSubtitleObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)) // When Boss Spawns
            FlickerLight();
        
        // if((Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.E))) && interactionScript.isImageOpen)
        // {
        //     CloseInteractImage();
        // }
    }

    
    public void NextScene()
    {
        //Transition
        StartCoroutine("Fade");
    }
    
    IEnumerator Fade()
    {
        fadeImage.GetComponent<Animator>().SetBool("isFading", true);
        yield return new WaitForSeconds(3f);
        //load next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenInteractImage(Sprite interactionObject, Interaction interactionScript)
    {
        interactionScript.canOpen = false;
        interactionZoom.SetActive(true);
        interactionImage.sprite = interactionObject;
        interactionImage.SetNativeSize();
        this.interactionScript = interactionScript;
        StartCoroutine("ImageCooldown");
    }

    private IEnumerator ImageCooldown()
    {
        yield return new WaitForSeconds(0.2f);
        if (!interactionScript.isImageOpen)
        {
            Time.timeScale = 0f;
            interactionScript.isImageOpen = true;
            interactionScript.canClose = true;
        }
        else
        {
            interactionZoom.SetActive(false);
            interactionScript.isImageOpen = false;
            interactionScript.canOpen = true;
        }
    }

    public void CloseInteractImage()
    {
        interactionScript.canClose = false;
        interactionImage.GetComponent<Animator>().SetBool("isZoomingIn", false);
        Time.timeScale = 1f;
        StartCoroutine("ImageCooldown");
    }
    
    // Makes light flicker for a brief second
    public void FlickerLight()
    {
        // Play Weird Sound
        lightSourceAnim.SetTrigger("isFlickering");
        // staminaImageAnim.SetTrigger("isFlickering");
        staminaContainerAnim.SetTrigger("isFlickering");

        
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
