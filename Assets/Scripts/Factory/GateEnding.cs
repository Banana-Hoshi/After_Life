using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateEnding : MonoBehaviour
{

    [SerializeField] string nextScene;

    private void Update()
    {
        if (GetComponentInChildren<DoorControllerRotate>() != null)
        {
            if (GetComponentInChildren<DoorControllerRotate>().doorOpen)
            {
                StartCoroutine(FactoryEnding());
            }
        }
    }

    IEnumerator FactoryEnding()
    {
        yield return new WaitForSeconds(1f);
        BedroomUtility.SetBadEnding(false);
        BedroomUtility.SetStage(4);
        SceneManager.LoadScene(nextScene);
    }
}
