using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesLoader : MonoBehaviour
{
    public Objective[] objectives;
    private string folderPath = "Objectives"; // Folder name inside the Assets/Resources folder
    public ObjectiveManager objectiveManager;
    void Awake()
    {
        //objectives = Resources.LoadAll<Objective>(folderPath);
    }

    void Start()
    {
        foreach (Objective obj in objectives)
        {
            Objective temp = Instantiate(obj);
            objectiveManager.objectivesList.Add(temp);
        }    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
