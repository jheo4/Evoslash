using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public AudioClip hitSound;
    private AudioSource audioPlayer;

    void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }


    void OnTriggerEnter(Collider c)
    {
        audioPlayer.PlayOneShot(hitSound, 0.5f);
    }
}

