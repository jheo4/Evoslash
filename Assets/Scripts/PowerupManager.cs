using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    // Private instance fields
    private List<Powerup.Active> activePowerups;
    private InGameUI ui;
    private float secondCounter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        this.ui = InGameUI.instance;
        activePowerups = new List<Powerup.Active>();
        this.UpdateUI();
    }

    void FixedUpdate()
    {
        this.secondCounter += Time.fixedDeltaTime;
        while (this.secondCounter >= 1f)
        {
            this.secondCounter -= 1f;

            // Update all active powerups & check to remove
            for (int i = 0; i < this.activePowerups.Count; ++i)
            {
                this.activePowerups[i].remaining -= 1;
                if (this.activePowerups[i].remaining <= 0)
                {
                    this.activePowerups.RemoveAt(i);
                    i--;
                }
            }
        }

        this.UpdateUI();
    }

    public void PickUp(Powerup.Type type, int duration)
    {
        // See if there are any active powerups with the same type.
        // If so, simply extend their duration
        bool found = false;
        for (int i = 0; i < this.activePowerups.Count; ++i)
        {
            if (this.activePowerups[i].type == type)
            {
                this.activePowerups[i].remaining += duration;
                found = true;
                break;
            }
        }

        if (!found)
        {
            this.activePowerups.Add(new Powerup.Active(duration, type));
        }

        this.UpdateUI();
    }

    private void UpdateUI()
    {
        this.ui.UpdatePowerups(this.activePowerups);
    }
}
