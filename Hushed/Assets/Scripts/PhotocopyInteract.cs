using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
public class PhotocopyInteract : Interactable
{
    public UnityEvent interactEvent;
    public PlayerMovement player;
    public GameObject waitingTextPanel;
    public GameObject photoCopyPanel;

    public List<UnityEvent> photocopyEvents;    

    bool isPhotocopying;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Interact()
    {
        base.Interact();

        photoCopyPanel.SetActive(true);
        player.playerState = PlayerMovement.PlayerState.PREPARING;

    }

    public IEnumerator PreparationTimer(int eventNumber)
    {
        

        StartCoroutine(LoadAnimation());

        //play animation
        yield return new WaitForSeconds(3f);
        isPhotocopying = false; 
        waitingTextPanel.SetActive(false);

        photocopyEvents[eventNumber].Invoke();

        Debug.Log("Tagumpay ang wait");


    }

    public IEnumerator LoadAnimation()
    {
        waitingTextPanel.SetActive(true);
        isPhotocopying = true;
        TextMeshProUGUI waitingText = waitingTextPanel.GetComponentInChildren<TextMeshProUGUI>();
        waitingText.text = "Photocopying.";

        string waitingIndicator = "";
        while (isPhotocopying)
        {
            if (waitingIndicator == "..")
            {
                waitingIndicator = "";
            }
            else
            {
                waitingIndicator += ".";
            }

            waitingText.text = "Photocopying." + waitingIndicator;
            yield return new WaitForSeconds(.4f);
        }
    }
    public void PlayerExit()
    {
        player.playerState = PlayerMovement.PlayerState.IDLE;
        InteractAgain();
        photoCopyPanel.SetActive(false);
    }

    public void PhotoCopy(int eventNumber)
    {
        
        StartCoroutine(PreparationTimer(eventNumber));
        
       
    }
}
