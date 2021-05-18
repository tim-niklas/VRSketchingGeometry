using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvasActivation : MonoBehaviour
{
    public GameObject UICanvas;
    public GameObject ScaleMenu;
    public GameObject ColorMenu;
    public GameObject HelpMenu;
    public GameObject CommandsInfomationDisplay;

    public void SetUICanvasActiveOrInactive(bool status)
    {
        UICanvas.SetActive(status);
    }

    public void SetScaleMenuActiveOrInactive()
    {
        SetUICanvasActiveOrInactive(true);

        if (ScaleMenu.activeSelf)
        {
            ScaleMenu.SetActive(false);
            SetUICanvasActiveOrInactive(false);
        }
        else
        {
            ScaleMenu.SetActive(true);
            SetUICanvasActiveOrInactive(true);

            ColorMenu.SetActive(false);
        }

    }

    public void SetColorMenuActiveOrInactive()
    {
        SetUICanvasActiveOrInactive(true);
        if (ColorMenu.activeSelf)
        {
            ColorMenu.SetActive(false);
            SetUICanvasActiveOrInactive(false);
        }
        else
        {
            ColorMenu.SetActive(true);
            SetUICanvasActiveOrInactive(true);

            ScaleMenu.SetActive(false);
        }
    }

}
