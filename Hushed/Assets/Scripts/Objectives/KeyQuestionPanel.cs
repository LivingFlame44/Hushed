using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class KeyQuestionPanel : MonoBehaviour
{
    public KeyQuestion keyQuestion;
    public GameObject answerSlot, answerText, questionText;
    public bool isAnswered;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowKeyQuestion()
    {
        questionText.GetComponent<TextMeshProUGUI>().text = keyQuestion.questionName;
    }

    public void AnswerKeyQuestion()
    {
        if(!isAnswered)
        {
            answerSlot.SetActive(false);
            answerText.SetActive(true);
            answerText.GetComponent<TextMeshProUGUI>().text = "<u>" + keyQuestion.questionAnswer + "</u>";

            questionText.GetComponent<TextMeshProUGUI>().color = Color.gray;
            answerText.GetComponent<TextMeshProUGUI>().color = Color.gray;

            isAnswered = true;

            KeyQuestionsManager.instance.eventOnAnswer[keyQuestion.eventOnAnswerNum].Invoke();
            Debug.Log("go event");
        }
        


    }
}
