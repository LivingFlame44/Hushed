using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "Notification", menuName = "ScriptableObjects/Notification")]

public class Notification : ScriptableObject

{
    public int notificationID;
    [TextArea(3, 10)]
    public string notificationInfo;
    public NotificationType notificationType;

    public enum NotificationType
    {
        TUTORIAL,
        CLUE,
        QUEST,
        NOTIFICATION,
        HINT,
        OBJECTIVE,
        MORE
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
