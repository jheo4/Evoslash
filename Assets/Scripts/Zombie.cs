using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Zombie : LivingObject
{
    public enum AIState {
        Chase,
        Attack
    };
    public float speed = 1f;

    public LayerMask targetLayerMask;
    private LivingObject targetObject;

    private Rigidbody zombieRigidbody;
    private Transform targetPlayer;
    private Animator anim;
    private NavMeshAgent agent;
    public AIState currState;

    public AudioClip deathSound;
    public AudioClip hitSound;
    public AudioClip attackSound;
    private AudioSource audioPlayer;

    public float attackDamage = 5f;
    public float attackDelay = 3.0f;
    private float lastAttackTime;

    public float exp;

    public bool hasTargetObject
    {
        get {
            if(targetObject != null && targetObject.isLive) return true;
            else return false;
        }
    }


    private void Awake()
    {
        zombieRigidbody = GetComponent<Rigidbody>();
        targetPlayer = FindObjectOfType<PlayerMovement>().transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audioPlayer = GetComponent<AudioSource>();
    }


    public void Setup(float HP, float damage, float exp)
    {
        this.lastAttackTime = 0;
        this.maxHP = HP;
        this.HP = HP;
        this.exp = exp;
        this.attackDamage = damage;
        this.currState = AIState.Chase;
    }


    void Start()
    {
        StartCoroutine(UpdatePath());
    }


   // Update is called once per frame
    void Update()
    {
        if(isLive && currState == AIState.Chase) {
            anim.SetTrigger("runTrigger");
        }

        if (hasTargetObject && (transform.position - targetObject.transform.position).magnitude >= 30f) {
            anim.SetFloat("speed",2);
            this.speed = 2f;
        } else {
            anim.SetFloat("speed",1);
            this.speed = 1f;
        }

        /*
            case AIState.Chase:
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
        }*/
    }


    // instead of update, use coroutine for updating path & finding target
    private IEnumerator UpdatePath() {
        while(isLive) {
            if(hasTargetObject) {
                if( (transform.position - targetObject.transform.position).magnitude <= 1f ) {
                    agent.isStopped = true;
                }
                else {
                    agent.isStopped = false;
                    zombieRigidbody.velocity = transform.forward * speed;
                    agent.SetDestination(targetObject.transform.position);
                }
            }
            else {
                agent.isStopped = true;
                Collider[] livingObjectCollidersWithMask = Physics.OverlapSphere(transform.position, 50f,
                                                                                 targetLayerMask);
                for(int i = 0; i < livingObjectCollidersWithMask.Length; i++) {
                    LivingObject livingObject = livingObjectCollidersWithMask[i].GetComponent<LivingObject>();
                    if(livingObject != null && livingObject.isLive) {
                        targetObject = livingObject;
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }


    public override void OnHit(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if(isLive) {
            audioPlayer.PlayOneShot(hitSound, 0.08f);
        }
        base.OnHit(damage, hitPoint, hitNormal);
    }


    public override void Die() {
        GameManager.instance.OnEnemyDeath();
        // Destroy is registered as event to onDeath

        base.Die();
        StartCoroutine(DeathCoroutine());
    }


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
        other.GetComponent<LivingObject>().OnHit(10.0f, hitPoint, hitNormal);
        yield return new WaitForSeconds(3.0f);

        currState = AIState.Chase;
    }
}

