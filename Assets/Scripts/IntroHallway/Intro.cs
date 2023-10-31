using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public bool doorOpen = false;
    public float openAngle;
    public float OpenSmooth = 5.0f;
    float originalX, originalY;
    [SerializeField] string nextScene = "Bedroom";
    [SerializeReference] GameObject door;
    void Start()
    {
        originalX = door.transform.localEulerAngles.x;
        originalY = door.transform.localEulerAngles.y;
        openAngle = door.transform.localEulerAngles.z + openAngle;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(End());
        }
    }

    IEnumerator End()
    {
        doorOpen = true;
        BedroomUtility.SetBadEnding(false);
        BedroomUtility.SetStage(1);
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(nextScene);
    }

    private void FixedUpdate()
    {
        if (doorOpen)
        {
            Quaternion targetRotation = Quaternion.Euler(originalX, originalY, openAngle);
            door.transform.localRotation = Quaternion.Slerp(door.transform.localRotation, targetRotation, OpenSmooth * Time.deltaTime);
        }
    }
}
