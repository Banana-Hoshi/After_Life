using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindmanChair : Interactable
{
    [SerializeField] CinemachineVirtualCamera m_Camera;
    [SerializeField] CinemachineVirtualCamera playerCam;
    [SerializeField] MindmanDialogue mindMan;

    public override string GetDescription()
    {
        return "Sit down?";
    }

    public override int GetSpeechBubble()
    {
        return 0;
    }

    public override void Interact()
    {
        StartCoroutine(Sit());
    }

    IEnumerator Sit()
    {
        playerCam.enabled = false;
        m_Camera.enabled = true;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(2f);
        mindMan.sitting = true;
    }
}
