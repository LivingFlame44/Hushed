using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnNPCEnter : MonoBehaviour
{
    public UnityEvent collideEvents;
    public string npcType;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag(npcType))
        {
            collideEvents.Invoke();
        }
    }
}
