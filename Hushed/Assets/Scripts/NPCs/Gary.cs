using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gary : MonoBehaviour
{
    public Level4_2Manager Level4_2Manager;

    public List<UnityEvent> garyEvents = new List<UnityEvent>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FirstTalk()
    {
        if (Level4_2Manager.guardInteracted) 
        {
            garyEvents[1].Invoke();
        }
        else
        {
            garyEvents[0].Invoke();
        }
    }
}
