using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalLevelController : MonoBehaviour
{
    [SerializeField] GameObject waterCutscene;
    [SerializeField] GameObject mindMan;
    [SerializeField] GameObject bedroom, fullHouse;
    [SerializeField] Light[] lights;
    [SerializeField] GameObject flashMechanic;
    [HideInInspector] public bool enableMindMan;
    [SerializeField] public GameObject finalLevelMusic;
    bool activated;
    [SerializeField] GameObject canvas;

    private void OnEnable()
    {
        activated = true;
        
    }

    private void Update()
    {
        if (activated && (!waterCutscene.activeSelf || SceneManager.GetActiveScene().name == "FinalLevelRespawn"))
        {
            StartCoroutine(StartFinalLevel());
        }
        if (enableMindMan)
        {
            mindMan.SetActive(true);
            enableMindMan = false;
        }
    }

    IEnumerator StartFinalLevel()
    {
        mindMan.SetActive(true);
        bedroom.SetActive(false);
        fullHouse.SetActive(true);
        flashMechanic.SetActive(true);
        canvas.SetActive(true);
        finalLevelMusic.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        canvas.SetActive(false);
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].intensity /= 3f;
            yield return new WaitForSeconds(0.2f);
        }
        activated = false;
    }
}
