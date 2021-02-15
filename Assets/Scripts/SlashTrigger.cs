using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashTrigger : MonoBehaviour
{
    Animator animator;
    private Rigidbody sword;
    private Collider sword_Collider;

    // Start is called before the first frame update
    void Start()
    {
        //Get Animator component
        animator = GetComponent<Animator>();
        sword = GetComponent<Rigidbody>();
        sword_Collider = GetComponent<Collider>();
        sword_Collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Play spin animation on key press
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("slashTrigger");
            sword_Collider.enabled = true;
        }
    }

    void DisableCollision() {
        sword_Collider.enabled = false;
    }

    void OnTriggerEnter(Collider c) {
        if (c.tag == "Zombie") {
            Zombie zombie = c.GetComponent<Zombie>();
            if(zombie != null) {
                zombie.Die();
            }
        }
    }
}
