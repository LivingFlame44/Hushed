using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security.Cryptography;

public class SaveDataJSON : MonoBehaviour
{
    public LevelData levelData;

    public Level1Manager level1Manager;
    // Start is called before the first frame update
    void Start()
    {

        levelData = LevelData.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData()
    {
        levelData.playerPos = GameManager.instance.player.transform.position;

        string json = JsonUtility.ToJson(levelData);

        using (StreamWriter writer = new StreamWriter(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json"))
        {
            writer.Write(json);
        }
    }

    public void LoadData()
    {
        string json = string.Empty;

        using (StreamReader reader = new StreamReader(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json"))
        {
            json = reader.ReadToEnd();
        }

        LevelData data = JsonUtility.FromJson<LevelData>(json);

        Level1Load(data);
    }

    public void Level1Load(LevelData data)
    {
        level1Manager.questNumber = Level1Manager.QuestNumber.ONE;
        GameManager.instance.player.GetComponent<PlayerMovement>().ResetAnimation();
        GameManager.instance.player.transform.position = data.playerPos;
        foreach (int i in data.quests)
        {
            ObjectiveManager.instance.ShowNewObjective(i);
        }
        foreach (int i in data.completedQuests)
        {
            ObjectiveManager.instance.CompleteObjective(i);
        }
        foreach (int i in data.keyQuestions)
        {
            KeyQuestionsManager.instance.ShowNewKeyQuestion(i);
        }
        foreach (int i in data.items)
        {
            InventoryManager.instance.UnlockItem(i);
        }
        foreach (int i in data.removedItems)
        {
            InventoryManager.instance.RemoveItem(i);
        }
        foreach (int i in data.clues)
        {
            EvidenceManager.instance.UnlockNewEvidence(i);
        }


    }
}


