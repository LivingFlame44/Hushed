using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class KeyQuestionPanel : MonoBehaviour
{
    public KeyQuestion keyQuestion;
    public GameObject answerSlot, answerText, questionText;
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
        answerSlot.SetActive(false);
        answerText.SetActive(true);
        answerText.GetComponent<TextMeshProUGUI>().text = "<u>" + keyQuestion.questionAnswer + "</u>";

        keyQuestion.GetComponent<TextMeshProUGUI>().color = Color.gray;
    }
}
