using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3;
    public float rotateSpeed = 100;

    private PlayerInput input;
    private Rigidbody rigidbody;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
    }


    private void FixedUpdate()
    {
        Rotate();
        Move();
        animator.SetFloat("Move", input.move);
    }

    private void Rotate() {
        float turningAngle = input.rotate * rotateSpeed * Time.fixedDeltaTime;
        if(input.rotate == 0)
            rigidbody.angularVelocity = Vector3.zero;
        else
            rigidbody.rotation = rigidbody.rotation * Quaternion.Euler(0, turningAngle, 0f);
    }

    private void Move() {
        Vector3 movingDistance = input.move * transform.forward * moveSpeed * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + movingDistance);
    }

    // TODO
    private void Jump() {

    }
}
