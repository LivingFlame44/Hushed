using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
[CreateAssetMenu(fileName = "Objective", menuName = "ScriptableObjects/Objective")]
public class Objective : ScriptableObject
{
    public int objectiveID;
    [TextArea(3, 10)]
    public string objectiveInfo;
    public bool objectiveComplete;
    public int[] prerequisiteObjectiveIDs;
    public UnityEvent onObjectiveStartEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
