using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public static InGameUI instance
    {
        get {
            if(inGameUIInstance == null) {
                inGameUIInstance = FindObjectOfType<InGameUI>();
            }
            return inGameUIInstance;
        }
    }

    private static InGameUI inGameUIInstance;

    // Public instance fields
    public Text primaryText;
    public Text secondaryText;
    public Text tertiaryText;
    public Text ammoText;
    public Image experienceBar;
    public Text levelText;
    public GameObject activePowerupTemplate = null;
    public Transform spawnPoint = null;
    public float powerupPadding = 2f;
    // Yes this is kinda terrible but I'm not sure what the best solution is
    // without some heavy Dictionary inspector editor
    public Sprite speedSprite;
    public Sprite invincibilitySprite;

    // Private instance fields
    private float maxExperience = 1f;
    private float experience;
    private List<GameObject> activePowerups = new List<GameObject>();
    private Dictionary<Powerup.Type, Sprite> powerupSprites = new Dictionary<Powerup.Type, Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        this.SetExperience(0.0f);
        this.powerupSprites[Powerup.Type.Speed] = this.speedSprite;
        this.powerupSprites[Powerup.Type.Invincibility] = this.invincibilitySprite;
    }

    // Sets the objective text on the in-game UI
    public void SetObjective(string primary, string secondary, string tertiary)
    {
        this.primaryText.text = primary.ToUpper();
        this.secondaryText.text = secondary;
        this.tertiaryText.text = tertiary;
    }

    public void UpdateAmmoText(int currentAmmoInMagazine, int currentAmmo) {
        ammoText.text = currentAmmoInMagazine + "/" + currentAmmo;
    }

    // Sets the experience in the in-game UI
    public void SetExperience(float exp)
    {
        this.experience = exp;
        this.updateExperience();
    }

    // Sets the max experience in the in-game UI
    public void SetMaxExperience(float maxExp)
    {
        this.maxExperience = maxExp;
        this.updateExperience();
    }

    // Updates the experience bar according to the current exp/max exp
    public void updateExperience()
    {
        this.experienceBar.rectTransform.localScale =
            new Vector3(Mathf.Clamp(this.experience / this.maxExperience, 0f, 1f), 1.0f, 1.0f);
    }

    // Set the level in the in-game UI
    public void SetLevel(int level)
    {
        this.levelText.text = "Level: " + level;
    }

    // Re-render the list of powerups according to the given list
    public void UpdatePowerups(List<Powerup.Active> powerups)
    {
        // Disable all existing powerup UI components
        for (int i = 0; i < this.activePowerups.Count; ++i)
        {
            this.activePowerups[i].SetActive(false);
        }

        // Instantiate new powerup UI components as needed
        int newComponents = Mathf.Max(0, powerups.Count - this.activePowerups.Count);
        for (int i = 0; i < newComponents; ++i)
        {
            GameObject spawnedItem = Instantiate(this.activePowerupTemplate,
                this.spawnPoint.position, this.spawnPoint.rotation);
            spawnedItem.transform.SetParent(this.spawnPoint, false);
            this.activePowerups.Add(spawnedItem);
        }

        // Make active and populate the rest of the UI components
        float currentOffset = 0f;
        for (int i = 0; i < powerups.Count; ++i)
        {
            GameObject powerupComponent = this.activePowerups[i];
            Powerup.Active powerupDefinition = powerups[i];
            Sprite sprite;
            powerupSprites.TryGetValue(powerupDefinition.type, out sprite);
            if (sprite != null)
            {
                powerupComponent.SetActive(true);
                ActivePowerup contents = powerupComponent.GetComponent<ActivePowerup>();
                contents.duration.text = powerupDefinition.remaining.ToString();
                contents.icon.sprite = sprite;

                currentOffset += (48 + this.powerupPadding);
            }
        }
    }

    // Sets the temporary objective text that can be displayed on an interactable
    public void SetInteractiveText(string text)
    {
        // TODO implement
    }
}
