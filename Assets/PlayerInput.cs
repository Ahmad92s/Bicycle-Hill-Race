using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerManager playerManager;

    internal bool buildSpeed, jump;
    internal bool bendForward, bendBackwards;

    Touch touch;
    void Update()
    {
        //MobileControls();

        PC_Controls();
    }
    
    void PC_Controls()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            buildSpeed = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            buildSpeed = false;
            jump = true;
        }
    }

    void MobileControls()
    {
        if (Input.touchCount > 0)
        {
            buildSpeed = true;
        }
        else
        {
            buildSpeed = false;
            jump = true;
        }
    }
}
