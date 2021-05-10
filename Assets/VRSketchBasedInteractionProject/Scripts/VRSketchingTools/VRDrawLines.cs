﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using VRSketchingGeometry;
using VRSketchingGeometry.Commands;
using VRSketchingGeometry.Commands.Line;
using VRSketchingGeometry.SketchObjectManagement;

public class VRDrawLines : MonoBehaviour
{
    // Controller settings
    public SteamVR_Input_Sources handType; // Left or right hand
    public SteamVR_Action_Boolean actionDrawLines; // Action set (set controller button)

    public Transform movementSource; // Left or right hand transform
    private bool isPressed = false; // If controller key is pressed
    private bool isMoving = false; // If controller is movin

    private List<Vector3> positionsList = new List<Vector3>(); // List of positions of the sketch
    public float newPositionTresholdDistance = 0.025f; // Min distance between the last and new points

    // VRSketchGeometry Framework
    public DefaultReferences Defaults;
    private LineSketchObject currentLineSketchObject;
    public List<LineSketchObject> listOfLineSketchObjects = new List<LineSketchObject>();
    private float lineSketchObjectDiameter = 0.05f;
    private Color lineSketchObjectColor = Color.black;

    public VRSketchingToolManager ToolManager;
    public VRSketchBasedCommander Commander;

    void Start()
    {
        actionDrawLines.AddOnStateDownListener(TriggerDown, handType);
        actionDrawLines.AddOnStateUpListener(TríggerUp, handType);
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
            StartDrawLine();
        }

        // Ending the drawing
        else if (isMoving && !isPressed)
        {
            EndDrawLine();
        }

        // Updating the drawing
        else if (isMoving && isPressed)
        {
            UpdateDrawLine();
        }
    }

    void StartDrawLine()
    {
        isMoving = true;

        positionsList.Clear();
        positionsList.Add(movementSource.position);

        // Create LineSketchObject and store it in the list
        currentLineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab.GetComponent<LineSketchObject>());
        listOfLineSketchObjects.Add(currentLineSketchObject);

        // Set material color
        SetLineSketchObjectColor();
        currentLineSketchObject.GetComponent<Renderer>().material.SetColor("_Color", lineSketchObjectColor);

        // Set diameter
        SetLineSketchObjectDiameter();
        currentLineSketchObject.SetLineDiameter(lineSketchObjectDiameter);

        // Set first point of line
        currentLineSketchObject.AddControlPoint(movementSource.position);
    }

    void EndDrawLine()
    {
        Commander.Invoker.ExecuteCommand(new AddObjectToSketchWorldRootCommand(currentLineSketchObject, ToolManager.SketchWorld));
        isMoving = false;
    }

    void UpdateDrawLine()
    {
        Vector3 lastPosition = positionsList[positionsList.Count - 1];

        if (Vector3.Distance(movementSource.position, lastPosition) > newPositionTresholdDistance)
        {
            positionsList.Add(movementSource.position);
            // Invoker.ExecuteCommand(new AddControlPointCommand(currentLineSketchObject, movementSource.position));
            currentLineSketchObject.AddControlPoint(movementSource.position);
        }
    }

    public void SetLineSketchObjectDiameter()
    {
        lineSketchObjectDiameter = ToolManager.GetScale();
    }

    public void SetLineSketchObjectColor()
    {
        lineSketchObjectColor = ToolManager.GetColor();
    }

}
