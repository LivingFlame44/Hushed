using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NotebookManager : MonoBehaviour
{
    public static NotebookManager instance;
    public NotificationManager notificationManager;
    public NewObjectiveTypes newObjectiveTypes;
    public InventoryManager inventoryManager;


    public bool noteBookOpened;
    public RectTransform noteBook;
    public GameObject openBtn;
    public GameObject closeBtn;

    public GameObject[] clues;

    private bool notebookInteracted, mainObjectiveInteracted, questsInteracted, phoneInteracted;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (!noteBookOpened)
            {
                OpenNotebook();
            }
            else
            {
                CloseNotebook();
            }
        }
    }

    public void OpenNotebook()
    {
        noteBook.DOAnchorPos(new Vector2(0, 0), 0.25f);
        noteBookOpened = true;
        if(!notebookInteracted)
        {
            notificationManager.ShowNotification(18);
            notificationManager.ShowNotification(19);
            notebookInteracted = true;
        }
        
    }

    public void CloseNotebook()
    {
        noteBook.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1660, 0), 0.25f);
        noteBookOpened = false;
    }

    public void QuestTutorial()
    {
        if(!questsInteracted)
        {
            questsInteracted = true;
            notificationManager.ShowNotification(20);
        }
        
    }

    public void PhoneTutorial()
    {
        if(!phoneInteracted)
        {
            phoneInteracted = true;
            notificationManager.ShowNotification(20);
        }
    }

    public void ObjectivesTutorial()
    {
        notificationManager.ShowNotification(0);
    }
}
