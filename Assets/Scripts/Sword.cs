using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Animator animator;
    public Collider collider;
    private AudioSource audioPlayer;
    public AudioClip swingAudio, contactAudio;

    public float damage = 100;
    public float delayBetweenAttack = 0.8f;
    public float attackTime = 1.0f;
    private float lastAttackTime;

    public enum State
    { // For future use
        Ready,
        Attack
    }
    public State state { get; private set; }

    private void Awake() {
        audioPlayer = GetComponentInChildren<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        collider = GetComponentInChildren<Collider>();
    }

    private void OnEnable() {
        lastAttackTime = 0;
        state = State.Ready;
    }

    public void Attack() {
        if(state == State.Ready && Time.time >= lastAttackTime + delayBetweenAttack) {
            lastAttackTime = Time.time;
            StartCoroutine(Swing());
        }
    }

    // Swing Coroutine
    private IEnumerator Swing() {
        state = State.Attack;
        audioPlayer.PlayOneShot(swingAudio);
        animator.SetTrigger("SlashTrigger");
        yield return new WaitForSeconds(attackTime);
        state = State.Ready;
    }

    void OnTriggerEnter(Collider c)
    {
        if (state == State.Attack && c.tag == "Zombie")
        {
            print("Hit zombie!");
            Zombie zombie = c.GetComponent<Zombie>();
            if (zombie != null)
            {
                audioPlayer.PlayOneShot(contactAudio);
                zombie.Die();
            }
        }

    }
}
