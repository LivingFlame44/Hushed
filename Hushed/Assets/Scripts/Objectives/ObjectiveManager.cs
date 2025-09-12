using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

//[System.Serializable]
//public class Objective
//{
//    public int objectiveID;
//    [TextArea(3, 10)]
//    public string objectiveInfo;
//    public bool objectiveComplete;
//    public int[] prerequisiteObjectiveIDs;
//}
[System.Serializable]
public class Objectives
{
    public List<Objective> objectives = new List<Objective>();
}

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager instance;

    public bool hasSaveProgress;
    public GameObject objectiveTextPrefab;
    public GameObject objectiveTextsPanel;
    public List<Objective> objectivesList = new List<Objective>();

    public List<GameObject> currentObjectivesTextList = new List<GameObject>();
    public List<GameObject> inactiveObjectivesTextList = new List<GameObject>();
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

        ShowNewObjective(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowNewObjective(int id)
    {
        bool actionDone = QuestExistCheck(id);
        //check if prerequisite is complete
        bool prerequisiteComplete = CheckPrerequisite(objectivesList[id].prerequisiteObjectiveIDs);
        if (prerequisiteComplete)
        {
            //checks pool
            if (inactiveObjectivesTextList.Count == 0 && !actionDone)
            {
                GameObject objectiveText = Instantiate(objectiveTextPrefab, objectiveTextsPanel.transform);

                objectiveText.GetComponent<TextMeshProUGUI>().text = objectivesList[id].objectiveInfo;
                objectiveText.GetComponent<ObjectiveTextID>().objectiveID = objectivesList[id].objectiveID;
                //objectiveText.GetComponent<Objective>().objectiveInfo = objectivesList[id].objectiveInfo;
                //objectiveText.GetComponent<Objective>().objectiveID = objectivesList[id].objectiveID;
                //objectiveText.GetComponent<Objective>().prerequisiteObjectiveIDs = objectivesList[id].prerequisiteObjectiveIDs;

                currentObjectivesTextList.Add(objectiveText);
            }

            else if(inactiveObjectivesTextList.Count > 0 && !actionDone)
            {
                inactiveObjectivesTextList[0].SetActive(true);
                inactiveObjectivesTextList[0].GetComponent<TextMeshProUGUI>().text = objectivesList[id].objectiveInfo;

                inactiveObjectivesTextList[0].GetComponent<ObjectiveTextID>().objectiveID = objectivesList[id].objectiveID;
                //inactiveObjectivesTextList[0].GetComponent<Objective>().objectiveInfo = objectivesList[id].objectiveInfo;
                //inactiveObjectivesTextList[0].GetComponent<Objective>().objectiveID = objectivesList[id].objectiveID;
                //inactiveObjectivesTextList[0].GetComponent<Objective>().prerequisiteObjectiveIDs = objectivesList[id].prerequisiteObjectiveIDs;

                currentObjectivesTextList.Add(inactiveObjectivesTextList[0]);
                inactiveObjectivesTextList.RemoveAt(0);
            }

            objectivesList[id].onObjectiveStartEvent.Invoke();
            if (hasSaveProgress)
            {
                LevelData.instance.quests.Add(id);
            }
                
        }  
    }

    public bool CheckPrerequisite(int[] idList)
    {
        foreach(int id in idList)
        {
            if (objectivesList[id].objectiveComplete == false)
            {
                return false;
            }
        }
        return true;
    }

    public bool QuestExistCheck(int targetID)
    {
        foreach (GameObject i in currentObjectivesTextList)
        {
            if (i.GetComponent<ObjectiveTextID>().objectiveID == targetID)
            {
                return true;
            }
        }
        return false;
    }

    //Removes obj from ui and completes quest
    public void CompleteObjective(int id)
    {
        bool questExist = QuestExistCheck(id);
        if (questExist)
        {
            for (int i = 0; i < currentObjectivesTextList.Count; i++)
            {
                if (currentObjectivesTextList[i].GetComponent<ObjectiveTextID>().objectiveID == id)
                {
                    //update pool and removes quest in UI    
                    currentObjectivesTextList[i].transform.GetChild(0).gameObject.SetActive(true);
                    //currentObjectivesTextList[i].transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(currentObjectivesTextList[i].transform.GetComponent<RectTransform>().sizeDelta.x, 2);
                    //Debug.Log(currentObjectivesTextList[i].transform.GetComponent<RectTransform>().sizeDelta.x);
                    //Debug.Log(currentObjectivesTextList[i].transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta.x);
                    currentObjectivesTextList[i].GetComponent<TextMeshProUGUI>().color = Color.grey;
                    //currentObjectivesTextList[i].SetActive(false);
                    //inactiveObjectivesTextList.Add(currentObjectivesTextList[i]);
                    //currentObjectivesTextList.RemoveAt(i);
                    objectivesList[id].objectiveComplete = true;
                    if (hasSaveProgress)
                    {
                        LevelData.instance.completedQuests.Add(id);
                    }
                    
                    break;
                }
            }
        }
        
    }
}
