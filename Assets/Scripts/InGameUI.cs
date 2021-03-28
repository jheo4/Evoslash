using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    // Public instance fields
    public Text primaryText;
    public Text secondaryText;
    public Text tertiaryText;
    public Image healthBar;
    public Image experienceBar;

    // Start is called before the first frame update
    void Start()
    {
        this.SetHealth(1.0f);
        this.SetExperience(0.0f);
    }

    // Sets the objective text on the in-game UI
    public void SetObjective(string primary, string secondary, string tertiary)
    {
        this.primaryText.text = primary.ToUpper();
        this.secondaryText.text = secondary;
        this.tertiaryText.text = tertiary;
    }

    // Sets the health in the in-game UI
    public void SetHealth(float health)
    {
        this.healthBar.rectTransform.localScale = new Vector3(health, 1.0f, 1.0f);
    }

    // Sets the experience in the in-game UI
    public void SetExperience(float exp)
    {
        this.experienceBar.rectTransform.localScale = new Vector3(exp, 1.0f, 1.0f);
    }
}
