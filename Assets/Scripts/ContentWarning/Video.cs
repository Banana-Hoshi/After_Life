using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    float t = 0f;
    public string nextScene = "Main Menu";

    void Update()
    {
        t += Time.deltaTime;
        if (SceneManager.GetActiveScene().name == "Credits")
        {

            if (t > (GetComponent<VideoPlayer>().clip.length) + 1f)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
        else
        {
            if (t > (GetComponent<VideoPlayer>().clip.length / 3.5) + 1f)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }
}
