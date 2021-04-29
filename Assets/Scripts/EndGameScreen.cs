using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameScreen : MonoBehaviour
{
    // Public instance fields
    public float blurAnimationDuration = 0.5f;
    public GameObject inner;
    public Text title;
    public GameObject restartButton;
    public string startScene = "DemoScene";
    public string menuScene = "MenuScene";

    // Instance fields
    private MenuScreenBlur screenBlur;
    private MouseHiding mouseHiding;

    // Start is called before the first frame update
    void Start()
    {
        // Obtain a reference to the UI
        this.screenBlur = FindObjectOfType<MenuScreenBlur>();
        if (this.screenBlur == null)
        {
            Debug.LogError("No instance of MenuScreenBlur found in scene");
        }

        // Obtain a reference to the MouseHiding
        this.mouseHiding = FindObjectOfType<MouseHiding>();
        if (this.mouseHiding == null)
        {
            Debug.LogError("No instance of MouseHiding found in scene");
        }

        // Hide the inner menu
        this.inner.SetActive(false);
    }

    // Performs shared show logic,
    // including showing the inner menu and blurring the screen
    private void Show()
    {
        this.mouseHiding.OnGuiOpen();
        this.inner.SetActive(true);
        this.screenBlur.Blur(this.blurAnimationDuration);
    }

    // Shows the end game menu with the victory title
    public void ShowVictory()
    {
        this.Show();
        this.title.text = "Victory";
        this.title.color = new Color(1f, 1f, 1f);
        this.restartButton.SetActive(false);
    }

    // Shows the end game menu with the loss title
    public void ShowLoss()
    {
        this.Show();
        this.title.text = "You Lost";
        this.title.color = new Color(1f, 0.58f, 0.58f);
        this.restartButton.SetActive(true);
    }

    // OnRestart is called when the user clicks the "Restart" button
    public void OnRestart()
    {
        SceneManager.LoadScene(this.startScene);
    }

    // OnReturnToMenu is called when the user clicks the "Return to Menu" button
    public void OnReturnToMenu()
    {
        SceneManager.LoadScene(this.menuScene);
    }

    // OnQuit is called when the user clicks the "Quit" button
    public void OnQuit()
    {
        Application.Quit();
    }
}
