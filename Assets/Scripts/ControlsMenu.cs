using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
    // Track whether the controls have been shown during the game process's lifetime
    public static bool hasShownControls = false;

    // Public instance fields
    public float blurAnimationDuration = 0.125f;
    public GameObject inner;

    // Private instance fields
    private bool isShown = false;
    private Action closeCallback = null;
    private MouseHiding mouseHiding;

    // Start is called before the first frame update
    void Start()
    {
        // Obtain a reference to the MouseHiding
        this.mouseHiding = FindObjectOfType<MouseHiding>();
        if (this.mouseHiding == null)
        {
            Debug.LogError("No instance of MouseHiding found in scene");
        }

        // If the controls haven't been shown,
        // then show them and blur the screen/pause time
        if (!ControlsMenu.hasShownControls)
        {
            ControlsMenu.hasShownControls = true;
            StartCoroutine(this.ShowInitialControls());
        }
    }

    // ShowInitialControls is a coroutine
    // that performs a delayed showing of the controls menu
    IEnumerator ShowInitialControls()
    {
        // Wait a small delay
        yield return new WaitForSeconds(0.25f);

        // Obtain a reference to the MenuScreenBlur and pause UI
        MenuScreenBlur screenBlur = FindObjectOfType<MenuScreenBlur>();
        if (screenBlur == null)
        {
            Debug.LogError("No instance of MenuScreenBlur found in scene; skipping showing controls");
        }
        else
        {
            PauseMenu pauseMenu = FindObjectOfType<PauseMenu>();
            if (pauseMenu != null) pauseMenu.Disable();

            screenBlur.Blur(this.blurAnimationDuration);
            Time.timeScale = 0f;
            this.Show(() =>
            {
                screenBlur.Unblur(this.blurAnimationDuration);
                Time.timeScale = 1f;
                if (pauseMenu != null) pauseMenu.Enable();
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isShown) return;

        // Check for Escape being pressed
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            this.Close();
        }
    }

    // OnClose is called when the user clicks the "Close" button
    public void OnClose() {
        this.Close();
    }

    // Close is called when the controls menu should be hidden.
    // It sets the game object to inactive and invokes the callback.
    private void Close()
    {
        if (!this.isShown) return;

        this.mouseHiding.OnGuiClose();
        this.inner.SetActive(false);
        this.isShown = false;
        this.closeCallback();
    }

    // Show is called when the controls menu should appear as a modal dialog.
    // This does not blur the screen or pause the time,
    // but it does include a dark screen overlay
    public void Show(Action onHide)
    {
        if (this.isShown) return;

        this.mouseHiding.OnGuiOpen();
        this.inner.SetActive(true);
        this.isShown = true;
        this.closeCallback = onHide;
    }
}
