using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Level4_1Manager : MonoBehaviour
{
    public List<UnityEvent> level4_1Events = new List<UnityEvent>();

    public InspectSystem inspectSystem;

    public int miguelMapPointsFound, warehouseMapPointsFound;
    public bool miguelMapInspected, warehouseMapInspected;

    public GameObject[] dialogueLines;
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

    public void MiguelMapInspect()
    {
        if(!miguelMapInspected)
        {
            level4_1Events[1].Invoke();
            miguelMapInspected = true;
        }

        if (miguelMapPointsFound < 2)
        {
            dialogueLines[0].GetComponentAtIndex<DialogueTrigger>(1).TriggerDialogue();
        }
    }

    public void WarehouseMapInspect()
    {
        if (!warehouseMapInspected)
        {
            level4_1Events[2].Invoke();
            warehouseMapInspected = true;
        }
        if (warehouseMapPointsFound < 2)
        {
            dialogueLines[1].GetComponentAtIndex<DialogueTrigger>(1).TriggerDialogue();
        }
    }

    public void MiguelMapEnd()
    {

        if (miguelMapPointsFound < 2)
        {
            dialogueLines[0].GetComponentAtIndex<DialogueTrigger>(4).TriggerDialogue();
        }
        else
        {
            level4_1Events[3].Invoke();

        }
    }

    public void WarehouseMapEnd()
    {

        if (warehouseMapPointsFound < 2)
        {
            dialogueLines[1].GetComponentAtIndex<DialogueTrigger>(4).TriggerDialogue();
        }
        else
        {
            level4_1Events[4].Invoke();
       
        }
    }

    public void FindMiguelMapPoint()
    {
        miguelMapPointsFound++;
        if (miguelMapPointsFound >= 2)
        {
            //inspectSystem.inspectEndEvents[1].RemoveAllListeners();
            //inspectSystem.inspectEvents[1].RemoveAllListeners();
            inspectSystem.EndInspect();
        }
    }

    public void FindWarehouseMapPoint()
    {
        warehouseMapPointsFound++;
        if (warehouseMapPointsFound >= 2)
        {
            //inspectSystem.inspectEndEvents[2].RemoveAllListeners();
            //inspectSystem.inspectEvents[2].RemoveAllListeners();
            inspectSystem.EndInspect();
        }
    }
}
