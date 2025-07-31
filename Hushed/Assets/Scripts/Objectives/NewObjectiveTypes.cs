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

    public List<GameObject> inspectableList = new List<GameObject>();
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

        GameObject obj = null;
        // Try to find an existing instance in the scene (either original name or clone)
        for (int i = 0; i < inspectableList.Count; i++)
        {
            if (inspectableList[i].name == go.name + "(Clone)")
            {
                obj = inspectableList[i];

                break;
            }
        }

        // If no instance exists, instantiate from the prefab
        if (obj == null)
        {
            obj = Instantiate(go, inspectSystem.transform);
            inspectableList.Add(obj);
        }

        obj.SetActive(true);
        inspectSystem.GetComponent<InspectSystem>().objectToInspect = obj.GetComponent<Transform>();
    }

    public void AddEventOnInspectStop(OnInspectStop eventOnStop)
    {
        inspectSystem.GetComponent<InspectSystem>().onInspectStop = eventOnStop;
    }

    
}
