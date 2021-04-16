using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour, IItem
{
    public Type type = Type.Invincibility;
    public int duration = 12;

    public enum Type {
        Speed,
        Invincibility
    }

public class Active
    {
        public int remaining;
        public Type type;

        public Active(int remaining, Type type)
        {
            this.remaining = remaining;
            this.type = type;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use(GameObject target)
    {
        PowerupManager manager = target.GetComponent<PowerupManager>();
        if (manager != null)
        {
            Destroy(this.gameObject);
            manager.PickUp(this.type, this.duration);
        }
    }
}
