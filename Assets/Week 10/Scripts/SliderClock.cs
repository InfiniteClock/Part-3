using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderClock : MonoBehaviour
{
    public Slider slider;
    float timer;
    public float speed = 1f;

    void Update()
    {
        timer += Time.deltaTime * speed;
        timer = timer % 60f;
        slider.value = timer;
    }
}
