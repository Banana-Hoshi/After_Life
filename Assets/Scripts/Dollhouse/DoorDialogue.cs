using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDialogue : Interactable
{
    [SerializeField] GameObject[] speechbubble;
    int n;

    private void Start()
    {
        n = Random.Range(0, speechbubble.Length);
    }
    public override string GetDescription()
    {
        if (GetComponent<DoorControllerRotate>().enabled)
        {
            Destroy(this);
            return "";
        }
        else
        {
            return "The door is locked";
        }
    }
    public override int GetSpeechBubble()
    {
        return n;
    }
    public override void Interact()
    {
        Debug.Log("The door is locked");
    }

}
