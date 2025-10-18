using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Vector3 respawnPoint = new Vector3(-12.1f, -0.02000001f, -1.7f);
    public GameObject player;

    public GameObject winPanel, gameOverPanel;
    public Transform objectivesPanel;

    public GameObject objectiveTextPrefab;

    public int maxPoolCount, currentPoolCount;

    public List<GameObject> objectivesList;
    public List<GameObject> KeyQuestionsList;
    public enum GameState
    {
        ACTIVE,
        PAUSED,
        GAMEOVER,
        WIN
    }

    public GameState gameState;

    public bool hasKey;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(gameState)
        {
            case GameState.ACTIVE:
                //player.SetActive(true);
                Time.timeScale = 1f;
                break;
            case GameState.GAMEOVER:
                Time.timeScale = 0f;
                break;
            case GameState.WIN:
                Time.timeScale = 0f;
                break;
            case GameState.PAUSED:
                Time.timeScale = 0f;
                break;
        }
    }

    public void StageClear()
    {
        winPanel.SetActive(true);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void ObjectiveClear(int objectiveID)
    {
        objectivesList[objectiveID].SetActive(false);
        if (CompletedAllObjectives(objectivesList) && CompletedAllKeyQuestions())
        {
            NewObjective(5);
        }
    }

    public void NewObjective(int objectiveID)
    {
        //objectivesList[objectiveID].SetActive(false);
        GameObject tempText = GetObjectFromPool();
        tempText.GetComponent<TextMeshProUGUI>().text = "";
        //tempText.GetComponent<Objective>().objectiveID = objectiveID;
        tempText.SetActive(true);
    }

    public bool CompletedAllObjectives(List<GameObject> collection)
    {
        for (int i = 0; i < collection.Count; i++)
            if (collection[i].activeSelf)
            {
                return false;
            }
        return true;
    }

    public bool CompletedAllKeyQuestions()
    {
        for (int i = 0; i < KeyQuestionsList.Count; i++)
            if (KeyQuestionsList[i].GetComponent<ClueSlot>().asnwerCorrect == false)
            {
                return false;
            }
        return true;
    }

    public GameObject GetObjectFromPool()
    {
        if(objectivesList != null)
        {
            for(int i = 0;i < objectivesList.Count;i++)
            {
                if (!objectivesList[i].activeSelf)
                {
                    return objectivesList[i];
                }
            }
            return CreateNewText();
        }
        return CreateNewText();
    }

    public GameObject CreateNewText()
    {
        var tempGO = Instantiate(objectiveTextPrefab, objectivesPanel);
        objectivesList.Add(tempGO); 
        return tempGO;
    }
}
