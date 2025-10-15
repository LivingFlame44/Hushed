using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Level4_3Manager : MonoBehaviour
{
    public List<UnityEvent> level4_1Events = new List<UnityEvent>();
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("CurrentLevelIndex", SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(WaitLoad());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitLoad()
    {
        yield return new WaitForSeconds(1);
        level4_1Events[0].Invoke();
    }
}
