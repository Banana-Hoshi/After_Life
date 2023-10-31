using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventController : MonoBehaviour
{
    [SerializeField] GameObject mindman;
    bool mindmanEnabled, footstepEnabled;
    [SerializeField] GameObject footstepEvent, footstepTarget;
    [SerializeField] GameObject mosquitoEvent;
    [SerializeField] DoorControllerRotate doorEvent;

    private void Start()
    {
        if ((BedroomUtility.GetStage() == 1 && BedroomUtility.GetAttempts() > 0) || BedroomUtility.GetStage() > 1)
        {
            int n = Random.Range(0, 10);
            if (n == 3)
            {
                StartCoroutine(doorEvent.RandomDoorSound());
            }
            else if (n == 6)
            {
                mindmanEnabled = true;
            }
            else if (n == 9)
            {
                footstepEnabled = true;
            }
            else if (n == 1)
            {
                mosquitoEvent.SetActive(true);
            }
        }
    }
    private void Update()
    {
        if (mindmanEnabled)
        {
            mindman.SetActive(true);
            StartCoroutine(StartMindmanEvent());
        }
        else if (footstepEnabled)
        {
            footstepEvent.SetActive(true);
            StartFootstepEvent();
        }
    }

    IEnumerator StartMindmanEvent()
    {
        mindman.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        mindman.GetComponentInChildren<Animator>().SetBool("hide", true);
        yield return new WaitForSeconds(2.5f);
        mindman.SetActive(false);
        mindmanEnabled = false;
    }

    void StartFootstepEvent()
    {
        footstepEvent.SetActive(true);
        footstepEvent.transform.localPosition = Vector3.MoveTowards(footstepEvent.transform.localPosition, footstepTarget.transform.localPosition, Time.deltaTime * 3f);
        if (footstepEvent.transform.localPosition == footstepTarget.transform.localPosition)
        {
            footstepEnabled = false;
            footstepEvent.SetActive(false);
        }
    }
}
