using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Animator animator;
    public enum State
    { // For future use
        Ready,
        Moving
    }
    public State state { get; private set; }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        // change the
        //animator.SetTrigger("slashTrigger");
        print("Attack!");
    }

    void Update()
    {
    }
}