using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindmanTrigger : MonoBehaviour
{
    bool entered = false;
    [SerializeField] GameObject option;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            entered = true;
            option.SetActive(false);
        }
    }

    public bool GetEntered()
    {
        return entered;
    }
}
