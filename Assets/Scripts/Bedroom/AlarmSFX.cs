using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSFX : MonoBehaviour
{

    [SerializeField] StudioEventEmitter emitter;
    float a;
    float n;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            emitter.OverrideMaxDistance = 15f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            emitter.OverrideMaxDistance = 0f;
        }
    }
}
