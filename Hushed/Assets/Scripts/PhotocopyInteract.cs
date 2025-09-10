using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class PhotocopyInteract : Interactable
{
    public UnityEvent interactEvent;
    public PlayerMovement player;
    public GameObject waitingTextPanel;
    public GameObject photoCopyPanel;
    public Image animationImage;

    public List<UnityEvent> photocopyEvents;
    public Sprite[] printAnimSprites;
    public int curAnimNum = 0;

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

    //public IEnumerator PreparationTimer(int eventNumber)
    //{
        

    //    StartCoroutine(LoadAnimation());

    //    //play animation
    //    yield return new WaitForSeconds(2f);
    //    isPhotocopying = false; 
    //    waitingTextPanel.SetActive(false);

    //    photocopyEvents[eventNumber].Invoke();

    //    Debug.Log("Tagumpay ang wait");


    //}

    //public IEnumerator LoadAnimation()
    //{
    //    waitingTextPanel.SetActive(true);
    //    isPhotocopying = true;
    //    //TextMeshProUGUI waitingText = waitingTextPanel.GetComponentInChildren<TextMeshProUGUI>();
    //    //waitingText.text = "Photocopying.";

    //    //string waitingIndicator = "";
    //    while (isPhotocopying)
    //    {
    //        //if (waitingIndicator == "..")
    //        //{
    //        //    waitingIndicator = "";
    //        //}
    //        //else
    //        //{
    //        //    waitingIndicator += ".";
    //        //}

    //        //waitingText.text = "Photocopying." + waitingIndicator;
    //        //yield return new WaitForSeconds(.4f);

    //        yield return new WaitForSeconds(0.5f);

    //        if(curAnimNum >= 3)
    //        {
    //            curAnimNum = 0;
    //        }
    //        curAnimNum++;

    //        animationImage.sprite = printAnimSprites[curAnimNum];


    //    }
    //}

    public IEnumerator PreparationTimer(int eventNumber)
    {
        // Start the animation
        StartCoroutine(LoadAnimation());

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Stop the animation
        isPhotocopying = false;
        waitingTextPanel.SetActive(false);

        // Invoke the photocopy event
        photocopyEvents[eventNumber].Invoke();

        Debug.Log("Tagumpay ang wait");
    }

    public IEnumerator LoadAnimation()
    {
        waitingTextPanel.SetActive(true);
        isPhotocopying = true;

        // Reset animation state
        curAnimNum = 0;

        // Animation loop
        while (isPhotocopying)
        {
            // Update the animation sprite
            animationImage.sprite = printAnimSprites[curAnimNum];

            // Wait before next frame
            yield return new WaitForSeconds(0.5f);

            // Advance to next frame
            curAnimNum = (curAnimNum + 1) % printAnimSprites.Length;
        }
    }


    public IEnumerator SwitchAnimation()
    {
        yield return new WaitForSeconds(.5f);

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
