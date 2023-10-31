using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimCurveCheck : MonoBehaviour
{

    public SpiderController spider;
    float ogSpeed;

    private void Start()
    {
        ogSpeed = spider._speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Curve"))
        {
            spider._speed = 5f;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Curve"))
        {
            spider._speed = 5f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Curve"))
        {
            spider._speed = 20f;
        }
    }
}
