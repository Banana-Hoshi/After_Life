using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UnderlineController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] tmp;
    [SerializeField] Button[] buttons;
    [SerializeField] Slider slider;
    bool up, down, left, right, enter;
    //InputManager inputManager;
    int index;

    private void Start()
    {
        //inputManager = InputManager.Instance;
        index = 0;
    }

    private void Update()
    {
        if (index > tmp.Length - 1)
        {
            index = 0;
        }
        else if (index < 0)
        {
            index = tmp.Length - 1;
        }

        if (up)
        {
            tmp[index].fontStyle = FontStyles.Normal;
            index++;
            if (index > tmp.Length - 1)
            {
                index = 0;
            }
            else if (index < 0)
            {
                index = tmp.Length - 1;
            }
            tmp[index].fontStyle = FontStyles.Underline;
        }
        else if (down)
        {
            tmp[index].fontStyle = FontStyles.Normal;
            index--;
            if (index > tmp.Length - 1)
            {
                index = 0;
            }
            else if (index < 0)
            {
                index = tmp.Length - 1;
            }
            tmp[index].fontStyle = FontStyles.Underline;
        }
        if (tmp[index].name == "Sens")
        {
            if (left)
            {
                slider.value -= 0.1f;
            }
            if (right)
            {
                slider.value += 0.1f;
            }
        }
        if (enter)
        {
            if (index == 0)
            {
                buttons[index].onClick.Invoke();
            }
            else if (index == 1)
            {
                buttons[index].onClick.Invoke();
            }
        }

    }
}
