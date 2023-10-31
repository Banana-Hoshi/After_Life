
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;
using System;
using System.Diagnostics;
using UnityEngine.InputSystem;

public class DoorControllerRotate : Interactable
{
    public bool doorOpen = false;
    public bool canOpen = true;

    public float openAngle;
    public float closeAngle;
    public float doorOriginalAngle;
    public float OpenSmooth = 5.0f;
    public float CloseSmooth = 6.0f;
    public bool finalLevel;

    public Vector3 doorDir = Vector3.up;

    public EventReference doorSFX;
    public EventReference doorSFX2;
    LayerMask mask;

    public bool reproduced;
    public bool reproduced2;

    Transform PlayerCamera;
    GameObject door;
    public float MaxDistance = 3.5f;
    public float flip = 1;
    float originalX, originalY;

    void Start()
    {
        door = this.gameObject;
        mask = LayerMask.GetMask("Door");
        PlayerCamera = Camera.main.transform;
        originalX = door.transform.localEulerAngles.x;
        originalY = door.transform.localEulerAngles.y;
        closeAngle = door.transform.localEulerAngles.z;
        doorOriginalAngle = door.transform.localEulerAngles.z;
    }

    public override string GetDescription()
    {
        return "";
    }

    public override int GetSpeechBubble()
    {
        return 0;
    }

    public override void Interact()
    {
        if (canOpen)
        {

            RaycastHit doorhit;

            if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out doorhit, MaxDistance, mask))
            {
                if (doorhit.collider.gameObject.name == door.name)
                {
                    if (Vector3.Dot(door.transform.rotation * doorDir, (PlayerCamera.transform.position - door.transform.position)) < 0)
                    {
                        openAngle = (doorOriginalAngle - 90f) * flip;
                        print("first loop");
                    }
                    else
                    {
                        openAngle = (doorOriginalAngle + 90f) * flip;
                        print("second loop");
                    }
                    doorOpen = !doorOpen;
                    StartCoroutine(DisableCollider());
                    if (!reproduced)
                    {
                        RuntimeManager.PlayOneShotAttached(doorSFX, gameObject);
                        reproduced = true;
                        reproduced2 = false;
                    }

                    else if (!reproduced2)
                    {
                        RuntimeManager.PlayOneShotAttached(doorSFX2, gameObject);
                        reproduced2 = true;
                        reproduced = false;
                    }
                }
            }
        }
    }

    void Update()
    {

        if (canOpen)
        {

            if (doorOpen)
            {
                Quaternion targetRotation = Quaternion.Euler(originalX, originalY, openAngle);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, OpenSmooth * Time.deltaTime);
            }

            else
            {
                Quaternion targetRotation2 = Quaternion.Euler(originalX, originalY, closeAngle);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, CloseSmooth * Time.deltaTime);
            }
        }
    }

    public IEnumerator RandomDoorSound()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
        doorOpen = !doorOpen;
        RuntimeManager.PlayOneShotAttached(doorSFX, gameObject);
        reproduced = true;
        reproduced2 = false;
    }

    IEnumerator DisableCollider()
    {
        GetComponentInChildren<Collider>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        GetComponentInChildren<Collider>().enabled = true;
    }
}