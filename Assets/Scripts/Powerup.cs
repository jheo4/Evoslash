using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    Collider collider;
    public AudioSource powerupAudio;


    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            print("player power up!");
            powerupAudio.Play();
            Destroy(this.gameObject);
        }

    }
}
