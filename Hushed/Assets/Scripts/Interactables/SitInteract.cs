using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitInteract : Interactable
{
    public PlayerMovement player;

    public GameObject sitTargetObject;
    public Vector3 sitPosition;
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ShowText()
    {
        base.ShowText();
    }

    public override void Interact()
    {
        base.Interact();
        player.playerState = PlayerMovement.PlayerState.SITTING;
        player.gameObject.transform.position = sitPosition;
    }

}
