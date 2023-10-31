using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollAudio : MonoBehaviour
{
    [SerializeField] EventReference DollGiggle;
    [SerializeField] GameObject[] dolls;
    [SerializeField] float minTime, maxTime;
    float t = 0;
    float randT = 0;

    private void Start()
    {
        randT = UnityEngine.Random.Range(minTime, maxTime);
    }

    private void Update()
    {
        t += Time.deltaTime;
        if (t >= randT)
        {
            randT = UnityEngine.Random.Range(minTime, maxTime);
            t = 0;
            RuntimeManager.PlayOneShotAttached(DollGiggle, dolls[UnityEngine.Random.Range(0, dolls.Length)]);
        }
    }
}
