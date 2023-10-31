using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeartbeat : MonoBehaviour
{
    [SerializeField] TimMovement tim;
    [SerializeField] EventReference _heartBeat;
    [SerializeField] float minHeartSpeed, maxHeartSpeed;
    [SerializeField] Transform playerTrans;

    EventInstance heartBeat;

    private void Awake()
    {
        if (!_heartBeat.IsNull)
        {
            heartBeat = RuntimeManager.CreateInstance(_heartBeat);
        }
    }

    private void Start()
    {
        if (heartBeat.isValid())
        {
            RuntimeManager.AttachInstanceToGameObject(heartBeat, transform);

            heartBeat.start();
        }
    }

    private void Update()
    {
        heartBeat.getParameterByName("BeatSpeed", out float n);
        /*
        float dist = Vector3.Distance(playerTrans.position, tim.transform.position);
        dist = maxHeartSpeed * dist;
        if (dist < minHeartSpeed) dist = minHeartSpeed;
        heartBeat.setParameterByName("BeatSpeed", Mathf.MoveTowards(n, maxHeartSpeed, Time.deltaTime * 2.5f)); */
  

    if (tim.chasing)
        {
            heartBeat.setParameterByName("BeatSpeed", Mathf.MoveTowards(n, maxHeartSpeed, Time.deltaTime * 2.5f));
        }
        else
        {
            heartBeat.setParameterByName("BeatSpeed", Mathf.MoveTowards(n, minHeartSpeed, Time.deltaTime * 2.5f));
        }
    }
}
