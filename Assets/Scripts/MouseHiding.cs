using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHiding : MonoBehaviour
{
    // Private instance variables
    private int guiOpenCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.HideCursor();
    }

    // ShowCursor is an internal function to hide the cursor
    // (for use when in-game)
    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // ShowCursor is an internal function to show the cursor
    // (for use when in a GUI)
    private void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // OnGuiClose is called whenever a modal GUI is opened
    public void OnGuiOpen()
    {
        if (this.guiOpenCount == 0)
        {
            this.ShowCursor();
        }
        this.guiOpenCount++;
    }

    // OnGuiClose is called whenever an open GUI is closed
    public void OnGuiClose()
    {
        this.guiOpenCount--;
        if (this.guiOpenCount == 0)
        {
            this.HideCursor();
        }
    }
}
