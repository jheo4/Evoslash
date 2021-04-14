using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerEXP : MonoBehaviour
{
    // Public Instance Variables
    public Slider EXPSlider;
    public int level;
    public float levelUpExp;
    public AudioClip levelUpSound;
    public TextMeshProUGUI tmp;
    public GameObject playerReference;

    // Private Instance Variables
    private AudioSource audioPlayer;
    private float exp;
    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        levelUpExp = EXPSlider.maxValue;
        audioPlayer = GetComponent<AudioSource>();
        exp = 0;
    }

    // addExperience is called whenever a kill is achieved
    public void addExperience(float add) {
        exp += add;
        if (exp >= EXPSlider.maxValue) {
            this.levelUp();
        } else {
            EXPSlider.value = exp;
        }
    }

    // levelUp is called whenever the player reaches the experience threshold
    private void levelUp() {
        // increase player health total
        PlayerHP php = playerReference.GetComponent<PlayerHP>();
        php.HPSlider.maxValue = php.HPSlider.maxValue + 10;
        // heal player
        php.Heal(php.HPSlider.maxValue - php.HPSlider.value);
        // play level up noise
        audioPlayer.PlayOneShot(levelUpSound);
        level += 1;
        tmp.text = "Level: " + level.ToString();
        // increse maximum exp needed to level up
        EXPSlider.maxValue = (25 * level) + EXPSlider.maxValue;
        EXPSlider.value = 0;
        exp = 0;
    }

}
