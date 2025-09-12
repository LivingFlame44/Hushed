using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using DG.Tweening;
public class NotebookManager : MonoBehaviour
{
    public static NotebookManager instance;
    public NotificationManager notificationManager;
    public NewObjectiveTypes newObjectiveTypes;
    public InventoryManager inventoryManager;

    public List<UnityEvent> notebookCloseEvents = new List<UnityEvent>();
    public int notebookEventNum;

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

        if (SceneManager.GetActiveScene().name != "Level 1")
        {
            notebookInteracted = true;
            mainObjectiveInteracted = true;
            questsInteracted = true;
            phoneInteracted = true;
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
        NotebookInvoke();
    }

    public void AssignNotebookEvent(int index)
    {
        notebookEventNum = index;
    }

    public void NotebookInvoke()
    {
        notebookCloseEvents[notebookEventNum].Invoke();
        notebookEventNum = 0;
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

    public void MainObjectivesTutorial()
    {
        if (!mainObjectiveInteracted)
        {
            mainObjectiveInteracted = true;
            notificationManager.ShowNotification(19);
        }
    }
}
