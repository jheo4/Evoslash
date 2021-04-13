using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockYPosition : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x,0,transform.position.z);
        // if(transform.position.y >= 0.3) {
        //     transform.position = new Vector3(transform.position.x,0,transform.position.z);
        // }
    }
}
