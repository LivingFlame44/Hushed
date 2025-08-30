using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Level2_2Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<UnityEvent> level2Events = new List<UnityEvent>();

    public List<UnityEvent> LockerThoughtEvents = new List<UnityEvent>();

    public List<UnityEvent> openingLockerEvents = new List<UnityEvent>();

    public bool hasTool;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitLoad());
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
        if (AsyncManager.instance == null)
        {
            SceneManager.LoadScene("Level 2.1");
        }
        else
        {
            AsyncManager.instance.LoadScene("Level 2.1");
        }

    }

    public bool HasLockerKey()
    {
        if (PlayerPrefs.GetInt("hasLockerKey") == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MikaLockerThought()
    {
        if (HasLockerKey())
        {
            LockerThoughtEvents[0].Invoke();
        }
        else
        {
            LockerThoughtEvents[1].Invoke();
        }
    }

    public void OpenLocker()
    {
        if (HasLockerKey())
        {
            openingLockerEvents[1].Invoke();
        }
        else
        {
            if (hasTool)
            {
                openingLockerEvents[0].Invoke();
            }
            else
            {
                openingLockerEvents[2].Invoke();
            }
        }
    }

    public void GetTool()
    {
        hasTool = true;
    }
}
