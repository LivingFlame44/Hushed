using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CoffeeInteract : Interactable
{
    public UnityEvent interactEvent;
    public PlayerMovement player;
    public GameObject waitingText;
    public GameObject coffeeSprite;

    public string interactWaitingText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Start is called before the first frame update
    public override void ShowText()
    {
        base.ShowText();
    }

    public override void Interact()
    {
        base.Interact();
        StartCoroutine(PreparationTimer());
    }

    public IEnumerator PreparationTimer()
    {
        player.playerState = PlayerMovement.PlayerState.PREPARING;

        waitingText.SetActive(true);
        if(interactWaitingText != null )
        {
            waitingText.GetComponentInChildren<TextMeshPro>().text = interactWaitingText;
        }
        
        //play animation
        yield return new WaitForSeconds(3f);
        waitingText.SetActive(false);
        player.hasCoffee = true;
        player.playerState = PlayerMovement.PlayerState.WALKHOLDING;
        interactEvent.Invoke();
    }

    public void ChangeInteractText()
    {

    }
}
