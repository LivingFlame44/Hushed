using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPShelfInteract : Interactable
{
    public BPShelfManager bpShelfManager;

    public string[] bpNames;

    public int[] correntBPNum;
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public override void ShowText()
    {
        base.ShowText();
    }

    public override void Interact()
    {
        base.Interact();
        bpShelfManager.gameObject.SetActive(true);

        for (int i = 0; i < correntBPNum.Length; i++)
        {
            bpShelfManager.bpNames[i].text = bpNames[i];
            if(correntBPNum[i] == 0)
            {
                bpShelfManager.bpButtons[i].onClick.RemoveAllListeners();
                //bpShelfManager.bpButtons[i].onClick.AddListener(() => PlayMusicTrack());
            }
        }

        
    }



}
