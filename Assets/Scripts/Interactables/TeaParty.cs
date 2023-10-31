using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeaParty : Interactable
{

    [SerializeField] GameObject end;
    [SerializeField] GameObject tim;
    [SerializeField] GameObject player;

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
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        BedroomUtility.SetBadEnding(false);
        BedroomUtility.SetStage(3);
        end.SetActive(true);
        player.SetActive(false);
        tim.SetActive(false);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Bedroom");
    }
}
