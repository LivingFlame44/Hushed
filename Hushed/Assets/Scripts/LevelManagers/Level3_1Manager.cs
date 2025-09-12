using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level3_1Manager : MonoBehaviour
{
    public List<UnityEvent> level3Events = new List<UnityEvent>();

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
        level3Events[0].Invoke();
    }

    public void LevelEvent(int i)
    {
        level3Events[i].Invoke();
    }

}
