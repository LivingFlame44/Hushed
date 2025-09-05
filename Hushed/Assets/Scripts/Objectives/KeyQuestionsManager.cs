using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;


public class KeyQuestionsManager : MonoBehaviour
{
    public static KeyQuestionsManager instance;
    public KeyQuestion[] keyQuestions;
    private string folderPath = "KeyQuestions"; // Folder name inside the Assets/Resources folder

    public List<KeyQuestion> currentQuestions = new List<KeyQuestion>();

    public List<GameObject> currentKeyQuestionPanel = new List<GameObject>();

    public List<UnityEvent> eventOnAnswer = new List<UnityEvent>();

    public GameObject keyQuestionsContainerPanel;
    public GameObject keyQuestionPanelPrefab;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Awake()
    {
        keyQuestions = Resources.LoadAll<KeyQuestion>(folderPath);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowNewKeyQuestion(int questionID)
    {
        KeyQuestion keyQuestion = keyQuestions[questionID];
        GameObject keyQuestionPanel = Instantiate(keyQuestionPanelPrefab, keyQuestionsContainerPanel.transform);
        keyQuestionPanel.SetActive(true);
        keyQuestionPanel.GetComponent<KeyQuestionPanel>().keyQuestion = keyQuestion;
        keyQuestionPanel.GetComponent<KeyQuestionPanel>().ShowKeyQuestion();

        LevelData.instance.keyQuestions.Add(questionID);
    }
}
