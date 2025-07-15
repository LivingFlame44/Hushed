using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static InspectSystem;

//[Serializable]
//public class MyEvent : UnityEvent { }
public class NewObjectiveTypes : MonoBehaviour
{
    public GameObject inspectSystem;
    public UnityEvent storyEvents;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenGameObject(GameObject go)
    {
        go.SetActive(true);
    }

    public void InspectObject(GameObject go)
    {
        inspectSystem.SetActive(true);
        GameObject obj = Instantiate(go, inspectSystem.transform);

        obj.transform.localPosition = Vector3.zero;
        obj.transform.rotation = Quaternion.identity; 

        inspectSystem.GetComponent<InspectSystem>().objectToInspect = obj.GetComponent<Transform>();

        inspectSystem.GetComponent<InspectSystem>().objectToInspect.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void AddEventOnInspectStop(OnInspectStop eventOnStop)
    {
        inspectSystem.GetComponent<InspectSystem>().onInspectStop = eventOnStop;
    }

    
}
