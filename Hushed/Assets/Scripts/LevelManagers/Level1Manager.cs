using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1Manager : MonoBehaviour
{
    public Scrollbar firstQuestScrollbar;
    public GameObject teresitaSprite;
    public QuestNumber questNumber;
    // Start is called before the first frame update
    void Start()
    {
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
        if (firstQuestScrollbar.value == 0)
        {
            ObjectiveManager.instance.CompleteObjective(0);
            //Debug.Log("Quest 1 complete");

            teresitaSprite.SetActive(true);
            ObjectiveManager.instance.CompleteObjective(0);

            questNumber = QuestNumber.ONE;
        }
    }
}
