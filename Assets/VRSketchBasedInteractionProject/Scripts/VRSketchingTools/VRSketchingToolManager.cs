using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRSketchingGeometry;
using VRSketchingGeometry.SketchObjectManagement;

public class VRSketchingToolManager : MonoBehaviour
{
    public Color VRSketchingToolColor = Color.black; // color of all sketch tools
    public float VRSketchingToolScale = 0.05f; // Scale of all sketch tools

    public SketchWorld SketchWorld; // SketchWorld of scene
    public DefaultReferences Defaults;

    public GameObject[] VRSketchingTools;

    public void Start()
    {
        // Create a SketchWorld, many commands require a SketchWorld to be present
        SketchWorld = Instantiate(Defaults.SketchWorldPrefab).GetComponent<SketchWorld>();
    }

    public void SetToolActiveOrInactive(bool status, string VRSketchingToolTag, string VRAttachmentTag)
    {
        VRSketchingTools = GameObject.FindGameObjectsWithTag(VRSketchingToolTag);

        foreach (GameObject VRSketchingTool in VRSketchingTools)
        {
            VRSketchingTool.SetActive(status);
        }

        VRSketchingTools = GameObject.FindGameObjectsWithTag(VRAttachmentTag);

        foreach (GameObject VRSketchingTool in VRSketchingTools)
        {
            VRSketchingTool.SetActive(status);
        }
    }

    public void SetVRDrawLinesActive()
    {
        SetToolActiveOrInactive(true, "VRDrawLinesTool", "VRDrawLinesAttachment");
        SetToolActiveOrInactive(false, "VRDrawRibbonsTool", "VRDrawRibbonsAttachment");
    }

    public void SetVRDrawRibbonsActive()
    {
        SetToolActiveOrInactive(false, "VRDrawLinesTool", "VRDrawLinesAttachment");
        SetToolActiveOrInactive(true, "VRDrawRibbonsTool", "VRDrawRibbonsAttachment");
    }

    public void SetColorOfToolAttachment(string VRAttachmentTag)
    {
        VRSketchingTools = GameObject.FindGameObjectsWithTag(VRAttachmentTag);

        foreach (GameObject VRSketchingTool in VRSketchingTools)
        {
            VRSketchingTool.GetComponent<Renderer>().material.SetColor("_Color", VRSketchingToolColor);
        }
    }

    public void SetScaleOfToolAttachment(string VRAttachmentModelTag)
    {
        VRSketchingTools = GameObject.FindGameObjectsWithTag(VRAttachmentModelTag);

        foreach (GameObject VRSketchingTool in VRSketchingTools)
        {
            VRSketchingTool.transform.localScale = new Vector3(VRSketchingToolScale, VRSketchingToolScale, VRSketchingToolScale);
        }
    }

    public void SetScaleAndColorOfToolAttachment(string VRAttachmentTag, string VRAttachmentModelTag)
    {
        SetScaleOfToolAttachment(VRAttachmentTag);
        SetColorOfToolAttachment(VRAttachmentModelTag);

    }

    public void SetAllToolsAttachmentApperances()
    {
        SetScaleAndColorOfToolAttachment("VRDrawLinesAttachment", "VRDrawLinesAttachmentModel");
        SetScaleAndColorOfToolAttachment("VRDrawRibbonsAttachment", "VRDrawRibbonsAttachmentModel");
    }

    public void SetColor(Color color)
    {
        VRSketchingToolColor = color;
        SetAllToolsAttachmentApperances();
    }

    public Color GetColor()
    {
        return VRSketchingToolColor;
    }

    public void SetScale(float scale)
    {
        VRSketchingToolScale = scale;
        SetAllToolsAttachmentApperances();
    }

    public float GetScale()
    {
        return VRSketchingToolScale;
    }

}
