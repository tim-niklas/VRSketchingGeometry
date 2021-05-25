using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRSketchingGeometry.Commands;
using TMPro;

public class VRSketchBasedCommander : MonoBehaviour
{
    public CommandInvoker Invoker; // CommandeInkover responsbile for the whole scene
    public VRSketchingToolManager ToolManager;
    public UICanvasActivation UIManager;

    public TMP_Text sketchTextMesh;
    public GameObject sketchTextCanvas;

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
                SetTextController("REDO");
                Debug.Log("REDO");
                break;
            case "Undo":
                Invoker.Undo();
                Debug.Log("UNDO");
                SetTextController("UNDO");
                break;
            case "ColorMenu":
                UIManager.SetColorMenuActiveOrInactive();
                Debug.Log("COLOR MENU");
                SetTextController("COLOR MENU");
                break;
            case "ScaleMenu":
                UIManager.SetScaleMenuActiveOrInactive();
                Debug.Log("SCALE MENU");
                SetTextController("SCALE MENU");
                break;
            case "ToolboxMenu":
                UIManager.SetToolBoxMenuActiveOrInactive();
                Debug.Log("TOOLBOX MENU");
                SetTextController("TOOLBOX MENU");
                break;
            case "LineTool":
                ToolManager.SetVRDrawLinesActive();
                Debug.Log("LINE TOOL");
                SetTextController("LINE TOOL");
                break;
            case "RibbonTool":
                ToolManager.SetVRDrawRibbonsActive();
                Debug.Log("RIBBON TOOL");
                SetTextController("RIBBON TOOL");
                break;
            case "DeleteSketch":
                ToolManager.DeleteSketchWorld();
                Debug.Log("DELETE SKETCH");
                SetTextController("DELETE SKETCH");
                break;
            case "ScaleIncrease":
                ToolManager.IncreaseScale();
                SetTextController("SCALE +");
                Debug.Log("SCALE +");
                break;
            case "ScaleDecrease":
                ToolManager.DecreaseScale();
                SetTextController("SCALE -");
                Debug.Log("SCALE -");
                break;
            case "HelpMenu":
                UIManager.SetCommandsInformationDisplayActiveOrInactive();
                Debug.Log("HELP MENU");
                SetTextController("HELP MENU");
                break;
            case "CloseUI":
                UIManager.CloseUI();
                Debug.Log("CLOSE UI");
                SetTextController("CLOSE UI");
                break;
            default:
                Debug.Log("NO COMMAND");
                SetTextController("NO COMMAND");
                break;
        }

        StartCoroutine(TextActivation());
    }

    public void UndoSketch()
    {
        Invoker.Undo();
    }

    public void RedoSketch()
    {
        Invoker.Redo();
    }

    public void SetTextController(string sketchName)
    {
        sketchTextMesh.text = sketchName;
    }
    private IEnumerator TextActivation()
    {
        sketchTextCanvas.SetActive(true);
        yield return new WaitForSeconds(0.9f);
        sketchTextCanvas.SetActive(false);
    }
}
