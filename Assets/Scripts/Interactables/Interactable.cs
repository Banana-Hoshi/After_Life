using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract string GetDescription();
    public abstract int GetSpeechBubble();
    public abstract void Interact();
}
