using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour, IItem
{
    public int ammo = 30;

    public void Use(GameObject target)
    {
        PlayerGun playerGun = target.GetComponent<PlayerGun>();
        if(playerGun.gun && playerGun != null) playerGun.gun.currentAmmo += ammo;
        Destroy(gameObject);
    }
}

