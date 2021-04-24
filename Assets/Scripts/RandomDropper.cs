using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Deprecated: replaced by VolumePowerupDropper
public class RandomDropper : MonoBehaviour
{
    public GameObject AmmoPrefab;
    public GameObject InvincPrefab;
    public GameObject QuickPrefab;
    public GameObject HealthPack;
    private NavMeshAgent navMeshAgent;
    private float timePassed;
    private float rate = 4f;
    private List<GameObject> powerups;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(RandomPosition(transform.position,10,-1));
        timePassed = 0;
        powerups = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
       timePassed += Time.deltaTime;
       if (timePassed >= rate) {
           timePassed = 0f;
           this.DropPickup();
           navMeshAgent.SetDestination(RandomPosition(transform.position,40,-1));
       }
    }

    void DropPickup() {
        float rand = Random.Range(0,30f);
        if (rand <= 3) {
            GameObject ammo = Instantiate(AmmoPrefab,transform.position,transform.rotation);
            // powerups.Add(ammo);
            // InGameUI.UpdatePowerups(powerups);
        } else if (rand > 3 && rand <= 10) {
            GameObject invincibility = Instantiate(InvincPrefab,transform.position,transform.rotation);
        } else if (rand > 10 && rand <= 20) {
            GameObject speed = Instantiate(QuickPrefab,transform.position,transform.rotation);
        } else {
            GameObject health = Instantiate(HealthPack,transform.position,transform.rotation);
        }
    }

    Vector3 RandomPosition(Vector3 origin, float maxDistance, int mask) {
        Vector3 randomLocation = Random.insideUnitSphere * maxDistance;
        randomLocation += origin;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomLocation,out hit,maxDistance,mask);
        return hit.position;
    }


}
