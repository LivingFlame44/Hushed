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
    private string prevName;

    public GameObject choicePanel;
    public Button[] choiceButtons;

    public bool isDialogueActive = false;
    public float typingSpeed;
    public string currentText;

    public DialogueLine currentLine;

    public GameObject[] textBoxPrefabs;
    public GameObject textBoxPanel;
    public Scrollbar dialogueScrollbar;

    public List<GameObject> activeLeftTextList, inactiveLeftTextList, activeRightTextList, 
        inactiveRightTextList, activeMiddleTextList, inactiveMiddleTextList;

    //public LayerMask canNextDialogue;
    //Camera cam;
    //public float lineWidth;

    //private bool castRays = true;
    // Start is called before the first frame update


    public enum TextType
    {
        Name,
        MyDialogue,
        OtherDialogue,
        NarratorDialogue,
    }

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
        //cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //if(castRays)
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        //    {
        //        if(hit.transform.gameObject.layer == canNextDialogue)
        //        {
                    
        //        }
        //    }
        //}
        //Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        //RaycastHit2D hit = Physics2D.CircleCast(mousePosition, lineWidth / 3f, Vector2.zero, 1f, canNextDialogue);
           
    }

    public void CheckNextDialogueClick()
    {
        if (isDialogueActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (charDialogue.text == currentLine.line)
                {
                    currentLine.onEndLineEvent.Invoke();
                    
                    if (currentLine.choices.Count != 0)
                    {
                        DisplayChoices(currentLine.choices);
                        dialogueScrollbar.value = 0;
                    }
                    else
                    {
                        dialogueScrollbar.value = 0;
                        DisplayNextDialogueLine();
                    }
                    
                }

                else
                {
                    StopAllCoroutines();
                    charDialogue.text = currentLine.line;
                    RefreshContentSize();
                    if (currentLine.choices.Count != 0)
                    {
                        DisplayChoices(currentLine.choices);
                    }
                    dialogueScrollbar.value = 0;

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
        if (lines.Count == 0 && currentLine.hasChoice == false)
        {
            EndDialogue();
            return;
        }

        //currentText = lines.Peek().line;
        if(lines.Count == 0){
            EndDialogue();
        }
        else
        {
            currentLine = lines.Dequeue();
        }

        if(currentLine.character.icon != null)
        {
            charIcon.gameObject.SetActive(true);
            charIcon.sprite = currentLine.character.icon;
        }
        else
        {
            charIcon.gameObject.SetActive(false);
        }

        //OLD   charName.text = currentLine.character.name;
        InstantiateText(TextType.Name, currentLine.character.name);
        prevName = currentLine.character.name;    

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));

        
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        charDialogue.text = "";
        
        foreach(char c in dialogueLine.line.ToCharArray())
        {

            charDialogue.text += c;
            charDialogue.GetComponentInParent<ContentSizeFitter>().SetLayoutHorizontal();
            dialogueScrollbar.value = 0;
            yield return new WaitForSeconds(typingSpeed);
        }
        
    }
    void EndDialogue()
    {
        isDialogueActive = false;
        StopAllCoroutines();
        ClearPool();
        prevName = null;
        dialoguePanel.gameObject.SetActive(false);
        
        dialogueEndEvent.Invoke();
    }

    public void DisplayChoices(List<Dialogue1> choices)
    {
        choicePanel.SetActive(true);
        choicePanel.transform.SetSiblingIndex(textBoxPanel.transform.childCount - 1);
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
        choicePanel.SetActive(false);
        //InstantiateText(TextType.Name, "d");
    }

    void InstantiateText(TextType type, string text)
    {
        GameObject textBox;
        switch (type)
        {
            case TextType.Name:
                switch (text)
                {
                    case "Mikaela":
                        InstantiateText(TextType.MyDialogue, currentLine.line);
                        break;
                    case "Narrator":
                        InstantiateText(TextType.NarratorDialogue, currentLine.line);
                        break;
                    default:
                        if(prevName == null || prevName != currentLine.character.name)
                        {
                            textBox = PoolLeftTextBox();
                            textBox.GetComponentInChildren<TextMeshProUGUI>().text = currentLine.character.name;
                        }
                        
                        InstantiateText(TextType.OtherDialogue, currentLine.line);
                        break;
                }
                break;
            case TextType.OtherDialogue:
                textBox = PoolLeftTextBox();
                charDialogue = textBox.GetComponentInChildren<TextMeshProUGUI>();
                break;
            case TextType.MyDialogue:
                textBox = PoolRightTextBox();
                charDialogue = textBox.GetComponentInChildren<TextMeshProUGUI>();
                break;
            case TextType.NarratorDialogue:
                textBox = PoolMiddleTextBox();
                charDialogue = textBox.GetComponentInChildren<TextMeshProUGUI>();
                break;
        }
    }

    public GameObject PoolLeftTextBox()
    {
        GameObject textBox;
        if(inactiveLeftTextList.Count == 0)
        {
            textBox = Instantiate(textBoxPrefabs[0], textBoxPanel.transform);
            activeLeftTextList.Add(textBox);
        }
        else
        {
            textBox = inactiveLeftTextList[0];
            activeLeftTextList.Add(textBox);
            inactiveLeftTextList.RemoveAt(0);
            textBox.SetActive(true);
        }

        textBox.transform.SetSiblingIndex(textBoxPanel.transform.childCount - 1);
        return textBox;
    }

    public GameObject PoolRightTextBox()
    {
        GameObject textBox;
        if (inactiveRightTextList.Count == 0)
        {
            textBox = Instantiate(textBoxPrefabs[1], textBoxPanel.transform);
            activeRightTextList.Add(textBox);
        }
        else
        {
            textBox = inactiveRightTextList[0];
            activeRightTextList.Add(textBox);
            inactiveRightTextList.RemoveAt(0);
            textBox.SetActive(true);
        }

        textBox.transform.SetSiblingIndex(textBoxPanel.transform.childCount - 1);
        return textBox;
    }

    public GameObject PoolMiddleTextBox()
    {
        GameObject textBox;
        if (inactiveMiddleTextList.Count == 0)
        {
            textBox = Instantiate(textBoxPrefabs[2], textBoxPanel.transform);
            activeMiddleTextList.Add(textBox);
        }
        else
        {
            textBox = inactiveMiddleTextList[0];
            activeMiddleTextList.Add(textBox);
            inactiveMiddleTextList.RemoveAt(0);
            textBox.SetActive(true);
        }

        textBox.transform.SetSiblingIndex(textBoxPanel.transform.childCount - 1);
        return textBox;
    }

    public void ClearPool()
    {
        ClearList(activeLeftTextList, inactiveLeftTextList);
        ClearList(activeRightTextList, inactiveRightTextList);
        ClearList(activeMiddleTextList, inactiveMiddleTextList);
    }

    private void ClearList(List<GameObject> activeList, List<GameObject> inactiveList)
    {
        while (activeList.Count > 0)
        {
            GameObject obj = activeList[0];
            inactiveList.Add(obj);
            activeList.RemoveAt(0);
            obj.SetActive(false);
        }
    }

    private void RefreshContentSize()
    {
        System.Collections.IEnumerator Routine()
        {
            var csf = charDialogue.GetComponentInParent<ContentSizeFitter>();
            csf.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            yield return null;
            csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
        this.StartCoroutine(Routine());
    }
}
