using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2_3Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("CurrentLevelIndex", SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene()
    {
        if (AsyncManager.instance == null)
        {
            SceneManager.LoadScene("Level 3");
        }
        else
        {
            AsyncManager.instance.LoadScene("Level 3");
        }
    }
}
