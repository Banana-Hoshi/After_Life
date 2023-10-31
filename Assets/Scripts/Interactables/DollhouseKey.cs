using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DollhouseKey : Interactable
{
    [SerializeField] TimMovement tim;
    [SerializeField] DoorControllerRotate door;
    [SerializeField] float searchTime;
    [SerializeField] GameObject[] speechbubble;
    [SerializeField] DoorDialogue doorDialogue;
    //[SerializeField] Volume vignette;
    string description = "Look through drawer?";
    [SerializeField] GameObject soundPlayer;
    int n;
    public bool hasKey;

    private void Start()
    {
        n = Random.Range(0, speechbubble.Length);
    }
    public override string GetDescription()
    {
        return description;
    }
    public override int GetSpeechBubble()
    {
        return n;
    }

    public override void Interact()
    {
        StartCoroutine("Rummage");
    }
    IEnumerator Rummage()
    {
        PlayerDisable.ToggledDisabled();
        CinemachineVirtualCamera cam = FindObjectOfType<CinemachineVirtualCamera>();
        float defaultFov = cam.m_Lens.FieldOfView;
        //vignette.GetComponent<Vignette>().intensity.value = Mathf.MoveTowards(vignette.GetComponent<Vignette>().intensity.value, 0.5f, Time.deltaTime);
        soundPlayer.SetActive(true);
        description = "...";
        float timer = searchTime - 1f;
        while (timer > 0f)
        {
            cam.m_Lens.FieldOfView -= Time.deltaTime * 2.5f;
            timer -= Time.deltaTime;
            yield return null;
        }
        //yield return new WaitForSeconds(searchTime - 1f);
        //vignette.GetComponent<Vignette>().intensity.value = Mathf.MoveTowards(vignette.GetComponent<Vignette>().intensity.value, 0, Time.deltaTime);

        soundPlayer.SetActive(false);
        if (hasKey && !door.enabled)
        {
            description = "There's a key in here";
            tim.EnteredSecondArea();
            door.enabled = true;
            doorDialogue.enabled = false;
            FindObjectOfType<TaskList>().SetTask("Get to the tea party room.");
        }
        else if (!hasKey && !door.enabled)
        {
            description = "No key in here";
        }
        else description = "I already have a key";

        //yield return new WaitForSeconds(1f);
        float endingFov = cam.m_Lens.FieldOfView;
        timer = 0.5f;
        while (timer > 0f)
        {
            cam.m_Lens.FieldOfView = Mathf.Lerp(defaultFov, endingFov, timer * 2f);
            timer -= Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        cam.m_Lens.FieldOfView = defaultFov;

        PlayerDisable.ToggledDisabled();
        Destroy(this);
    }
}
