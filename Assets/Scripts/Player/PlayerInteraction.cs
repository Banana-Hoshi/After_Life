using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDist;
    public TextMeshProUGUI[] interactionText;
    [SerializeField] LayerMask mask;
    public GameObject[] speechBubble;
    public GameObject interactObject;
    public GameObject EscapeMenu;
    public bool activated;
    public float interactTime = 5f;
    bool interacted;


    public Camera cam;
    Interactable interactable;
    Material[] mats;
    int n;

    Color white = new Color(255, 255, 255);
    Color black = new Color(0, 0, 0);

    bool objectInteracted;

    public void OnInteract(InputAction.CallbackContext context)
    {
        interacted = context.performed;
    }

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDist, mask))
        {
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                if (interactObject != null && interactObject != hit.transform.gameObject)
                {
                    for (int i = 0; i < mats.Length; i++)
                    {
                        if (mats[i] != null)
                        {
                            if (mats[i].name.Contains("Outline", StringComparison.InvariantCultureIgnoreCase))
                            {
                                mats[i].color = white;
                                break;
                            }

                        }
                    }
                    interactObject = null;
                }
                interactable = hit.collider.GetComponent<Interactable>();
                n = interactable.GetSpeechBubble();
                HandleInteraction(interactable);
                interactObject = hit.transform.gameObject;

                if (interactable.GetDescription() != "")
                {
                    interactionText[n].text = interactable.GetDescription();
                    SetSpeechBubble(interactable, true);
                }
                else
                {
                    SetSpeechBubble(interactable, false);
                }
                mats = (interactObject.GetComponent<Renderer>().sharedMaterials);
                for (int i = 0; i < mats.Length; i++)
                {
                    if (mats[i] != null && objectInteracted == false)
                    {
                        if (mats[i].name.Contains("Outline", StringComparison.InvariantCultureIgnoreCase))
                        {
                            mats[i].color = white;
                            break;
                        }
                    }
                }
                return;
            }
        }

        speechBubble[n].SetActive(false);
        if (interactObject != null)
        {
            for (int i = 0; i < mats.Length; i++)
            {
                if (mats[i] != null)
                {
                    if (mats[i].name.Contains("outline", StringComparison.InvariantCultureIgnoreCase))
                    {
                        mats[i].color = black;
                        break;
                    }
                }
            }
            if (interactObject.gameObject.tag == "Bed")
            {
                objectInteracted = false;
            }
            activated = false;
            interactObject = null;
        }
    }
    void HandleInteraction(Interactable interactable)
    {
        if (!PlayerDisable.IsPlayerDisabled() && (!activated || interactable.GetComponent<Bed>()))
        {
            if (interacted)
            {
                if (interactObject.gameObject.tag == "Bed")
                {
                    objectInteracted = true;
                }
                StartCoroutine(ResetColour());
                interactable.Interact();
                activated = true;
            }
        }
    }

    void SetSpeechBubble(Interactable interactable, bool state)
    {
        speechBubble[n].SetActive(state);
    }

    IEnumerator ResetColour()
    {
        for (int i = 0; i < mats.Length; i++)
        {
            if (mats[i] != null)
            {
                if (mats[i].name.Contains("Outline", StringComparison.InvariantCultureIgnoreCase))
                {
                    mats[i].color = black;
                    break;
                }

            }
        }
        EscapeMenu.SetActive(false);
        yield return new WaitForSeconds(interactTime);
        EscapeMenu.SetActive(true);
        activated = false;
        //objectInteracted = false;
    }
}
