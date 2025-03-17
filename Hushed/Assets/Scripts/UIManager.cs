
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    // Start is called before the first frame update
    public GameObject settingsPanel;
    public GameObject menuPanel;
    public GameObject levelSelectPanel;


    public enum MenuPanels
    {
        MAINMENU,
        LEVELSELECT,
        SETTINGS
    }

    public static MenuPanels currentMenuScene = MenuPanels.MAINMENU;
    private void Awake()
    {
        switch (currentMenuScene)
        {
            case MenuPanels.MAINMENU:
                ShowMainMenu();
                break;
            case MenuPanels.LEVELSELECT:
                LevelSelectMenu();
                break;
            case MenuPanels.SETTINGS:
                Settings();
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Play(string loadLevel)
    {
        AsyncManager.instance.LoadLevelBtn(loadLevel);
    }

    public void ChangeScene(string sceneName)
    {
        AsyncManager.instance.LoadScene(sceneName);
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
        levelSelectPanel.SetActive(false);
    }
    public void LevelSelectMenu()
    {
        levelSelectPanel.SetActive(true);
    }
    public void ShowMainMenu()
    {
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
    }

    public void ShowSettings()
    {
        settingsPanel.SetActive(true);
        levelSelectPanel.SetActive(false);
        menuPanel.SetActive(false);
    }


    public void Quitgame()
    {
        Application.Quit();
    }

    public void SettingsBack()
    {
        if (SceneManager.loadedSceneCount > 1)
        {
            SceneManager.UnloadSceneAsync("MainMenu");
        }
        else
        {
            ShowMainMenu();
        }

    }
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void UnlockAllLevels()
    {
        PlayerPrefs.SetInt("UnlockedLevel", 5);
        PlayerPrefs.SetInt("ReachedIndex", 5);
        PlayerPrefs.Save();
    }

    public void ShowTutorial()
    {
        SceneManager.LoadScene("IntroCutscene");
    }
}
