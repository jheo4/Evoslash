using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Animator animator;
    Collider collider;
    private bool isSwing = false;
    public AudioSource swordSwingAudio;
    public AudioSource swordContactAudio;

    public enum State
    { // For future use
        Ready,
        Moving
    }
    public State state { get; private set; }

    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        //collider.enabled = false;
    }

    public void Attack()
    {
        // change the
        animator.SetTrigger("SlashTrigger");
        print("Attack!");
        swordSwingAudio.Play();
    }


    void Update()
    {
        if (animator.GetCurrentAnimatorClipInfo(0).Length > 0 && animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Katana Swing")
        {
            isSwing = true;
        }
        else
        {
            isSwing = false;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (isSwing)
        {
            if (c.tag == "Zombie")
            {
                Zombie zombie = c.GetComponent<Zombie>();
                if (zombie != null)
                {
                    swordContactAudio.Play();
                    zombie.Die();
                }
            }
        }
           
    }
}