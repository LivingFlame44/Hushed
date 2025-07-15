using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class InspectableObjectEvents : MonoBehaviour
{
    public int eventIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Debug.Log("Roateted");
    }
}
