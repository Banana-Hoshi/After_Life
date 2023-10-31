using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingDialogue : MonoBehaviour
{
    [SerializeField] MindmanTrigger trigger;
    [SerializeField] GameObject ui;
    bool activated;

    private void Update()
    {
        if (trigger.GetEntered() && !activated)
        {
            StartCoroutine(Dialogue());
        }
    }

    IEnumerator Dialogue()
    {
        activated = true;
        ui.SetActive(true);
        yield return new WaitForSeconds(2f);
        ui.SetActive(false);
    }
}
