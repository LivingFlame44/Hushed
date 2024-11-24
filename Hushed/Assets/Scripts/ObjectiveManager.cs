using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//public class Objective
//{
//    [TextArea(3, 10)]
//    public string objectiveInfo;
//    public int objectiveID;
//}
//[System.Serializable]
//public class Objectives
//{
//    public List<Objective> objectives = new List<Objective>();
//}

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager instance;
    public List<GameObject> objectivesTextList = new List<GameObject>();
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
        
    }

    public void CompleteObjective(int ID)
    {
        objectivesTextList[ID].gameObject.SetActive(false);
    }
}
