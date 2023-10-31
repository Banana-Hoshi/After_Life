using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalMusicEndTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<FinalLevelMusicController>().ending = true;
        }
    }
}
