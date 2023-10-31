using FMOD;
using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] EventReference _footsteps;
    EventInstance footsteps;
    [SerializeField] float footstepInterval = 0.245f;
    [SerializeField] PlayerController playerController;
    float t;

    private void Awake()
    {
        if (!_footsteps.IsNull)
        {
            footsteps = RuntimeManager.CreateInstance(_footsteps);
            footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        }
    }
    private void FixedUpdate()
    {
        t += Time.deltaTime;
        if (t >= footstepInterval)
        {
            t = 0f;
            if (playerController.moveDirection != Vector2.zero)
            {
                PlayFootsteps();
            }
        }
        else if (playerController.moveDirection == Vector2.zero)
        {
            footsteps.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    public void PlayFootsteps()
    {
        if (footsteps.isValid())
        {
            RuntimeManager.AttachInstanceToGameObject(footsteps, transform);
            footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            GroundSwitch();
            footsteps.start();
        }
    }

    void GroundSwitch()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -Vector3.up);

        if (Physics.Raycast(ray, out hit, 1.0f, 1 << 10))
        {
            Collider surfaceRenderer = hit.collider;
            if (surfaceRenderer)
            {
                //UnityEngine.Debug.Log(surfaceRenderer.name);
                if (surfaceRenderer.CompareTag("Wood"))
                {
                    //UnityEngine.Debug.Log("Wood");
                    footsteps.setParameterByName("Footsteps", 0);
                }
                if (surfaceRenderer.CompareTag("Concrete"))
                {
                    //UnityEngine.Debug.Log("Concrete");
                    footsteps.setParameterByName("Footsteps", 1);
                }
                if (surfaceRenderer.CompareTag("Steel"))
                {
                    //UnityEngine.Debug.Log("Steel");
                    footsteps.setParameterByName("Footsteps", 2);
                }
            }
        }
    }
}
