using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydroLight : MonoBehaviour
{

    public GameObject lights, bulb;
    public float timeDelay;
    bool isFlickering;

    private void Update()
    {
        if (!isFlickering)
        {
            StartCoroutine(FlickerLights());
        }
    }

    IEnumerator FlickerLights()
    {
        isFlickering = true;
        lights.SetActive(false);
        if (bulb != null) bulb.SetActive(false);
        timeDelay = UnityEngine.Random.Range(0.05f, 0.1f);
        yield return new WaitForSeconds(timeDelay);
        lights.SetActive(true);
        if(bulb != null) bulb.SetActive(true);
        timeDelay = UnityEngine.Random.Range(0.15f, 0.3f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
