using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Section1Unlock : MonoBehaviour
{
    public GameObject crate1;
    public GameObject crate2;
    public GameObject crate3;
    public GameObject sword;
    public TextMeshProUGUI text;
    PlayerEXP playerEXP;
    int level;


    // Start is called before the first frame update
    void Start()
    {
        playerEXP = sword.GetComponent<PlayerEXP>();
    }

    // Update is called once per frame
    void Update()
    {
        level = playerEXP.level;
        if (level >= 3 && Input.GetKeyDown(KeyCode.F)) {
            this.OpenArea();
        }
    }

    void OpenArea() {
        Destroy(crate1);
        Destroy(crate2);
        Destroy(crate3);
        Destroy(text);
    }

    void OnTriggerEnter(Collider c) {
        if (c.tag == "Player") {
            text.SetText("These crates are really heavy! I'll have to be at least level 3 to move them, and when I am, I should press the F key!");
        }
    }

    void OnTriggerExit(Collider c) {
        if (c.tag == "Player") {
            text.SetText("");
        }
    }
}
