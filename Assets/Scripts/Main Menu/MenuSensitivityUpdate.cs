using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class MenuSensitivityUpdate : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] bool isMainMenu;
    Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = SensitivityAdjustment.xSens;
    }
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = SensitivityAdjustment.xSens;
    }
    public void UpdateSens()
    {
        float sens = slider.value;
        SensitivityAdjustment.xSens = sens;
        SensitivityAdjustment.ySens = sens;
        text.text = (Mathf.Round(sens * 5f * 10f) / 100).ToString();
    }
    public void UpdateXSens()
    {
        float sens = slider.value;
        SensitivityAdjustment.xSens = sens;
        text.text = (Mathf.Round(sens * 5f * 10f) / 100).ToString();
    }
    public void UpdateYSens()
    {
        float sens = slider.value;
        SensitivityAdjustment.ySens = sens;
        text.text = (Mathf.Round(sens * 5f * 10f) / 100).ToString();
    }

}
