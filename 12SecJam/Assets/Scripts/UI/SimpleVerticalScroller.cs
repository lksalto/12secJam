using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleVerticalScroller : MonoBehaviour
{
    public float scrollSpeed = 1.0f;
    private bool stopWhenInMiddle;
    private static readonly int IsFading = Animator.StringToHash("isFading");

    private void Start()
    {
        if (this.CompareTag("Image"))
            stopWhenInMiddle = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (stopWhenInMiddle && transform.localPosition.y >= 0f)
        {
            scrollSpeed = 0f;
            StartCoroutine("Fade");
        }

        transform.position += Vector3.up * (scrollSpeed * Time.deltaTime);
        if(transform.localPosition.y > 1750f)
            Destroy(this.gameObject);
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(1.5f);
        GetComponent<Animator>().SetBool(IsFading, true);
        yield return new WaitForSeconds(3f);
        //load next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    
    
}
