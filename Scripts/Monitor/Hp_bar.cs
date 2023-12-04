using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_bar : MonoBehaviour
{
    private Slider slider_bar;
    private void Start()
    {
        slider_bar = GetComponent<Slider>();
    }

    public void MaxLife(float maxLife)
    {
        slider_bar.maxValue = maxLife;
    }

    public void CurrentLife(float currentLife)
    {
        slider_bar.value = currentLife;
    }

    public void LifeStart(float life)
    {
        MaxLife(life);
        CurrentLife(life);
    }
}
