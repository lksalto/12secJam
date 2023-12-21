using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] int life = 1;
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject jumpscare;

    public void Die(bool boss)
    {
        if (!boss)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            StartCoroutine(Jumpscare(15f));
        }
    }
    IEnumerator Jumpscare(float s)
    {
        jumpscare.gameObject.active = true;
        yield return new WaitForSeconds(s);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
