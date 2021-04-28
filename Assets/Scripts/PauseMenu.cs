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
    private ControlsMenu controlsMenu;
    private bool disabled = false;

    // Start is called before the first frame update
    void Start()
    {
        // Obtain a reference to the MenuScreenBlur
        this.screenBlur = FindObjectOfType<MenuScreenBlur>();
        if (this.screenBlur == null)
        {
            Debug.LogError("No instance of MenuScreenBlur found in scene");
        }

        // Obtain a reference to the ControlsMenu
        this.controlsMenu = FindObjectOfType<ControlsMenu>();
        if (this.controlsMenu == null)
        {
            Debug.LogError("No instance of ControlsMenu found in scene");
        }

        // Hide the pause menu
        this.inner.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("PauseMenu::Update():" + this.disabled);
        if (this.disabled) return;

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

    // Used to disable the pause menu
    // to stop it from showing/hiding even when the Esc key is pressed
    public void Disable()
    {
        this.disabled = true;
    }

    // Used to re-enable the pause menu
    public void Enable()
    {
        this.disabled = false;
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

    // OnControls is called when the user clicks the "Controls" button
    public void OnControls()
    {
        // Hide the pause menu
        this.inner.SetActive(false);
        this.Disable();

        // Show the controls menu,
        // and pass a callback that is called when it is closed
        this.controlsMenu.Show(() =>
        {
            this.Enable();
            this.inner.SetActive(true);
        });
    }

    // OnQuit is called when the user clicks the "Quit" button
    public void OnQuit()
    {
        Application.Quit();
    }
}
