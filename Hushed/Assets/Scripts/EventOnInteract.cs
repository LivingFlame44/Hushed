using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
public class EventOnInteract : Interactable
{
    
    public UnityEvent interactEvent;
    // Start is called before the first frame update
    public override void ShowText()
    {
        base.ShowText();
          
    }

    public override void Interact()
    {
        base.Interact();
        interactEvent.Invoke();
    }
}
