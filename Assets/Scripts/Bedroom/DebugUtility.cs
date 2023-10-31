using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUtility : MonoBehaviour
{
    [SerializeField] int stage;
    [SerializeField] bool badEnding;

    private void Awake()
    {
        BedroomUtility.SetBadEnding(badEnding);
        BedroomUtility.SetStage(stage);
    }
}
