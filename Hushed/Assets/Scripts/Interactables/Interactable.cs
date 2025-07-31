using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject interactText;
    public string interactMessage;
    public bool priorityText;

    public bool interactEnabled = false;
    public bool interacted = false;
    // Start is called before the first frame update
    void Start()
    {
        interactText = this.gameObject.transform.GetChild(0).gameObject;
    }

    private void Awake()
    {
        interactText = this.gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactEnabled && interacted == false)
            {
                Interact();
            }      
        }
    }

    public void OnTriggerStay(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (interacted == false)
            {
                ShowText();
                interactEnabled = true;
            }   
        }    
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (interacted == false)
            {
                ShowText();
                interactEnabled = true;
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            interactText.SetActive(false);
            interactEnabled = false;
        }
    }

    public virtual void ShowText()
    {
        if(this.isActiveAndEnabled)
        {
            interactText.SetActive(true);
            if (!string.IsNullOrEmpty(interactMessage))
            {
                if (priorityText)
                {
                    StartCoroutine(SetTextNextFrame());
                }
                else
                {
                    //Debug.Log($"Before: {interactText.GetComponent<TextMeshPro>().text}");
                    interactText.GetComponentInChildren<TextMeshPro>().text = interactMessage;
                    //Debug.Log($"After: {interactText.GetComponent<TextMeshPro>().text}");
                }

            }
        }
        

    }

    IEnumerator SetTextNextFrame()
    {
        yield return null;
        if (interactText != null && interactMessage != null)
        {
            interactText.GetComponentInChildren<TextMeshPro>().text = interactMessage;
        }
    }

    public virtual void Interact()
    {
        Debug.Log("This is base class");
        interacted = true;
        interactText.SetActive(false);
    }

    public void InteractAgain()
    {
        interacted = false;
    }
}
