using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptedEventController : MonoBehaviour
{
    TaskList taskList;
    [Header("Tutorial")]
    [SerializeField] EventReference bookFall;
    [SerializeField] GameObject book;
    [SerializeField] GameObject stuntBook;
    [SerializeField] GameObject tutorialUI;
    [Header("Drink")]
    [SerializeField] GameObject drinkUI;
    [Header("FinalLevel")]
    [SerializeField] GameObject finalLevel;

    private void Start()
    {
        taskList = FindObjectOfType<TaskList>();
        if (BedroomUtility.GetStage() == 1 && BedroomUtility.GetAttempts() == 0)
        {
            StartCoroutine(BookFall());
        }
        else if(BedroomUtility.GetStage() >= 2 && BedroomUtility.GetAttempts() == 0)
        {
            StartCoroutine(Drink());
        }
        else
        {
            StartCoroutine(Sleep());
        }
        if (BedroomUtility.GetStage() == 4 || SceneManager.GetActiveScene().name == "FinalLevelRespawn")
        {
            finalLevel.SetActive(true);
        }
    }

    IEnumerator BookFall()
    {
        stuntBook.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        taskList.SetTask("Read the book that fell off the shelf");
        book.SetActive(true);
        Destroy(stuntBook);
        yield return new WaitForSeconds(2.7f);
        tutorialUI.SetActive(true);
        yield return new WaitForSeconds(3f);
        tutorialUI.SetActive(false);
    }

    IEnumerator Drink()
    {
        yield return new WaitForSeconds(4f);
        drinkUI.SetActive(true);
        taskList.SetTask("Grab a drink of water from the Kitchen");
        yield return new WaitForSeconds(3f);
        drinkUI.SetActive(false);
    }

    IEnumerator Sleep()
    {
        yield return new WaitForSeconds(3f);
        taskList.SetTask("Go back to sleep.");
    }
}
