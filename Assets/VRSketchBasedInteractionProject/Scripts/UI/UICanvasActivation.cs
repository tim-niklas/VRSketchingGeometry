using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvasActivation : MonoBehaviour
{
    public GameObject UICanvas;
    public GameObject ScaleMenu;
    public GameObject ColorMenu;

    public void SetUICanvasActiveOrInactive(bool status)
    {
        UICanvas.SetActive(status);
    }

    public void SetScaleMenuActiveOrInactive(bool status)
    {
        ScaleMenu.SetActive(status);
    }

    public void SetColorMenuActiveOrInactive(bool status)
    {
        ColorMenu.SetActive(status);
    }
}
