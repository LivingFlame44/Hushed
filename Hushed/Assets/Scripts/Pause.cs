using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour, IPointerDownHandler
{
    public GameObject pauseBtn;

    public GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if(GameManager.instance.gameState == GameManager.GameState.PAUSED)
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        Resume();
        //    }
        //}
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            switch (GameManager.instance.gameState)
            {
                case GameManager.GameState.PAUSED:
                    if (SceneManager.loadedSceneCount > 1)
                    {
                        SceneManager.UnloadSceneAsync("MainMenu");
                    }
                    else
                    {
                        Resume();
                    }
                    break;

                case GameManager.GameState.ACTIVE:
                    Pause();
                    break;
            }          
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Resume();
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        UIManager.currentMenuScene = UIManager.MenuPanels.MAINMENU;
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        //pauseBtn.SetActive(true);
        Time.timeScale = 1f;
        GameManager.instance.gameState = GameManager.GameState.ACTIVE;

    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        //pauseBtn.SetActive(false);
        GameManager.instance.gameState = GameManager.GameState.PAUSED;
        Time.timeScale = 0f;
    }
    public void Settings()
    {
        UIManager.currentMenuScene = UIManager.MenuPanels.SETTINGS;
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
    }

    public void Restart()
    {
        pausePanel.SetActive(false);
        GameManager.instance.player.transform.position = GameManager.instance.respawnPoint;
        Time.timeScale = 1f;
        GameManager.instance.player.GetComponent<PlayerMovement>().playerState = PlayerMovement.PlayerState.IDLE;
        StopAllCoroutines();
        GameManager.instance.gameState = GameManager.GameState.ACTIVE;

    }
    public void Continue()
    {
        SceneManager.LoadScene("MainMenu");
        UIManager.currentMenuScene = UIManager.MenuPanels.LEVELSELECT;
        Time.timeScale = 1f;
    }
}
