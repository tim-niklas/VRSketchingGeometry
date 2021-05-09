using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using VRSketchingGeometry;
using VRSketchingGeometry.Commands;
using VRSketchingGeometry.Commands.Ribbon;
using VRSketchingGeometry.SketchObjectManagement;

public class VRDrawRibbons : MonoBehaviour
{
    // Controller settings
    public SteamVR_Input_Sources handType; // Left or right hand
    public SteamVR_Action_Boolean actionDrawRibbons; // Action set (set controller button))

    public Transform movementSource; // Left or right hand transform
    private bool isPressed = false; // If controller key is pressed
    private bool isMoving = false; // If controller is movin

    private List<Vector3> positionsList = new List<Vector3>(); // List of positions of the sketch
    public float newPositionTresholdDistance = 0.025f; // Min distance between the last and new points

    // VRSketchGeometry Framework
    private CommandInvoker Invoker;
    public DefaultReferences Defaults;
    public SketchWorld SketchWorld;

    public GameObject Commander;

    public RibbonSketchObject currentRibbonSketchObject;
    public List<RibbonSketchObject> listOfRibbonSketchObjects = new List<RibbonSketchObject>();
    public float ribbonSketchObjectScale = 0.05f;
    public Color ribbonSketchObjectColor = Color.black;

    public GameObject ToolManagerRibbon;


    void Start()
    {
        actionDrawRibbons.AddOnStateDownListener(TriggerDown, handType);
        actionDrawRibbons.AddOnStateUpListener(TríggerUp, handType);

        // Create a SketchWorld, many commands require a SketchWorld to be present
        SketchWorld = Instantiate(Defaults.SketchWorldPrefab).GetComponent<SketchWorld>();

        // Invoker = new CommandInvoker();
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        isPressed = true;
    }

    public void TríggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        isPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Start the drawing
        if (!isMoving && isPressed)
        {
            StartDrawRibbon();
        }

        // Ending the drawing
        else if (isMoving && !isPressed)
        {
            EndDrawRibbon();
        }

        // Updating the drawing
        else if (isMoving && isPressed)
        {
            UpdateDrawRibbon();
        }
    }

    void StartDrawRibbon()
    {
        isMoving = true;

        positionsList.Clear();
        positionsList.Add(movementSource.position);

        // Create RibbonSketchObject and store it in the list
        currentRibbonSketchObject = Instantiate(Defaults.RibbonSketchObjectPrefab).GetComponent<RibbonSketchObject>();
        listOfRibbonSketchObjects.Add(currentRibbonSketchObject);

        // Set material color
        //SetRibbonSketchObjectColor();
        currentRibbonSketchObject.GetComponent<Renderer>().material.SetColor("_Color", ribbonSketchObjectColor);

        // Set width
        //SetRibbonSketchObjectScale();
        currentRibbonSketchObject.SetRibbonScale(Vector3.one * ribbonSketchObjectScale);

        // Set first point of ribbon
        // Invoker.ExecuteCommand(new AddPointAndRotationCommand(currentRibbonSketchObject, movementSource.position, movementSource.rotation));
        currentRibbonSketchObject.AddControlPoint(movementSource.position, movementSource.rotation);

    }

    void EndDrawRibbon()
    {
        Commander.GetComponent<SketchBasedCommands>().Invoker.ExecuteCommand(new AddObjectToSketchWorldRootCommand(currentRibbonSketchObject, SketchWorld));
        isMoving = false;
    }

    void UpdateDrawRibbon()
    {
        Vector3 lastPosition = positionsList[positionsList.Count - 1];

        if (Vector3.Distance(movementSource.position, lastPosition) > newPositionTresholdDistance)
        {
            positionsList.Add(movementSource.position);
            // Invoker.ExecuteCommand(new AddPointAndRotationCommand(currentRibbonSketchObject, movementSource.position, movementSource.rotation));
            currentRibbonSketchObject.AddControlPoint(movementSource.position, movementSource.rotation);
        }
    }

    public void SetRibbonSketchObjectScale()
    {
        ribbonSketchObjectScale = ToolManagerRibbon.GetComponent<VRSketchingToolManager>().scale;
    }

    public void SetRibbonSketchObjectColor()
    {
        ribbonSketchObjectColor = ToolManagerRibbon.GetComponent<VRSketchingToolManager>().color;
    }
}

