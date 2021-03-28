using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Public instance fields
    public GameObject mainMenu;
    public GameObject creditsScreen;
    public string startScene = "DemoScene";

    // Start is called before the first frame update
    void Start()
    {
        this.mainMenu.SetActive(true);
        this.creditsScreen.SetActive(false);
    }

    // OnStart is called when the user clicks the "Start" button
    public void OnStart()
    {
        SceneManager.LoadScene(this.startScene);
    }

    // OnCredits is called when the user clicks the "Credits" button
    public void OnCredits()
    {
        this.mainMenu.SetActive(false);
        this.creditsScreen.SetActive(true);
    }

    // OnExit is called when the user clicks the "Exit" button
    public void OnExit()
    {
        Application.Quit();
    }

    // OnBack is called when the user clicks the "Back" button from within the credits screen
    public void OnBack()
    {
        this.mainMenu.SetActive(true);
        this.creditsScreen.SetActive(false);
    }
}
