using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VoidEnding : MonoBehaviour
{
    [SerializeField] Image topEye;
    bool finished;
    Color col = Color.clear;

    private void Update()
    {

        col += Color.Lerp(Color.clear, Color.black, Time.deltaTime / 30f);
        if (!finished)
        {
            topEye.color = col;
            if(topEye.color.a >= 0.999f)
            {
                finished = true;
            }
        }
        else
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
