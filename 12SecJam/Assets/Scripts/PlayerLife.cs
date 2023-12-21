using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] int life = 1;
    [SerializeField] GameObject jumpscare;

    public void Die(bool boss)
    {
        if (!boss)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            StartCoroutine(Jumpscare(5f));
        }
    }
    IEnumerator Jumpscare(float s)
    {
        jumpscare.SetActive(true);
        yield return new WaitForSeconds(s);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    
}
