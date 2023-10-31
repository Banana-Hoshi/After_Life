using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KimAudio : MonoBehaviour
{
    [SerializeField] EventReference kimVoice;
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
            RuntimeManager.PlayOneShotAttached(kimVoice, gameObject);
        }
    }
}
