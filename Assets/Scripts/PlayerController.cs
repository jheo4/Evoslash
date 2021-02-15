using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;

    public GameObject bulletPrefab;
    public float speed = 5f;
    public float jumpPower = 30f;
    public GameObject sword;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        // movement
        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;
        Vector3 newVelocity = new Vector3(xSpeed, 0f, zSpeed);
        playerRigidbody.velocity = newVelocity;

        if(Input.GetKey(KeyCode.Space) == true) {
            // jump
            playerRigidbody.AddForce(0f, jumpPower, 0f);
        }

        if(Input.GetMouseButtonDown(0)) {
            // attack
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.transform.LookAt(transform.forward);
        }
    }

    public void Die() {
        gameObject.SetActive(false);

        //GameManager gameManager = FindObjectOfType<GameManager>();
        //gameManager.EndGame();
    }
}
