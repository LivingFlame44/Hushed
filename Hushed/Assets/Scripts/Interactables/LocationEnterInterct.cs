using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationEnterInterct : Interactable
{
    // Start is called before the first frame update
    public Vector3 spawnPosition;
    //public int oldPlayerIndex;
    public GameObject newPlayer, oldPlayer, oldCamera, newCamera;
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
        Debug.Log("Change Location");
        //GameManager.instance.player.transform.position = spawnPosition;
        oldCamera.SetActive(false);
        newPlayer.SetActive(true);
        newCamera.SetActive(true);
        
        //oldPlayerParent.transform.GetChild(oldPlayerIndex).gameObject.SetActive(false);
        oldPlayer.SetActive(false);
        
        this.InteractAgain();
        interactEnabled = false;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            other.gameObject.SetActive(false);
    //        }
            
    //    }
    //}
}
