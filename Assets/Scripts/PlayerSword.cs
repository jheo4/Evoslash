using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    private Animator playerAnimator;
    private PlayerInput playerInput;
    public Sword sword;
    public Transform swordTransform;
    public Transform leftHandle, rightHandle;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
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
        if(playerInput.fire) {
            sword.Attack();
        }
    }

    void OnAnimatorIK() {
        // Sword Hink for IK
        sword.transform.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);
        sword.transform.rotation = this.transform.rotation;

        // Right Hand
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);
        playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandle.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandle.rotation);
    }
}
