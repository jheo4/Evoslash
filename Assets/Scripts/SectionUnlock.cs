using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionUnlock : MonoBehaviour
{
    public GameObject rock;
    public GameObject sword;
    public GameObject spawnerRoot;
    public int levelThreshold;
    public string incentive;

    // Private instance variables
    private InGameUI ui;
    private PlayerEXP playerEXP;
    private bool playerInRange = false;
    private bool opened = false;

    // Start is called before the first frame update
    void Start()
    {
        // Obtain a reference to the UI
        this.ui = FindObjectOfType<InGameUI>();
        if (this.ui == null)
        {
            Debug.LogError("No instance of InGameUI found in scene");
        }

        playerEXP = sword.GetComponent<PlayerEXP>();
        spawnerRoot.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.playerInRange && !this.opened) {
            string baseText = "These rocks are really heavy, but I can see a stash of " + incentive + " behind them!\n\n";
            if (this.playerEXP.level >= this.levelThreshold)
            {
                ui.SetInteractiveText(baseText +
                    "<i>Press F to move rocks</i>");
            }
            else
            {
                ui.SetInteractiveText(baseText +
                    "<i>Minimum level: " +
                    this.levelThreshold +
                    "</i>");
            }
        }
        else
        {
            ui.SetInteractiveText("");
        }

        if (this.playerInRange &&
            !this.opened &&
            playerEXP.level >= this.levelThreshold &&
            Input.GetKeyDown(KeyCode.F)) {
            this.OpenArea();
        }
    }

    void OpenArea()
    {
        spawnerRoot.SetActive(true);
        Destroy(rock);
        this.opened = true;
    }

    void OnTriggerEnter(Collider c) {
        if (c.tag == "Player") {
            this.playerInRange = true;
        }
    }

    void OnTriggerExit(Collider c) {
        if (c.tag == "Player") {
            ui.SetInteractiveText("");
            this.playerInRange = false;
        }
    }
}
