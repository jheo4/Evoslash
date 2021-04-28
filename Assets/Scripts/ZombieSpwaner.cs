using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpwaner : MonoBehaviour
{
    public GameObject mesh;
    public Zombie ZombiePrefab;
    public float spawnRateMin = 1.0f;
    public float spawnRateMax = 3.0f;

    public float damageMax = 2f;
    public float damageMin = 0.5f;

    public float HPMax = 100;
    public float HPMin = 10;

    public float EXPMax = 20;
    public float EXPMin = 5;

    private float spawnRate;
    private float timeAfterSpawn;
    private PlayerVisibilityTester playerVisibility;

    private List<Zombie> zombies = new List<Zombie>();

    // Start is called before the first frame update
    void Start()
    {
        if (this.mesh != null)
        {
            this.mesh.SetActive(false);
        }

        timeAfterSpawn = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);

        // Get an instance of the PlayerVisibilityTester
        this.playerVisibility = FindObjectOfType<PlayerVisibilityTester>();
        if (this.playerVisibility == null)
        {
            Debug.LogError("No instance of PlayerVisibilityTester found in scene");
        }
    }

    void FixedUpdate()
    {
        timeAfterSpawn += Time.fixedDeltaTime;
        if(timeAfterSpawn > spawnRate) {
            // Only spawn a zombie if the player cannot see the spawner
            if (!this.playerVisibility.IsVisible(this.gameObject.transform.position))
            {
                // Attempt to spawn a zombie
                if (GameManager.instance.AttemptEnemySpawn())
                {
                    float zombieIntensity = Random.Range(0f, 1f);
                    CreateZombie(zombieIntensity);
                }
            }

            timeAfterSpawn = 0f;
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draws a ray to the player, or the hit position
        PlayerVisibilityTester playerVisibility = FindObjectOfType<PlayerVisibilityTester>();
        if (playerVisibility != null)
        {
            playerVisibility.DrawGizmo(this.gameObject.transform.position);
        }
    }

    private void CreateZombie(float intensity)
    {
        float HP = Mathf.Lerp(HPMin, HPMax, intensity);
        float damage = Mathf.Lerp(damageMin, damageMax, intensity);
        float exp = Mathf.Lerp(EXPMin, EXPMax, intensity);

        Zombie zombie = Instantiate(ZombiePrefab, transform.position, transform.rotation);
        zombie.Setup(HP, damage, exp);
        zombies.Add(zombie);

        zombie.onDeath += () => zombies.Remove(zombie);
        zombie.onDeath += () => Destroy(zombie.gameObject, 3f);
        zombie.onDeath += () => GameManager.instance.AddPlayerEXP(zombie.exp);
    }
}
