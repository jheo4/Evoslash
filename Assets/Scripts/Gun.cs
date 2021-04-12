using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum State {
        Ready,
        Empty,
        Reloading
    }

    public State state{get; private set;}
    public Transform muzzleTransform;
    public ParticleSystem muzzleFireEffect;

    private LineRenderer bulletTrajectoryRenderer;
    private AudioSource audioPlayer;
    public AudioClip fireSound, reloadSound;

    public float damage = 10;
    public float range = 20f;

    public int currentAmmo = 150;
    public int magazineCapacity = 30;
    public int currentAmmoInMagazine = 30;

    public float fireDelay = 0.1f;
    public float reloadDelay = 2.0f;
    private float lastFireTime;

    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();

        bulletTrajectoryRenderer = GetComponent<LineRenderer>();
        bulletTrajectoryRenderer.positionCount = 2;
        bulletTrajectoryRenderer.enabled = false;
    }

    private void OnEnable()
    {
        //currentAmmoInMagazine = magazineCapacity;
        state = State.Ready;
        lastFireTime = 0;
    }

    public void Fire()
    {
        if(state == State.Ready && Time.time > lastFireTime+fireDelay) {
            lastFireTime = Time.time;
            Shot();
        }
    }

    private void Shot()
    {
        RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;

        if(Physics.Raycast(muzzleTransform.position, muzzleTransform.forward, out hit, range)) {
            IDamageable target = hit.collider.GetComponent<IDamageable>();
            if(target != null) target.OnHit(damage, hit.point, hit.normal);
            hitPosition = hit.point;
        }
        else {
            hitPosition = muzzleTransform.position + muzzleTransform.forward * range;
        }

        StartCoroutine(ShootingEffectRoutine(hitPosition));
        currentAmmoInMagazine--;
        if (currentAmmoInMagazine <= 0) state = State.Empty;
    }


    private IEnumerator ShootingEffectRoutine(Vector3 hitPosition)
    {
        muzzleFireEffect.Play();
        audioPlayer.PlayOneShot(fireSound);

        bulletTrajectoryRenderer.SetPosition(0, muzzleTransform.position);
        bulletTrajectoryRenderer.SetPosition(1, hitPosition);
        bulletTrajectoryRenderer.enabled = true;
        yield return new WaitForSeconds(0.03f);
        bulletTrajectoryRenderer.enabled = false;
    }

    public bool Reload()
    {
        if(state == State.Reloading || currentAmmo <= 0 || currentAmmoInMagazine >= magazineCapacity) return false;
        StartCoroutine(ReloadRoutine());
        return true;
    }

    private IEnumerator ReloadRoutine()
    {
        state = State.Reloading;
        audioPlayer.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(reloadDelay);

        int reloadingAmmo = magazineCapacity - currentAmmoInMagazine;
        if(currentAmmo < reloadingAmmo) reloadingAmmo = currentAmmo;

        currentAmmoInMagazine += reloadingAmmo;
        currentAmmo -= reloadingAmmo;

        state = State.Ready;
    }
}
