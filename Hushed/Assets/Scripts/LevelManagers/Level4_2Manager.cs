using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Level4_2Manager : MonoBehaviour
{
    public List<UnityEvent> level4_2Events = new List<UnityEvent>();

    public InspectSystem inspectSystem;

    public bool guardInteracted;
    public int keyQuestionsSolved;

    public GameObject[] dialogueLines;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("CurrentLevelIndex", SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(WaitLoad());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator WaitLoad()
    {
        yield return new WaitForSeconds(1);
        level4_2Events[0].Invoke();
    }

    public void SolveKeyQuestion()
    {
        keyQuestionsSolved++;
        if (keyQuestionsSolved >= 2)
        {
            NotebookManager.instance.CloseNotebook();
            dialogueLines[0].GetComponentAtIndex<DialogueTrigger>(4).TriggerDialogue();
        }
    }
}
