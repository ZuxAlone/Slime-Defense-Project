using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Slider slider;

    public void SetMaxHealth(int value) 
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void SetCurrentHealth(int value) 
    {
        slider.value = value;
    }
}
