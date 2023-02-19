using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    public Slider slider;

    public void SetDashTimer(float maxTime)
    {
        slider.maxValue = maxTime;
        slider.value = maxTime;
    }

    public void SetValue(float newVal)
    {
        slider.value = newVal;
    }
}
