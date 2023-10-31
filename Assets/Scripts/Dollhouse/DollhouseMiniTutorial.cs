using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollhouseMiniTutorial : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(DollhouseTutorial());
    }

    IEnumerator DollhouseTutorial()
    {
        FindObjectOfType<TaskList>().SetTask("Find a key for the locked hallway door.");
        yield return new WaitForSeconds(3);
    }
}
