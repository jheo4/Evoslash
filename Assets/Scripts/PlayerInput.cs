using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Movement Inputs
    public string moveAxisName = "Vertical";
    public string rotateAxisName = "Horizontal";
    public string jumpButtonName = "Jump";

    // Weapon Inputs
    public string fireButtonName = "Fire1";
    public string reloadButtonName = "Reload"; // gun only

    // Input Key States
    public float move { get; private set; }
    public float rotate { get; private set; }
    public bool jump { get; private set; }
    public bool fire { get; private set; }
    public bool reload { get; private set; }


    // Update is called once per frame
    private void Update()
    {
        if(GameManager.instance != null && GameManager.instance.isEnd) {
            move = 0;
            rotate = 0;
            jump= false;
            fire = false;
            reload = false;

            return;
        }

        move = Input.GetAxis(moveAxisName);
        rotate = Input.GetAxis(rotateAxisName);
        jump = Input.GetButton(jumpButtonName);
        fire = Input.GetButton(fireButtonName);
        reload = Input.GetButtonDown(reloadButtonName);
    }
}
