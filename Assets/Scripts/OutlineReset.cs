using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineReset : MonoBehaviour
{
    Renderer[] renderers;
    Material[] mats;

    private void Start()
    {
        renderers = FindObjectsOfType<Renderer>(true);
        foreach (Renderer renderer in renderers)
        {
            if (renderer.CompareTag("Interactable"))
            {
                if (renderer.sharedMaterials.Length > 1)
                {
                    mats = renderer.sharedMaterials;
                    for (int i = 0; i < mats.Length; i++)
                    {
                        if (mats[i] != null)
                        {
                            if (mats[i].name.Contains("Outline", StringComparison.InvariantCultureIgnoreCase))
                            {
                                mats[i].color = Color.black;
                                break;
                            }

                        }
                    }
                }
            }
        }
    }
}
