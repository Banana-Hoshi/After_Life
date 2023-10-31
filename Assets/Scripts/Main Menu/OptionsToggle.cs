using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsToggle : MonoBehaviour
{
    [SerializeField] GameObject firstToggle;
    [SerializeField] GameObject secondToggle;
    public void ToggleInOrder()
    {
        firstToggle.SetActive(!firstToggle.activeInHierarchy);
        secondToggle.SetActive(!secondToggle.activeInHierarchy);
    }
}
