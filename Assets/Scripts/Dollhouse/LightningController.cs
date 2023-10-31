using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    public GameObject lights;
    public GameObject lightParent;
    public float _interval = 20f;
    public float _strikeLength = 3f;
    float _time;
    public bool isFlickering = false;
    public float timeDelay;

    private void Start()
    {
        _time = 0;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        while (_time >= _interval)
        {
            lightParent.SetActive(true);
            _time -= _interval;
            _interval = UnityEngine.Random.Range(15f, 30f);
            _strikeLength = UnityEngine.Random.Range(1f, 1.5f);
        }
        if (_time >= _strikeLength)
        {
            lightParent.SetActive(false);
        }
        if (!isFlickering)
        {
            StartCoroutine(TriggerLightning());
        }
    }

    IEnumerator TriggerLightning()
    {
        isFlickering = true;
        lights.SetActive(false);
        timeDelay = UnityEngine.Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(timeDelay);
        lights.SetActive(true);
        timeDelay = UnityEngine.Random.Range(0.3f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
