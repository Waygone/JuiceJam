using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHp(int maxHp)
    {
        slider.maxValue = maxHp;
        slider.value = maxHp;
    }

    public void SetHealth(int newHp)
    {
        slider.value = newHp; 
    }
}
