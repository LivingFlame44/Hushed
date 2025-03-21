using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueCharacter
{
    public string name; 
    public Sprite icon;
}
[System.Serializable]
public class DialogueLine
{
    public bool hasChoice;
    public DialogueCharacter character;
    [TextArea(3,10)]
    public string line;

    public List<Dialogue1> choices = new List<Dialogue1>();
    public UnityEvent onEndLineEvent;
}
[System.Serializable]
public class Dialogue1
{
    public string dialogueName;
    public UnityEvent dialogueEndEvent;
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    public Dialogue1 dialogue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TriggerDialogue()
    {
        Debug.Log("talk");
        DialogueManager.Instance.StartDialogue(dialogue, dialogue.dialogueEndEvent);
    }

    public virtual void Choose()
    {
       
    }

}
