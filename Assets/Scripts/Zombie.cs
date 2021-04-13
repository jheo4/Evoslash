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
        anim.SetFloat("vely", agent.velocity.magnitude / agent.speed);
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

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
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

