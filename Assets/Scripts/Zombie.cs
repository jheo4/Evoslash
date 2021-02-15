using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody zombieRigidbody;
    private Transform targetPlayer;

    // Start is called before the first frame update
    void Start()
    {
        zombieRigidbody = GetComponent<Rigidbody>();
        targetPlayer = FindObjectOfType<PlayerController>().transform;
        //Debug.Log(transform.forward.x + ", " + transform.forward.y + ", " + transform.forward.z);
    }

   // Update is called once per frame
    void Update()
    {
        transform.LookAt(targetPlayer);
        zombieRigidbody.velocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if(playerController != null) {
                playerController.Die();
            }
        }

        //if(other.tag == "Wall") {
        //    targetPlayer = FindObjectOfType<PlayerController>().transform;
        //    transform.LookAt(targetPlayer);
        //    zombieRigidbody.velocity = transform.forward * speed;
        //}
    }

    public void Die() {
        Destroy(gameObject);
    }
}

