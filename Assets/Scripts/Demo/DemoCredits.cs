using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DemoCredits : MonoBehaviour
{
    bool quit;
    void Update()
    {
        if (quit)
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }

    public void GetQuitButton(InputAction.CallbackContext context)
    {
        quit = context.performed;
    }
}
