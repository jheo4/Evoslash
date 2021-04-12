using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour, IItem
{
    public float HP = 15;

    public void Use(GameObject target)
    {
        LivingObject livingObject = target.GetComponent<LivingObject>();
        if(livingObject != null) livingObject.Heal(HP);
        Destroy(gameObject);
    }
}
