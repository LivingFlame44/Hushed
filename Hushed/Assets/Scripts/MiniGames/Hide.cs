using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    // Start is called before the first frame update
    public GoonLookWalkManager goonLookWalkManager;
    public GameObject hidingSprite;
    public GameObject interacText;

    public bool isHiding;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHiding)
        {
            // Show when not holding S
            if (Input.GetKeyUp(KeyCode.S))
            {
                goonLookWalkManager.player.SetActive(true);
                hidingSprite.SetActive(false);
                interacText.SetActive(true);
                isHiding = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interacText.SetActive(true);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || isHiding)
        {
            // Hide while holding S
            if (Input.GetKeyDown(KeyCode.S))
            {          
                hidingSprite.SetActive(true);
                isHiding = true;
                interacText.SetActive(false);
                goonLookWalkManager.player.SetActive(false);
            }
            // Show when not holding S
            if (Input.GetKeyUp(KeyCode.S))
            {
                goonLookWalkManager.player.SetActive(true);
                hidingSprite.SetActive(false);
                isHiding = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            interacText.SetActive(false);
        }
    }
}
