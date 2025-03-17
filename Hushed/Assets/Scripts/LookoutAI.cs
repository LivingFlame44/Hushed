using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookoutAI : MonoBehaviour
{
    public bool isLeft;
    public bool crRunning;
    public bool caughtCRRunning;

    public GameObject chatBubble;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!crRunning && GameManager.instance.gameState != GameManager.GameState.GAMEOVER)
        {
            StartCoroutine(SwitchSide());
            //Debug.Log("SWitch SIdes");
        }
    }

    public IEnumerator SwitchSide()
    {
        crRunning = true;
        yield return new WaitForSeconds(3f);
        int colliderSize = isLeft == true ? colliderSize = -13 : colliderSize = 13; 

        
        this.gameObject.GetComponent<SpriteRenderer>().flipX = isLeft;
        this.gameObject.GetComponent<BoxCollider>().center = new Vector3(colliderSize, 0, 5);
        isLeft = !isLeft; 
        crRunning = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<PlayerMovement>().playerState != PlayerMovement.PlayerState.HIDING)
            {
                //play audio hoy
                if(!caughtCRRunning)
                {
                    GameManager.instance.gameState = GameManager.GameState.GAMEOVER;
                    caughtCRRunning = true;
                    StopCoroutine(SwitchSide());
                    chatBubble.SetActive(true);
                    StartCoroutine(GameOverDelay());
                }
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            caughtCRRunning = false;
            chatBubble.SetActive(false);
        }
    }
    public IEnumerator GameOverDelay()
    {
        yield return new WaitForSecondsRealtime(1f);
        GameManager.instance.GameOver();
        
    }
}
