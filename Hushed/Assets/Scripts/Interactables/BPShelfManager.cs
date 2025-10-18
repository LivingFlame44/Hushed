using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class BPShelfManager : MonoBehaviour
{
    public Button[] bpButtons;
    public TextMeshProUGUI[] bpNames;
    public UnityEvent wrongBPEvent;
    public UnityEvent correctBPEvent;
    public NotificationManager notificationManager;
    public EvidenceManager evidenceManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WrongBP()
    {
        wrongBPEvent.Invoke();
    }

    public void RightBP(int evidenceIndex)
    {
        notificationManager.ShowNotification(9);
        evidenceManager.UnlockNewEvidence(evidenceIndex);
    }
}
