using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlashFade : MonoBehaviour
{

    public float fadeTime;
    public float timer;
    float originalLight;
    Light lightSource;

    float flashRange;
    float fadeScale, distanceScale, intensityScale;
    float colliderOffset, radiusRatio, distanceRatio;
    bool enemySpotted;

    private void Start()
    {
        lightSource = GetComponent<Light>();
        originalLight = lightSource.range;
    }

    private void Update()
    {
        if (lightSource.range > 0)
        {
            timer += Time.deltaTime;
            if (enemySpotted)
            {
                float liveScale = 1 - (timer / 1);
                lightSource.range = liveScale * originalLight;
                GetComponent<CapsuleCollider>().center = new Vector3(0, 0, (distanceRatio * flashRange * distanceScale * liveScale) / 2 + colliderOffset);
                GetComponent<CapsuleCollider>().radius = radiusRatio * flashRange * distanceScale * liveScale;
                GetComponent<CapsuleCollider>().height = distanceRatio * flashRange * distanceScale * liveScale;
            }
            else if (timer > fadeTime / 2)
            {
                float liveScale = (1 - ((timer - (fadeTime / 2)) / (fadeTime / 2)));
                lightSource.range = liveScale * originalLight;
                GetComponent<CapsuleCollider>().center = new Vector3(0, 0, (distanceRatio * flashRange * distanceScale * liveScale) / 2 + colliderOffset);
                GetComponent<CapsuleCollider>().radius = radiusRatio * flashRange * distanceScale * liveScale;
                GetComponent<CapsuleCollider>().height = distanceRatio * flashRange * distanceScale * liveScale;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ImportScale(float fade, float distance, float intensity, float colliderOffset, float radiusRatio, float distanceRatio, float flashRange)
    {
        fadeScale = fade;
        distanceScale = distance;
        intensityScale = intensity;
        this.colliderOffset = colliderOffset;
        this.radiusRatio = radiusRatio;
        this.distanceRatio = distanceRatio;
        this.flashRange = flashRange;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody && other.attachedRigidbody.CompareTag("Monster"))
        {
            SpiderController jimController = other.transform.parent.GetComponent<SpiderController>();
            enemySpotted = true;
            jimController.TriggerRunaway();
            if (jimController.timesFlashed >= 3)
            {
                jimController.enabled = false;
                jimController.transform.GetComponentInChildren<SpiderProceduralAnimation>().enabled = false;
                Destroy(jimController.transform.gameObject, 1);
            }
            jimController.timesFlashed++;
            originalLight = lightSource.range;
            enemySpotted = true;
            timer = 0;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tim"))
        {
            TimMovement timController = other.transform.GetComponent<TimMovement>();
            timController.Freeze(1);
        }
    }
}
