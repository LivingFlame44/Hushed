using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoonCatchMika : MonoBehaviour
{
    public LRTChaseManager lrtChaseManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lrtChaseManager.playerState = LRTChaseManager.PlayerState.Idle;
            lrtChaseManager.goonerState = LRTChaseManager.GoonerState.Idle;
            this.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
            lrtChaseManager.StopAllCoroutines();
        }
    }
}
