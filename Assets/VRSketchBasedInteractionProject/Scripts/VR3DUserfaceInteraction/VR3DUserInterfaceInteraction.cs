﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VR3DUserInterfaceInteraction : MonoBehaviour
{
    public SteamVR_Input_Sources handType; // Left or right hand
    public SteamVR_Action_Boolean action3DUserInterface; // Action set (select key input of controller)

    public GameObject UICanvas;

    // If controller grip button is pressed
    public void GripButtonUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (UICanvas.activeSelf)
        {
            UICanvas.SetActive(false);
        }
        else
        {
            UICanvas.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set controller input listener
        action3DUserInterface.AddOnStateUpListener(GripButtonUp, handType);
    }
}
