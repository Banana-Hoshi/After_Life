using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMOD.Studio;

public class DigitalClockButton : Interactable
{
    [SerializeField] string text;
    [SerializeField] TMP_Text digit;
    [SerializeField] EventReference _buttonSound;
    EventInstance buttonSound;
    int num;
    [SerializeField] bool increamentUp;
    private void Awake()
    {
        buttonSound = RuntimeManager.CreateInstance(_buttonSound);
        buttonSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }
    public override string GetDescription()
    {
        return text;
    }
    public override int GetSpeechBubble()
    {
        return 0;
    }
    public override void Interact()
    {
        num = int.Parse(digit.text);
        if (increamentUp)
        {
            if (name == "DigitalClockButtons.006" || name == "DigitalClockButtons.007")
            {
                num += 5;
            }
            else
            {
                num += 1;
            }
            if (num > 9)
            {
                num -= 10;
            }
            if (buttonSound.isValid())
            {
                RuntimeManager.AttachInstanceToGameObject(buttonSound, transform);
                buttonSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                buttonSound.setParameterByName("ButtonState", 0);
                buttonSound.start();
            }
        }
        else
        {
            if (name == "DigitalClockButtons.006" || name == "DigitalClockButtons.007")
            {
                num -= 5;
            }
            else
            {
                num -= 1;
            }
            if (num < 0)
            {
                num += 10;
            }
            if (buttonSound.isValid())
            {
                RuntimeManager.AttachInstanceToGameObject(buttonSound, transform);
                buttonSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                buttonSound.setParameterByName("ButtonState", 1);
                buttonSound.start();
            }
        }
        digit.text = num.ToString();
    }

}
