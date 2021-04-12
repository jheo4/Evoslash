using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    private Animator playerAnimator;
    private PlayerInput playerInput;
    private Rigidbody rigidbody;
    public Sword sword;
    public Transform swordTransform;
    public Transform rightHandle;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable() {
        sword.gameObject.SetActive(true);
    }

    void OnDisable() {
        sword.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 1. Sense the input: fire
        // 2. Activate the animation of sword
        if(playerInput.useGun)
            OnDisable();
        else if(playerInput.useSword)
            OnEnable();
        else if(playerInput.fire && sword.gameObject.active) {
            sword.Attack();
        }
    }

    void OnAnimatorIK() {
        if(sword.gameObject.active) {
            // Sword Hink for IK
            float turningAngle = playerInput.rotate * 100 * Time.fixedDeltaTime;
            sword.transform.position = playerAnimator.GetIKPosition(AvatarIKGoal.RightHand);
            sword.transform.rotation = rigidbody.rotation * Quaternion.Euler(40, turningAngle, 0f);

            // Right Hand
            playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
            playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);
            playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandle.position);
            playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandle.rotation);
        }
    }
}
