using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using PDollarGestureRecognizer;
using System.IO;
using UnityEngine.Events;

public class VRSketchRecognizer : MonoBehaviour
{
    // Controller settings
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean actionRecognizeSketch;


    public Transform movementSource; // Left or right hand transform
    private bool isPressed = false; // If controller key is pressed
    private bool isMoving = false; // If controller is moving

    // Draw settings
    public GameObject debugPrefab; // Debug prefab model 
    LineRenderer lineRenderer; // lineRenderer draw lines accrding to the given positions
    public Gradient lineRendererGradientNeutral;
    public Gradient lineRendererGradientFalse;

    // Recogntion settings
    public float newPositionTresholdDistance = 0.025f; // Min distance between the last and new points
    public float recognitionTreshold = 0.80f; // Min value of the recogntion result score

    private List<Gesture> trainingSet = new List<Gesture>(); // Training set of gestures
    private List<Vector3> positionsList = new List<Vector3>(); // List of positions of the sketch

    // Creation settings
    public bool creationMode = false; // Activate creation mode
    public string newGestureName; // Name of the new created sketch gesture

    // Event system for calling the sketch based controller
    [System.Serializable]
    public class UnityEventSketchRecognized : UnityEvent<string, float> { }
    public UnityEventSketchRecognized OnRecognized;

    // Start is called before the first frame update
    void Start()
    {
        actionRecognizeSketch.AddOnStateDownListener(GripButtonDown, handType);
        actionRecognizeSketch.AddOnStateUpListener(GripButtonUp, handType);

        string[] gesturesFiles = Directory.GetFiles(Application.streamingAssetsPath + "/TrainingSet", "*.xml");
        foreach (var item in gesturesFiles)
        {
            trainingSet.Add(GestureIO.ReadGestureFromFile(item));
        }

        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }


    public void GripButtonDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        isPressed = true;

    }

    public void GripButtonUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        isPressed = false;

    }

    // Update is called once per frame
    void Update()
    {
        // Start the movement
        if (!isMoving && isPressed)
        {
            StartMovement();
        }
        // Ending the movement
        else if (isMoving && !isPressed)
        {
            EndMovement();
        }
        // Updating the movement
        else if (isMoving && isPressed)
        {
            UpdateMovement();
        }
    }

    void StartMovement()
    {
        isMoving = true;

        positionsList.Clear();
        positionsList.Add(movementSource.position);

        lineRenderer.positionCount = 0;
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(0, movementSource.position);
        lineRenderer.colorGradient = lineRendererGradientNeutral;

        // if (debugPrefab)
        //     Destroy(Instantiate(debugPrefab, movementSource.position, Quaternion.identity), 3);

    }

    void EndMovement()
    {
        isMoving = false;

        // Creates the Gesture (Sketch) from the positionsList
        Point[] pointArray = new Point[positionsList.Count];

        for (int i = 0; i < positionsList.Count; i++)
        {
            // Debug.Log(positionsList[i]);
            // Debug.Log(Camera.main.WorldToScreenPoint(positionsList[i]));

            Vector2 screenPoint = Camera.main.WorldToScreenPoint(positionsList[i]);
            pointArray[i] = new Point(screenPoint.x, screenPoint.y, 0);

        }

        Gesture newGesture = new Gesture(pointArray);

        if (creationMode)
        {

            newGesture.Name = newGestureName;
            trainingSet.Add(newGesture);

            string fileName = Application.streamingAssetsPath + "/TrainingSet/" + newGestureName + DateTime.Now.ToFileTime().ToString() + ".xml";
            GestureIO.WriteGesture(pointArray, newGestureName, fileName);
        }

        else
        {
            Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());

            Debug.Log(" GestureClass " + result.GestureClass + " GestureScore " + result.Score);

            if (result.Score > recognitionTreshold)
            {
                OnRecognized.Invoke(result.GestureClass, result.Score);

                lineRenderer.positionCount = 0;
            }
            else
            {
                StartCoroutine(FalseRecognized());
            }
        }
    }

    void UpdateMovement()
    {
        Vector3 lastPosition = positionsList[positionsList.Count - 1];

        if (Vector3.Distance(movementSource.position, lastPosition) > newPositionTresholdDistance)
        {
            positionsList.Add(movementSource.position);

            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, movementSource.position);

            //  if (debugPrefab)
            //      Destroy(Instantiate(debugPrefab, movementSource.position, Quaternion.identity), 3);
        }
    }

    private IEnumerator FalseRecognized()
    {
        lineRenderer.colorGradient = lineRendererGradientFalse;
        yield return new WaitForSeconds(0.5f);
        lineRenderer.positionCount = 0;
    }
}
