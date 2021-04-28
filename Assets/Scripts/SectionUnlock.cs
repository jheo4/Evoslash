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

    // Private instance variables
    private InGameUI ui;
    private PlayerEXP playerEXP;
    private bool playerInRange;

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
        if (this.playerInRange &&
            playerEXP.level >= this.levelThreshold &&
            Input.GetKeyDown(KeyCode.F)) {
            this.OpenArea();
        }
    }

    void OpenArea()
    {
        spawnerRoot.SetActive(true);
        Destroy(rock);
    }

    void OnTriggerEnter(Collider c) {
        if (c.tag == "Player") {
            ui.SetInteractiveText("These rocks are really heavy! I'll have to be at least level "
                + this.levelThreshold
                + " to move them, and when I am, I should press the F key!");
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
