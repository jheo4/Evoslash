using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Animator animator;
    private Collider collider;
    private AudioSource audioPlayer;
    public AudioClip swingAudio, contactAudio;

    public float damage = 100;
    public float delayBetweenAttack = 0.8f;
    public float attackTime = 1.0f;
    private float lastAttackTime;
    public PlayerEXP playerEXP;

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
        playerEXP = GetComponent<PlayerEXP>();
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
        if (Time.timeScale != 0)
        {
            state = State.Attack;
            audioPlayer.PlayOneShot(swingAudio, 0.3f);
            animator.SetTrigger("SlashTrigger");

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Katana Swing Cross Body RL"))
            {
                animator.SetBool("currRL", true);
            }
            else
            {
                animator.SetBool("currRL", false);
            }

            yield return new WaitForSeconds(attackTime);
            state = State.Ready;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (state == State.Attack && c.tag == "Zombie")
        {
            Zombie zombie = c.GetComponent<Zombie>();
            if (zombie != null)
            {
                audioPlayer.PlayOneShot(contactAudio, 0.3f);
                Vector3 hitPoint = c.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - c.transform.position;
                zombie.OnHit(damage, hitPoint, hitNormal);
            }
        }

    }
}
