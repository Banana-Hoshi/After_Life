using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraulicPressButton : Interactable
{
  public bool isBeingPushed = false;
  bool active = true;
  Animator animator;
  float timer;
  [SerializeField] float cooldown;
    [SerializeField] EventReference buttonSound, pressSound;
    EventInstance pressSoundInst;
    [SerializeField] GameObject hydroPress;
    bool state = false;
    private void Awake()
    {
        if (!pressSound.IsNull)
        {
            pressSoundInst = RuntimeManager.CreateInstance(pressSound);
            pressSoundInst.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        }
    }
    private void Start() {
    animator = GetComponent<Animator>();
  }
  private void Update() {
    if (timer < cooldown) {
      timer += Time.deltaTime;
    }
  }
  public override string GetDescription() {
    return "Push the button?";
  }
  public override int GetSpeechBubble() {
    return 0;
  }
  public override void Interact() {
    state = !state;
    if (!isBeingPushed && timer >= cooldown) {
      timer = 0;
      active = !active;
      animator.SetBool("push", true);
      isBeingPushed = true;
    }
    
    RuntimeManager.PlayOneShotAttached(buttonSound, gameObject);
    
        if (state)
        {
            pressSoundInst.setParameterByName("HydroPressState", 0);
        }
        else if(!state)
        {
            pressSoundInst.setParameterByName("HydroPressState", 1);
        }
        if (pressSoundInst.isValid())
        {
            RuntimeManager.AttachInstanceToGameObject(pressSoundInst, hydroPress.transform);
            pressSoundInst.start();
            pressSoundInst.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        }
        //also lighting/effect insert code here

    }
  public bool GetSwitchDown() {
    return active;
  }
  public void ToggleAnimationOff() {
    animator.SetBool("push", false);
    pressSoundInst.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    isBeingPushed = false;
  }
}
