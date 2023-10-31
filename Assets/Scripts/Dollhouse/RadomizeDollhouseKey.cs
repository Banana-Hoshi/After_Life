using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadomizeDollhouseKey : MonoBehaviour
{
    public List<DollhouseKey> keyLocations;
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).CompareTag("IgnoreKey"))
            {
                keyLocations.Add(transform.GetChild(i).GetComponent<DollhouseKey>());
            }
        }
        keyLocations[Random.Range(0, keyLocations.Count - 1)].hasKey = true;
    }
}
