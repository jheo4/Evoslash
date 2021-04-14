using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    private Rigidbody rigidbody;
    public float damage = 15f;
    public GameObject gameObject;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        if (gameObject == null) {
            gameObject = GetComponent<GameObject>();
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider c) {
        if (c.tag == "Zombie") {
            PlayerHP playerHP = gameObject.GetComponent<PlayerHP>();
            if (playerHP != null) {
                playerHP.OnHit(damage, this.transform.position,c.transform.position);
            }
        }
    }

    void OnCollisionEnter(Collider c) {
        if (c.tag == "Zombie") {
            PlayerHP playerHP = gameObject.GetComponent<PlayerHP>();
            if (playerHP != null) {
                playerHP.OnHit(5f, this.transform.position,c.transform.position);
            }
        }
    }
}
