using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerEXP : MonoBehaviour
{
    // Public Instance Variables
    public int level;
    public float baseLevelUpExp = 50f;
    public AudioClip levelUpSound;
    public GameObject playerReference;

    // Private Instance Variables
    private AudioSource audioPlayer;
    private float exp;
    private float levelUpExp;
    private InGameUI ui;

    // Start is called before the first frame update
    void Start()
    {
        this.ui = InGameUI.instance;

        levelUpExp = baseLevelUpExp;
        exp = 0;
        level = 1;
        this.UpdateUI();

        audioPlayer = GetComponent<AudioSource>();
    }

    // addExperience is called whenever a kill is achieved
    public void addExperience(float add) {
        exp += add;
        if (exp >= this.levelUpExp) {
            this.levelUp();
        } else {
            this.UpdateUI();
        }
    }

    // Updates the InGameUI with the current values of this script
    private void UpdateUI()
    {
        this.ui.SetMaxExperience(this.levelUpExp);
        this.ui.SetExperience(this.exp);
        this.ui.SetLevel(this.level);
    }

    // levelUp is called whenever the player reaches the experience threshold
    private void levelUp() {
        // increase player health total
        PlayerHP php = playerReference.GetComponent<PlayerHP>();
        php.HPSlider.maxValue = php.HPSlider.maxValue + 10;
        // heal player
        php.Heal(php.HPSlider.maxValue - php.HPSlider.value);
        // play level up noise
        audioPlayer.PlayOneShot(levelUpSound, 0.5f);

        level += 1;
        // increse maximum exp needed to level up
        this.levelUpExp = (25 * level) + this.baseLevelUpExp;
        exp = 0;

        this.UpdateUI();
    }
}
