﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRSketchingGeometry.Commands;

public class VRSketchBasedCommander : MonoBehaviour
{
    public CommandInvoker Invoker; // CommandeInkover responsbile for the whole scene

    public void Start()
    {
        Invoker = new CommandInvoker(); // Initialize CommandInvoker
    }

    // Call the specific command according to the recognized gesture (sketch)
    public void CallCommand(string sketchName, float score)
    {
        switch (sketchName)
        {
            case "Redo":
                Invoker.Redo();
                Debug.Log("Redo");
                break;

            case "Undo":
                Invoker.Undo();
                Debug.Log("Undo");
                break;

            default:
                Debug.Log("No command found");
                break;
        }
    }
}
