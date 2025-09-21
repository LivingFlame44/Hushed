using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class Level2Manager : MonoBehaviour
{
    public List<UnityEvent> level2Events = new List<UnityEvent>();

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("CurrentLevelIndex", SceneManager.GetActiveScene().buildIndex);
        //StartCoroutine(WaitLoad());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitLoad()
    {
        yield return new WaitForSeconds(1);
        level2Events[0].Invoke();
    }

    public void LevelEvent(int i)
    {
        level2Events[i].Invoke();
    }

    public void ChangeScene()
    {
        if (AsyncManager.instance == null)
        {
            SceneManager.LoadScene("Level 2.1");
        }
        else
        {
            AsyncManager.instance.LoadScene("Level 2.1");
        }
    }

    public void ReloadScene()
    {
        if(AsyncManager.instance == null)
        {
            SceneManager.LoadScene("Level 2.1");
        }
        else
        {
            AsyncManager.instance.LoadScene("Level 2.1");
        }
            
    }

    public void GoToLevel2_2()
    {
        if (AsyncManager.instance == null)
        {
            SceneManager.LoadScene("Level 2.2");
        }
        else
        {
            AsyncManager.instance.LoadScene("Level 2.2");
        }
    }
}
