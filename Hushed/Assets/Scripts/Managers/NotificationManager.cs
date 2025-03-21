using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    public GameObject[] notificationList;
    private bool[] notificationStatusList;


    private string folderPath = "Notifications"; // Folder name inside the Assets/Resources folder

    private Notification[] notificationValuesList;
    // Start is called before the first frame update
    private void Awake()
    {
        notificationValuesList = Resources.LoadAll<Notification>(folderPath);

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowNotification(int id)
    {
        int availableIndex = AvailableNotif();
        GameObject notif = notificationList[availableIndex];
        notif.GetComponent<RectTransform>().DOAnchorPos(new Vector2(notif.GetComponent<Transform>().position.x + 50, 
            notif.GetComponent<Transform>().position.y), 0.25f);
        StartCoroutine(NotificationCountdown(availableIndex));
    }

    public void InsertValues(int id, GameObject obj)
    {
        Notification notif = notificationValuesList[id];

        switch (notif.notificationType)
        {
            case Notification.NotificationType.TUTORIAL:
                obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Tutorial";
                break;
            case Notification.NotificationType.NOTIFICATION:
                obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Notification";
                break;
            case Notification.NotificationType.QUEST:
                obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Quest";
                break;
            case Notification.NotificationType.CLUE:
                obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Clue";
                break;
            case Notification.NotificationType.MORE:
                obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "More Notifications";
                break;
        }
        
        obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = notif.notificationInfo;
    }
    public int AvailableNotif()
    {
        for(int i = 0; i < notificationStatusList.Length; i++)
        {
            if (notificationStatusList[i] == false)
            {
                notificationStatusList[i] = true;
                return i;
            }
        }

        return 0;       
    }

    public IEnumerator NotificationCountdown(int i)
    {
        new WaitForSeconds(5);
        HideNotification(i);
        yield return null;
    }
    public void HideNotification(int i)
    {
        GameObject notif = notificationList[i];
        notif.GetComponent<RectTransform>().DOAnchorPos(new Vector2(notif.GetComponent<Transform>().position.x -50,
            notif.GetComponent<Transform>().position.y), 0.25f);
        notificationStatusList[i] = false;
    }
}
