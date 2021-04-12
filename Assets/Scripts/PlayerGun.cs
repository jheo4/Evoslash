using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public Gun gun;
    public Transform gunTransform;
    public Transform leftHandleTransform, rightHandleTransform;
    private PlayerInput playerInput;
    private Animator playerAnimator;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
        OnDisable();
    }

    private void OnEnable()
    {
        gun.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        gun.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(playerInput.useGun) {
            OnEnable();
            playerAnimator.SetBool("UseGun", true);
        }
        else if(playerInput.useSword) {
            OnDisable();
            playerAnimator.SetBool("UseGun", false);
        }
        else if(playerInput.fire && gun.gameObject.active)
            gun.Fire();
        else if(playerInput.reload && gun.gameObject.active) {
            if(gun.Reload()) {
                playerAnimator.SetTrigger("Reload");
            }
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if(gun != null && InGameUI.instance != null) {
            if(InGameUI.instance.gameObject.active) {
                InGameUI.instance.UpdateAmmoText(gun.currentAmmoInMagazine, gun.currentAmmo);
            }
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(gun.gameObject.active) {
            gunTransform.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);
            playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
            playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
            playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandleTransform.position);
            playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandleTransform.rotation);

            playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
            playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
            playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandleTransform.position);
            playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandleTransform.rotation);
        }
    }
}

