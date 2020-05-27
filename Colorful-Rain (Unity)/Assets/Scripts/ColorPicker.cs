using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    public FlexibleColorPicker b1_ColorPicker;
    public FlexibleColorPicker b2_ColorPicker;
    public Material bucket1;
    public Material bucket2;
    void Update()
    {
        bucket1.color = b1_ColorPicker.color;
        bucket2.color = b2_ColorPicker.color;
    }
}
