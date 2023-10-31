using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestionSelect : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{

    [SerializeField] int index;
    [SerializeField] MindmanDialogue dialogue;

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        dialogue.SetInput(index);
    }
}
