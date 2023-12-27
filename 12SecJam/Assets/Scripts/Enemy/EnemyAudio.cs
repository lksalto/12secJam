using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private AudioClip audioclip;
    private AudioSource audioSource;
    [SerializeField] private bool isBoss;
    private Transform player;
    [SerializeField] float distanceToPlaySound = 8f;
    private float distanceToPlayer;

    private bool playingSound;
    private GameManager gm;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < distanceToPlaySound && !playingSound)
        {
            playingSound = true;
            StartCoroutine("PlayAudio");
            if (isBoss)
                TryFlickerLight();
        }
    }

    private IEnumerator PlayAudio()
    {
        audioSource.PlayOneShot(audioclip);
        yield return new WaitForSeconds(audioclip.length);
        playingSound = false;
    }

    private void TryFlickerLight()
    {
        if(gm == null)
            gm = FindObjectOfType<GameManager>();

        int random = UnityEngine.Random.Range(0, 10);
        if(random < 8)
            gm.FlickerLight();
    }
}
