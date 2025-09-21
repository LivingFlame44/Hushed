using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Level1Manager : MonoBehaviour
{
    public Scrollbar firstQuestScrollbar;
    public GameObject teresitaSprite;
    public QuestNumber questNumber;

    public List<UnityEvent> level1Events = new List<UnityEvent>();
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("CurrentLevelIndex", SceneManager.GetActiveScene().buildIndex);
        questNumber = QuestNumber.ZERO;
    }
    public enum QuestNumber
    {
        ZERO = 0,
        ONE = 1,
        TWO = 2,
        THREE =3,
        FOUR = 4,
        FIVE = 5,
        SIX = 6
    }
    // Update is called once per frame
    void Update()
    {
        switch (questNumber)
        {
            case QuestNumber.ZERO:
                Quest1();
                break;
            case QuestNumber.ONE:
                break;
        }
        
    }

    public void Quest1()
    {
        if (firstQuestScrollbar.value <= 0)
        {
            if(questNumber == QuestNumber.ZERO)
            {
                //Debug.Log("Quest 1 complete");
                ObjectiveManager.instance.CompleteObjective(0);
                level1Events[0].Invoke();
                questNumber = QuestNumber.ONE;
            }
            
        }
    }

    public void HasWallet()
    {
        if (InventoryManager.instance.CheckInventory(1))
        {
            NotificationManager.instance.ShowNotification(22);
            NotificationManager.instance.ShowNotification(23);
        }
        else
        {
            NotificationManager.instance.ShowNotification(24);
        }

        NotificationManager.instance.ShowNotification(25);
    }
}
