using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    // Private instance fields
    private List<Powerup.Active> activePowerups = new List<Powerup.Active>();
    private InGameUI ui;
    private float secondCounter = 0f;
    private PlayerMovement playerMovement;
    private PlayerHP playerHp;

    // Start is called before the first frame update
    void Start()
    {
        this.playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        this.playerHp = this.gameObject.GetComponent<PlayerHP>();
        this.ui = InGameUI.instance;
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
                    this.EndPowerup(this.activePowerups[i].type);
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
            this.BeginPowerup(type);
        }

        this.UpdateUI();
    }

    private void UpdateUI()
    {
        this.ui.UpdatePowerups(this.activePowerups);
    }

    private void BeginPowerup(Powerup.Type type)
    {
        switch (type)
        {
            case Powerup.Type.Speed:
                this.playerMovement.SetMoveSpeedMultplier(2f);
                break;
            case Powerup.Type.Invincibility:
                this.playerHp.SetInvincible(true);
                break;
        }
    }

    private void EndPowerup(Powerup.Type type)
    {
        switch (type)
        {
            case Powerup.Type.Speed:
                this.playerMovement.SetMoveSpeedMultplier(1f);
                break;
            case Powerup.Type.Invincibility:
                this.playerHp.SetInvincible(false);
                break;
        }
    }
}
