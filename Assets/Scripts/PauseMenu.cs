using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Public instance fields
    public float blurAnimationDuration = 0.125f;
    public GameObject inner;

    // Instance fields
    private MenuScreenBlur screenBlur;

    // Start is called before the first frame update
    void Start()
    {
        // Obtain a reference to the UI
        this.screenBlur = FindObjectOfType<MenuScreenBlur>();
        if (this.screenBlur == null)
        {
            Debug.LogError("No instance of MenuScreenBlur found in scene");
        }

        // Hide the pause menu
        this.inner.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check for Escape being pressed
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!this.inner.activeSelf)
            {
                this.Show();
            }
            else
            {
                this.Hide();
            }
        }
    }

    // Shows the pause menu and freezes time
    private void Show()
    {
        this.screenBlur.Blur(this.blurAnimationDuration);
        this.inner.SetActive(true);
        Time.timeScale = 0f;
    }

    // Hides the pause menu and unfreezes time
    private void Hide()
    {
        this.screenBlur.Unblur(this.blurAnimationDuration);
        this.inner.SetActive(false);
        Time.timeScale = 1f;
    }

    // OnResume is called when the user clicks the "Resume" button
    public void OnResume()
    {
        this.Hide();
    }

    // OnQuit is called when the user clicks the "Quit" button
    public void OnQuit()
    {
        Application.Quit();
    }
}
