using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Zombie : MonoBehaviour
{
    public enum AIState {
        Chase,
        Attack
    };
    public float speed = 1f;
    private Rigidbody zombieRigidbody;
    private Transform targetPlayer;
    private Animator anim;
    private NavMeshAgent agent;
    public AIState currState;
    //GameObject zombie;

    // Start is called before the first frame update
    void Start()
    {
        zombieRigidbody = GetComponent<Rigidbody>();
        targetPlayer = FindObjectOfType<PlayerMovement>().transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //zombie = GetComponent<GameObject>();
        currState = AIState.Chase;
    }

   // Update is called once per frame
    void Update()
    {
        //anim.SetFloat("vely", agent.velocity.magnitude / agent.speed);
        switch(currState) {
            case AIState.Chase:
            agent.SetDestination(targetPlayer.transform.position);
            zombieRigidbody.velocity = transform.forward * speed;
            if ((zombieRigidbody.transform.position - targetPlayer.transform.position).magnitude <= 2) {
                anim.SetTrigger("attackTrigger");
                agent.isStopped = true;
                currState = AIState.Attack;
            }
            transform.LookAt(targetPlayer);
            break;

            case AIState.Attack:
            if ((zombieRigidbody.transform.position - targetPlayer.transform.position).magnitude >= 5) {
                agent.SetDestination(targetPlayer.transform.position);
                anim.SetTrigger("runTrigger");
                agent.isStopped = false;
                currState = AIState.Chase;
            }
            break;
        }
    }

<<<<<<< Updated upstream
    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
=======

    public override void OnHit(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if(isLive) {
            audioPlayer.PlayOneShot(hitSound, 0.08f);
>>>>>>> Stashed changes
        }

        //if(other.tag == "Wall") {
        //    targetPlayer = FindObjectOfType<PlayerController>().transform;
        //    transform.LookAt(targetPlayer);
        //    zombieRigidbody.velocity = transform.forward * speed;
        //}
    }

<<<<<<< Updated upstream
    public void Die() {
        GameManager.instance.OnEnemyDeath();
        Destroy(gameObject);
=======

    private IEnumerator DeathCoroutine()
    {
        agent.isStopped = true;
        agent.enabled = false;
        anim.SetTrigger("Die");
        audioPlayer.PlayOneShot(deathSound, 0.08f);
        zombieRigidbody.velocity = transform.forward * 0;
        zombieRigidbody.freezeRotation = true;
        Collider[] zombieColliders = GetComponents<Collider>();
        for(int i = 0; i < zombieColliders.Length; i++) zombieColliders[i].enabled = false;
        yield return new WaitForSeconds(3.0f);

    }


    void OnTriggerStay(Collider other) {
        if(isLive && Time.time >= lastAttackTime + attackDelay && currState == AIState.Chase) {
            LivingObject attackTarget = other.GetComponent<LivingObject>();
            if(attackTarget != null && attackTarget == targetObject) { // attack only chasing object
                StartCoroutine(Attack(other));
            }
        }
    }


    private IEnumerator Attack(Collider other) {
        lastAttackTime = Time.time;
        currState = AIState.Attack;
        anim.SetTrigger("attackTrigger");
        audioPlayer.PlayOneShot(attackSound, 0.3f);
        Vector3 hitPoint = other.ClosestPoint(transform.position);
        Vector3 hitNormal = transform.position - other.transform.position;
        other.GetComponent<LivingObject>().OnHit(this.attackDamage, hitPoint, hitNormal);
        yield return new WaitForSeconds(3.0f);

        currState = AIState.Chase;
>>>>>>> Stashed changes
    }
}

