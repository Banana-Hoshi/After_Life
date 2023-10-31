using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockSound : MonoBehaviour
{
    [SerializeField] EventReference _clockSound;
    public EventInstance clockSound;
    public bool close;
    float t = 0;

    private void Awake()
    {
        if (!_clockSound.IsNull)
        {
            clockSound = RuntimeManager.CreateInstance(_clockSound);
        }
    }

    private void Start()
    {
        if (clockSound.isValid())
        {
            RuntimeManager.AttachInstanceToGameObject(clockSound, transform);
            clockSound.setParameterByName("GearState", 0);
            clockSound.start();
        }
    }

    private void Update()
    {
        t += Time.deltaTime;
        if (t >= 1.044f && !close)
        {
            clockSound.setParameterByName("GearState", 1);
        }
        if (close)
        {
            clockSound.setParameterByName("GearState", 2);
            clockSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
