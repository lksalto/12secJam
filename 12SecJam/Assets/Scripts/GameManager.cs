using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    // Start is called before the first frame update
    void Start()
    {
        // DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)) // When Boss Spawns
            FlickerLight();
        
        if((Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.E))) && interactionScript.isImageOpen)
        {
            CloseInteractImage();
        }
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
}
