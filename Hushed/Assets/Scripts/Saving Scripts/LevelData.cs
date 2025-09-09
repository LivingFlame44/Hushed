using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static LevelData instance;
    public Vector3 playerPos;
    public List<int> quests;
    public List<int> keyQuestions;
    public List<int> completedQuests;
    public List<int> clues;
    public List<int> items;
    public List<int> removedItems;
    public List<bool> gameObjectsStatus;
    public List<GameObject> gameObjectsList;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
