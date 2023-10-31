using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FinalLevelMusicController : MonoBehaviour
{
    [SerializeField] GameObject jim, tim, kim, endLayer;
    public bool ending;
    [SerializeField] StudioEventEmitter layer1, layer2, layer3, heartbeat, atmo;

    private void Start()
    {
        layer1.SetParameter("vol", 0);
        layer2.SetParameter("vol", 0);
        layer3.SetParameter("vol", 0);
    }

    private void Update()
    {
        if (jim.activeSelf)
        {
            layer1.SetParameter("vol", Mathf.Lerp(1f, 0f, Time.deltaTime));
        }
        if (tim.activeSelf)
        {
            layer2.SetParameter("vol", Mathf.Lerp(1f, 0f, Time.deltaTime));
        }
        if (kim.activeSelf)
        {
            layer3.SetParameter("vol", Mathf.Lerp(1f, 0f, Time.deltaTime));
        }
        if (ending)
        {
            layer1.Stop();
            layer2.Stop();
            layer3.Stop();
            heartbeat.Stop();
            atmo.Stop();
        }
    }


}
