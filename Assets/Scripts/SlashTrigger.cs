using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashTrigger : MonoBehaviour
{
    Animator animator;
    private bool slashing;

    // Start is called before the first frame update
    void Start()
    {
        //Get Animator component
        animator = GetComponent<Animator>();
        slashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Play spin animation on key press
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("slashTrigger");
            slashing = true;
        }
    }
    void FixedUpdate() {
        slashing = false;
    }

    void onTriggerEnter(Collider c) {
        Debug.Log("Collide");
        if (c.tag == "Zombie") {
            Zombie zombie = c.GetComponent<Zombie>();
            if(zombie != null) {
                zombie.Die();
            }
        }
    }
}
