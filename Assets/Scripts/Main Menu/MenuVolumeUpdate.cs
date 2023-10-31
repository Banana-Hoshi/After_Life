using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using FMODUnity;
using FMOD.Studio;

public class MenuVolumeUpdate : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] Bus bus;
    float a;
    Slider slider;
    private void Start()
    {
        bus = RuntimeManager.GetBus("bus:/");
        slider = GetComponent<Slider>();
        bus.getVolume(out a);
        slider.value = a;
    }
    private void Awake()
    {
        slider = GetComponent<Slider>();
        bus.getVolume(out a);
        slider.value = a;
    }
    public void UpdateSens()
    {
        a = slider.value;
        bus.setVolume(a);
        text.text = (Mathf.Round(a * 100f)).ToString() + "%";
    }
}
