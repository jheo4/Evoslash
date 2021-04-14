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

    // Private instance fields
    private float maxExperience;
    private float experience;

    // Start is called before the first frame update
    void Start()
    {
        this.SetExperience(0.0f);
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
            new Vector3(this.experience / this.maxExperience, 1.0f, 1.0f);
    }

    // Set the level in the in-game UI
    public void SetLevel(int level)
    {
        this.levelText.text = "Level: " + level;
    }
}
