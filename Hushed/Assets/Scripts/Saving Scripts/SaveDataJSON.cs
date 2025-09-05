using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataJSON : MonoBehaviour
{
    public LevelData levelData;
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
    }
}
