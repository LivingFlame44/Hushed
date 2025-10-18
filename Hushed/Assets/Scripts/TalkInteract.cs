using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkInteract : Interactable
{
    // Start is called before the first frame update
    int talkIndex = 1;
    DialogueTrigger currentDialogue;
    public override void ShowText()
    {
        base.ShowText();
        //base.interactText.GetComponent<TextMeshPro>().text = "[E] To Talk";
    }

    public override void Interact()
    {
        base.Interact();
        Debug.Log("talk");
        DialogueTrigger trigger = this.gameObject.GetComponentAtIndex(GetComponentIndex() + talkIndex) as DialogueTrigger;
        trigger.TriggerDialogue();
        
    }

    public void ChangeDialogue()
    {
        DialogueTrigger trigger = this.gameObject.GetComponentAtIndex(GetComponentIndex() + talkIndex) as DialogueTrigger;
        trigger.enabled = false;

        talkIndex++;

        trigger = this.gameObject.GetComponentAtIndex(GetComponentIndex() + talkIndex) as DialogueTrigger;
        trigger.enabled = true;

    }

    public void SpecifyDialogue(int index)
    {
        talkIndex = index;
    }
}
