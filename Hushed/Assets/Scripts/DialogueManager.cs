using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialoguePanel;
    public Image charIcon;
    public TextMeshProUGUI charName;
    public TextMeshProUGUI charDialogue;

    public UnityEvent dialogueEndEvent;

    private Queue<DialogueLine> lines;

    public Button[] choiceButtons;

    public bool isDialogueActive = false;
    public float typingSpeed;
    public string currentText;

    public DialogueLine currentLine;
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
    }
    void Start()
    {
        lines = new Queue<DialogueLine>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDialogueActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (charDialogue.text == currentLine.line)
                {
                    DisplayNextDialogueLine();
                }
                else
                {
                    StopAllCoroutines();
                    charDialogue.text = currentLine.line;
                }
            }
        }  
    }

    public void StartDialogue(Dialogue1 dialogue, UnityEvent dialogueEvent)
    {
        if(dialogue.dialogueLines.Count != 0) 
        {
            dialoguePanel.SetActive(true);
            isDialogueActive = true;

            lines.Clear();

            foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
            {
                lines.Enqueue(dialogueLine);
            }

            dialogueEndEvent = dialogueEvent;
            DisplayNextDialogueLine();
        }
        else
        {
            EndDialogue();
        }
    }

    public void DisplayNextDialogueLine()
    {
        //checks if end of dialogue ands has no choices
        if(lines.Count == 0 && currentLine.hasChoice == false)
        {
            EndDialogue();
            return;
        }

        //currentText = lines.Peek().line;
        currentLine = lines.Dequeue();
        if(currentLine.character.icon != null)
        {
            charIcon.gameObject.SetActive(true);
            charIcon.sprite = currentLine.character.icon;
        }
        else
        {
            charIcon.gameObject.SetActive(false);
        }

        charName.text = currentLine.character.name;

        if(currentLine.hasChoice)
        {
            DisplayChoices(currentLine.choices);
        }   

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));

        currentLine.onEndLineEvent.Invoke();
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        charDialogue.text = "";
        foreach(char c in dialogueLine.line.ToCharArray())
        {
            charDialogue.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        
    }
    void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.gameObject.SetActive(false);
        dialogueEndEvent.Invoke();
    }

    public void DisplayChoices(List<Dialogue1> choices)
    {
        for (int i = 0; i < choices.Count; i++)
        {
            choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i].dialogueName;
            choiceButtons[i].onClick.RemoveAllListeners();
            int index = i; // Capture the current index
            choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(choices[index]));
            choiceButtons[i].gameObject.SetActive(true);
        }
    }

    void OnChoiceSelected(Dialogue1 dialogue)
    {
        StartDialogue(dialogue, dialogue.dialogueEndEvent);
        foreach(Button button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }
    }
}
