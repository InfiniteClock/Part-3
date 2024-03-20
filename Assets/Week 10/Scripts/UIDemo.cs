using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIdemo : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public SpriteRenderer spr;
    public Color start;
    public Color end;
    float interpolation;
    public void SliderValueHasChanged(Single value)         // float works too
    {
        interpolation = value;
    }
    private void Update()
    {
        spr.color = Color.Lerp(start, end, interpolation/60f);
    }

    public void DropdownSelectionHasChanged(Int32 value)    // int works too
    {
        Debug.Log(dropdown.options[value].text);
    }
}
