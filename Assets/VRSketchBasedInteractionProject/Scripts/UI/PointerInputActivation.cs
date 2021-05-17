using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerInputActivation : MonoBehaviour
{

    public GameObject Pointer;
    public GameObject InputModule;
    public GameObject RightHand;
    public VRSketchingToolManager ToolManager;
    
    // Start is called before the first frame update
    void Start()
    {
        Pointer.SetActive(false);
        InputModule.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(RightHand.transform.position, RightHand.transform.forward);
        if (Physics.Raycast(ray, out hit))
        {
             if (hit.transform.tag == "UICanvas")
            {
                Pointer.SetActive(true);
                InputModule.SetActive(true);
                ToolManager.SetVRSketchingToolBoxActiveOrInactive(false);
             
            }
        }
        else
        {
            Pointer.SetActive(false);
            InputModule.SetActive(false);
            ToolManager.SetVRSketchingToolBoxActiveOrInactive(true);
        }
    }
}
