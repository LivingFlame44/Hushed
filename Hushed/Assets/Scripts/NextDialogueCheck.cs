using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class NextDialogueCheck : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    public DialogueManager dialogueManager;
    // Start is called before the first frame update
    public void OnPointerClick(PointerEventData eventData)
    {
        dialogueManager.CheckNextDialogueClick();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dialogueManager.CheckNextDialogueClick();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
