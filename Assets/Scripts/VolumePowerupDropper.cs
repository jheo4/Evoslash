using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VolumePowerupDropper : MonoBehaviour
{
    public float spawnInterval = 6f;
    public float spawnIntervalVariance = 2f;
    public float maxNavMeshSampleDistance = 5f;
    public int navMeshSampleMask = -1;
    public Vector3 powerupSpawnOffset = new Vector3(0f, 0.5f, 0f);
    public float ammoSpawnWeight = 3f;
    public float invincibilitySpawnWeight = 7f;
    public float speedSpawnWeight = 10f;
    public float healthSpawnWeight = 10f;
    public GameObject ammoPrefab;
    public GameObject invincibilityPrefab;
    public GameObject speedPrefab;
    public GameObject healthPrefab;

    private BoxCollider volume;
    private float currentSpawnIntervalRemaining;
    private PlayerVisibilityTester playerVisibility;

    void Start()
    {
        // Obtain a reference to the PlayerVisibilityTester
        this.playerVisibility = FindObjectOfType<PlayerVisibilityTester>();
        if (this.playerVisibility == null)
        {
            Debug.LogError("No instance of PlayerVisibilityTester found in scene");
        }

        this.volume = this.GetComponent<BoxCollider>();
        this.ResetSpawn();
    }

    void FixedUpdate()
    {
        this.currentSpawnIntervalRemaining -= Time.fixedDeltaTime;
        if (this.currentSpawnIntervalRemaining <= 0)
        {
            this.ResetSpawn();

            Vector3 position;
            while (true)
            {
                // Spawn a random powerup at a random location inside the volume,
                // then projected onto the NavMesh
                Vector3 volumeLocation = this.RandomPointInVolume();
                NavMeshHit hit;
                NavMesh.SamplePosition(volumeLocation, out hit, this.maxNavMeshSampleDistance, this.navMeshSampleMask);

                // Ensure that the point isn't visible to the player
                if (!this.playerVisibility.IsVisible(hit.position))
                {
                    position = hit.position;
                    break;
                }
            }

            Quaternion rotation = Quaternion.AngleAxis(Random.Range(0f, 360f), new Vector3(0, 1, 0));
            GameObject prefab = this.SelectPickup();
            Instantiate(prefab, position + this.powerupSpawnOffset, rotation);
        }
    }

    private void ResetSpawn()
    {
        this.currentSpawnIntervalRemaining = Random.Range(
            this.spawnInterval - this.spawnIntervalVariance,
            this.spawnInterval + this.spawnIntervalVariance
        );
    }

    private Vector3 RandomPointInVolume()
    {
        // Adapted from:
        // https://forum.unity.com/threads/pick-random-point-inside-box-collider.541585/
        Bounds bounds = this.volume.bounds;
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    private GameObject SelectPickup()
    {
        float rand = Random.Range(0,
            this.ammoSpawnWeight +
            this.invincibilitySpawnWeight +
            this.speedSpawnWeight +
            this.healthSpawnWeight);
        if (rand <= ammoSpawnWeight)
        {
            return this.ammoPrefab;
        }
        else
        {
            rand -= ammoSpawnWeight;
        }

        if (rand <= invincibilitySpawnWeight)
        {
            return this.invincibilityPrefab;
        }
        else
        {
            rand -= invincibilitySpawnWeight;
        }

        if (rand <= speedSpawnWeight)
        {
            return this.speedPrefab;
        }

        return this.healthPrefab;
    }
}
