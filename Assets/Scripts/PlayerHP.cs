using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : LivingObject
{
    public Slider HPSlider;
    public AudioClip deathSound;
    public AudioClip hitSound;
    public AudioClip itemPickupSound;

    private AudioSource audioPlayer;
    private Animator animator;

    private PlayerMovement playerMovement;
    private PlayerSword playerSword;
    private PlayerGun playerGun;

    private bool isInvinncible = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();

        playerMovement = GetComponent<PlayerMovement>();
        playerSword = GetComponent<PlayerSword>();
        playerGun = GetComponent<PlayerGun>();
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        HPSlider.gameObject.SetActive(true);
        HPSlider.maxValue = maxHP;
        HPSlider.value = HP;

        playerMovement.enabled = true;
        playerSword.enabled = true;
        playerGun.enabled = true;
    }


    public override void Heal(float healingPoint)
    {
        base.Heal(healingPoint);
        HPSlider.value = HP;
    }


    public override void OnHit(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (isInvinncible) return;
        if(isLive) {
            audioPlayer.PlayOneShot(hitSound, 0.3f);
        }
        base.OnHit(damage, hitPoint, hitNormal);
        HPSlider.value = HP;
    }


    public override void Die()
    {
        base.Die();
        audioPlayer.PlayOneShot(deathSound, 0.3f);
        animator.SetTrigger("Die");

        playerMovement.enabled = false;
        playerSword.enabled = false;
        playerGun.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(isLive) {
            print("OnTriggerEnter Item");
            IItem item = other.GetComponent<IItem>();
            if(item != null) {
                item.Use(gameObject);
                audioPlayer.PlayOneShot(itemPickupSound, 0.15f);
            }
        }
    }

    public void SetInvincible(bool invincible)
    {
        this.isInvinncible = invincible;
    }


    public void increaseMaxHP(int amountToIncrease)
    {
        maxHP = maxHP + amountToIncrease;
        HPSlider.maxValue = maxHP;
    }
}
