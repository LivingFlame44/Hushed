using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class StartEvent : MonoBehaviour
{
    public UnityEvent startEvent;
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
        yield return new WaitForSeconds(1f);
        startEvent.Invoke();
        Debug.Log("done show obj");
    }
}
