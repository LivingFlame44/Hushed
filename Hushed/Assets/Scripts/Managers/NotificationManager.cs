using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager instance;
    public GameObject spawnRef;
    //public Vector3 spawnLocation;
    public GameObject notificationPanel;
    public GameObject notificationPrefab;

    //public GameObject[] notificationList;
    private bool[] notificationStatusList;

    public List<GameObject> activeNotifList = new List<GameObject>();
    public List<GameObject> inactiveNotifList = new List<GameObject>();

    private string folderPath = "Notifications"; // Folder name inside the Assets/Resources folder

    public Notification[] notificationValuesList;
    // Start is called before the first frame update
    private void Awake()
    {
        //notificationValuesList = Resources.LoadAll<Notification>(folderPath);
        

    }
    void Start()
    {
        //spawnLocation = new Vector3(spawnRef.transform.localPosition.x, spawnRef.transform.localPosition.y, 0);
        if(instance  == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowNotification(int id)
    {
        //Debug.Log("SHow notif");
        //MoveNotificationsDown();
        if(inactiveNotifList.Count == 0)
        {
            GameObject notif = Instantiate(notificationPrefab);
            notif.transform.SetParent(notificationPanel.transform);
            //notif.transform.position = spawnLocation;
            activeNotifList.Add(notif);
            notif.transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector2(0,0);
            notif.transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPos(new Vector2(notif.transform.GetChild(0).GetComponent<Transform>().localPosition.x + 325,
            notif.transform.GetChild(0).GetComponent<Transform>().localPosition.y), 0.25f);

            InsertValues(id, notif);
            StartCoroutine(NotificationCountdown(notif));
        }
        else
        {
            GameObject notif = inactiveNotifList[0];
            inactiveNotifList.Remove(notif);
            Debug.Log("SHow notif 2");
            //notif.transform.SetParent(notificationPanel.transform);
            notif.SetActive(true);
            activeNotifList.Add(notif);
            
            //notif.transform.position = spawnLocation;
            

            notif.transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPos(new Vector2(notif.transform.GetChild(0).GetComponent<Transform>().localPosition.x + 325,
            notif.transform.GetChild(0).GetComponent<Transform>().localPosition.y), 0.25f);

            InsertValues(id, notif);
            StartCoroutine(NotificationCountdown(notif));
        }
        //int availableIndex = AvailableNotif();
        //GameObject notif = notificationList[availableIndex];
        
    }

    public void MoveNotificationsDown()
    {
        foreach(GameObject notif in activeNotifList)
        {
            notif.transform.position = new Vector3(notif.transform.position.x, notif.transform.position.y - 160, 0);
        }
    }
    public void InsertValues(int id, GameObject obj)
    {
        Notification notif = notificationValuesList[id];

        switch (notif.notificationType)
        {
            case Notification.NotificationType.TUTORIAL:
                obj.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Tutorial";
                break;
            case Notification.NotificationType.NOTIFICATION:
                obj.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Notification";
                break;
            case Notification.NotificationType.QUEST:
                obj.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Quest";
                break;
            case Notification.NotificationType.CLUE:
                obj.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Clue";
                break;
            case Notification.NotificationType.HINT:
                obj.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Hint";
                break;
            case Notification.NotificationType.OBJECTIVE:
                obj.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Objective";
                break;
            case Notification.NotificationType.MORE:
                obj.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "More Notifications";
                break;
        }
        
        obj.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = notif.notificationInfo;
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

    public IEnumerator NotificationCountdown(GameObject notif)
    {
        //StartCoroutine(ClampNotifTimer(new Vector3(325,0,0),notif));
        yield return new WaitForSeconds(7.5f);
        HideNotification(notif);
        Debug.Log("HIde notif");
    }
    public void HideNotification(GameObject notif)
    {
        //GameObject notif = activeNotifList[0];
        notif.transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPos(new Vector2(notif.transform.GetChild(0).GetComponent<Transform>().localPosition.x -325,
            notif.transform.GetChild(0).GetComponent<Transform>().localPosition.y), 0.25f);

        StartCoroutine(HideNotificationCountdown(notif));
        
    }

    public IEnumerator HideNotificationCountdown(GameObject notif)
    {
        yield return new WaitForSeconds(0.25f);
        activeNotifList.Remove(notif);
        inactiveNotifList.Add(notif);
        
        notif.SetActive(false);

    }

    public IEnumerator ClampNotifTimer(Vector3 vector, GameObject notif)
    {
        yield return new WaitForSeconds(0.25f);
        notif.transform.GetChild(0).GetComponent<Transform>().localPosition = vector;

    }
}
