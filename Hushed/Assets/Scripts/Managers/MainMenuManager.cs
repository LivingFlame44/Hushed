using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        AudioManager.Instance.sfxSource.Stop();
        string pathToScene = SceneUtility.GetScenePathByBuildIndex(PlayerPrefs.GetInt("CurrentLevelIndex", 1));
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(pathToScene);

        AsyncManager.instance.LoadScene(sceneName);
    }

    public void NewGame()
    {
        AudioManager.Instance.sfxSource.Stop();

        //string pathToScene = SceneUtility.GetScenePathByBuildIndex(PlayerPrefs.GetInt("CurrentLevelIndex", 1));
        //string sceneName = System.IO.Path.GetFileNameWithoutExtension(pathToScene);

        AsyncManager.instance.LoadScene("IntroCutScene");
        PlayerPrefs.SetInt("Level1Save", 0);
    }
}
