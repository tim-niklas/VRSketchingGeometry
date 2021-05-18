using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRSketchingGeometry.Commands;

public class VRSketchBasedCommander : MonoBehaviour
{
    public CommandInvoker Invoker; // CommandeInkover responsbile for the whole scene
    public VRSketchingToolManager ToolManager;
    public UICanvasActivation CanvasActivation;

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
            case "ColorMenu":
                CanvasActivation.SetColorMenuActiveOrInactive();
                Debug.Log("ColorMenu");
                break;
            case "ScaleMenu":
                CanvasActivation.SetScaleMenuActiveOrInactive();
                Debug.Log("ScaleMenu");
                break;
            case "VRDrawLines":
                ToolManager.SetVRDrawLinesActive();
                Debug.Log("VRDrawLines");
                break;
            case "VRDrawRibbons":
                ToolManager.SetVRDrawRibbonsActive();
                Debug.Log("VRDrawRibbons");
                break;
            case "DeleteSketches":
                ToolManager.DeleteAllSketches();
                Debug.Log("DeleteSketches");
                break;
            case "LineScalePlus":
                ToolManager.IncreaseScale();
                Debug.Log("LineScalePlus");
                break;
            case "LineScaleMinus":
                ToolManager.DecreaseScale();
                Debug.Log("LineScaleMinus");
                break;
            case "ControlCommandsDisplay":
                Debug.Log("ControlCommandDisplay");
                break;
            default:
                Debug.Log("No command found");
                break;
        }
    }
}
