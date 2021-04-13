using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpwaner : MonoBehaviour
{
    public GameObject ZombiePrefab;
    public float spawnRateMin = 0.0f;
    public float spawnRateMax = 50.0f;

    private Transform target;
    private float spawnRate;
    private float timeAfterSpawn;

    // Start is called before the first frame update
    void Start()
    {
        timeAfterSpawn = 0f;
        spawnRate = Random.RandomRange(spawnRateMin, spawnRateMax);
    }

    // Update is called once per frame
    void Update()
    {
        timeAfterSpawn += Time.deltaTime;
        if(timeAfterSpawn > spawnRate) {
            // Spawn a zombie
            Vector3 zombielocation = new Vector3(transform.position.x,transform.position.y - 2,transform.position.z - 3);
            GameObject zombie = Instantiate(ZombiePrefab, zombielocation, transform.rotation);

            timeAfterSpawn = 0f;
            spawnRate = Random.RandomRange(spawnRateMin, spawnRateMax);
        }
    }
}

