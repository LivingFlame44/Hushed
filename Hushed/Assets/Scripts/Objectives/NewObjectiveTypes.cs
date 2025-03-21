using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObjectiveTypes : MonoBehaviour
{
    public GameObject inspectSystem;
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
        inspectSystem.GetComponent<InspectSystem>().objectToInspect = obj.transform;
    }
}
