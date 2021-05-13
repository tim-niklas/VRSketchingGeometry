using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorMenuController : MonoBehaviour
{
    public Slider RColorSlider;
    public Slider GColorSlider;
    public Slider BColorSlider;

    private Color SliderColor;

    public TMP_Text RSliderText;
    public TMP_Text GSliderText;
    public TMP_Text BSliderText;

    public Image ColorDisplayImage;

    public VRSketchingToolManager ToolManager;

    // Start is called before the first frame update
    void Start()
    {
   
        
    }

    public void ColorChange()
    {
        float colorRed = RColorSlider.value;
        float colorGreen = GColorSlider.value;
        float colorBlue = BColorSlider.value;
     
        SliderColor = new Color(colorRed, colorGreen, colorBlue, 1);
      
        RSliderText.SetText((colorRed * 255).ToString("0"));
        GSliderText.SetText((colorGreen * 255).ToString("0"));
        BSliderText.SetText((colorBlue * 255).ToString("0"));

        ColorDisplayImage.color = SliderColor;
        ToolManager.SetColor(SliderColor);

    }
}
