using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour {
    [SerializeField] Slider Hue;
    [SerializeField] Slider Saturation;
    [SerializeField] Slider Value;
    [SerializeField] Material colorPickerMat;
    [SerializeField] Material birdMat; //i really should compartmentalize this but it's 12:34

    public Color color;
    public void Start()
    {
        ChangedSliders(); //just to initilize the values
    }
    public void ChangedSliders() //should break this up but it's 12:18 and assignment is due today and i still havn't finished my custom renderer
    {
       colorPickerMat.SetFloat("_Hue", Hue.value);
       colorPickerMat.SetFloat("_Saturation", Saturation.value);
       colorPickerMat.SetFloat("_Value", Value.value);
        print("iwork");
    }
    //these functions are from http://www.chilliant.com/rgb2hsv.html still don't know the math for this lmao, addapted to C#
    Vector3 HUEtoRGB(float H)
    {
        return new Vector3(
            Mathf.Clamp(Mathf.Abs(H * 6 - 3) - 1,0,1),//R
            Mathf.Clamp(2 - Mathf.Abs(H * 6 - 2),0,1),//G
            Mathf.Clamp(2 - Mathf.Abs(H * 6 - 4),0,1) //B
        );
    }
    Vector3 HSVtoRGB(Vector3 HSV)
    {
        Vector3 RGB = HUEtoRGB(HSV.x);
        return ((RGB - new Vector3(1,1,1)) * (HSV.y + 1.0f)) * HSV.z;
    }
}
