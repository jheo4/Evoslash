using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKControl : MonoBehaviour
{
    // Woman-->hard to add animation
    // Sword-->animation already
    private Animator playerAnimator;
    private PlayerInput playerInput;
    private Rigidbody rigidbody;
    public Sword sword;
    public Transform swordTransform;
    public Transform rightHandle;

    

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }


    // These two are for different weapons
    void OnEnable()
    {
    }
    void OnDisable()
    {
    }

    void Update()
    {
        // 1. Sense the input: fire
        // 2. Activate the animation of sword
        //if (playerInput.fire)
        //{
        //    sword.Attack();
        //}
    }

    void OnAnimatorIK()
    {
        float turningAngle = playerInput.rotate * 100 * Time.fixedDeltaTime;

        //Sword Hink for IK
        sword.transform.position = playerAnimator.GetIKPosition(AvatarIKGoal.RightHand);
        sword.transform.rotation = rigidbody.rotation * Quaternion.Euler(40, turningAngle, 0f);

        // Right Hand
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
        playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandle.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandle.rotation);

        // Left Hand
        //playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        //playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
       // playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandle.position);
       // playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandle.rotation);
    }
}