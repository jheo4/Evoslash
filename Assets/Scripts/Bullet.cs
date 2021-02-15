using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 6f;
    private Rigidbody bulletRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        bulletRigidbody.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Hit target
    void OnTriggerEnter(Collider other) {
        if(other.tag == "Zombie") {
            Zombie zombie = other.GetComponent<Zombie>();
            if(zombie != null) {
                zombie.Die();
                Destroy(gameObject);
            }
        }

        if(other.tag == "Wall") {
            Destroy(gameObject);
        }
    }
}
