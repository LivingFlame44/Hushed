using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabAI : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private float counter = 0f;
    public float breakTime;
    private int grab;
    public GameObject[] arms;
    public bool crOngoing;
    public float breakTimer;
    public Coroutine inputChecker;
    public Vector3 startPos;
    public enum AIState
    {
        IDLE,
        CHASE,
        GRAB,
        GRABBED,
        DOWNED
    }

    public AIState aiState;
    // Start is called before the first frame update
    void Start()
    {
        grab = UnityEngine.Random.Range(0, 2);
        startPos = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
 
        switch(aiState)
        {
            case AIState.IDLE:
                breakTimer = 0;
                transform.position = Vector2.MoveTowards(this.transform.position, startPos, speed * Time.deltaTime);
                break;

            case AIState.CHASE:
                ChasePlayer();
                break;

            case AIState.GRAB:
                Grabbing();
                break;

            case AIState.GRABBED:
                if(GameManager.instance.gameState == GameManager.GameState.ACTIVE)
                {
                    aiState = AIState.IDLE;
                }
                player.gameObject.SetActive(false);
                break;

            case AIState.DOWNED: 
                this.gameObject.SetActive(false);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            aiState = AIState.CHASE;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
            aiState = AIState.IDLE;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("MOuse CLicekd");
    }

    public void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, this.transform.position.y), speed * Time.deltaTime);
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < 2)
        {
            Debug.Log("huli");
            aiState = AIState.GRAB;

        }
    }

    public void Grabbing()
    {
        player.GetComponent<PlayerMovement>().playerState = PlayerMovement.PlayerState.GRABBED;
        
        switch(grab)
        {
            case 0:
                arms[grab].SetActive(true);
                BreakGrabs(grab);
                //if (!crOngoing)
                //{
                //    StartCoroutine(BreakGrab(grab));
                //}
                break;

            case 1:
                arms[grab].SetActive(true);
                BreakGrabs(grab);
                //if (!crOngoing)
                //{
                //    StartCoroutine(BreakGrab(grab));
                //}
                break;
        }
    }

    public IEnumerator BreakGrab(int key)
    {

        if (Input.GetMouseButtonDown(key))
        {
            arms[key].SetActive(false);
            yield return aiState = AIState.DOWNED;
        }

        switch (key)
        {
            case 0:
                if (Input.GetKey(KeyCode.Mouse1))
                {
                    yield return new WaitForSeconds(0);
                    arms[key].SetActive(false);
                    aiState = AIState.GRABBED;
                    if(player != null)
                    {
                        GameManager.instance.GameOver();
                        GameManager.instance.gameState = GameManager.GameState.GAMEOVER;
                    } 
                }
                break;
            case 1:
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    yield return new WaitForSeconds(0);
                    arms[key].SetActive(false);
                    aiState = AIState.GRABBED;
                    if (player != null)
                    {
                        GameManager.instance.GameOver();
                        GameManager.instance.gameState = GameManager.GameState.GAMEOVER;
                    }
                }
                break;
        }

        yield return new WaitForSeconds(breakTime);
            arms[key].SetActive(false);
            aiState = AIState.GRABBED;
            GameManager.instance.GameOver();
            GameManager.instance.gameState = GameManager.GameState.GAMEOVER;
    }

    //public IEnumerator InputCheck(int key, Coroutine inputChecker)
    //{

    //    switch (key)
    //    {
    //        case 0:
    //            if (Input.GetMouseButtonDown(key))
    //            {
    //                arms[key].SetActive(false);
    //                yield return aiState = AIState.DOWNED;
    //            }
    //            else if (Input.GetKey(KeyCode.Mouse1))
    //            {
    //                arms[key].SetActive(false);

    //                GameManager.instance.GameOver();
    //                GameManager.instance.gameState = GameManager.GameState.GAMEOVER;
    //                yield return aiState = AIState.GRABBED;
    //            }
    //            else
    //            {
    //                yield return StartCoroutine();
    //            }
    //            break;
    //        case 1:
    //            if (Input.GetMouseButtonDown(key))
    //            {
    //                arms[key].SetActive(false);
    //                yield return aiState = AIState.DOWNED;
    //            }
    //            else if (Input.GetKey(KeyCode.Mouse0))
    //            {
    //                arms[key].SetActive(false);

    //                GameManager.instance.GameOver();
    //                GameManager.instance.gameState = GameManager.GameState.GAMEOVER;
    //                yield return aiState = AIState.GRABBED;
    //            }
    //            else
    //            {
    //                yield return StartCoroutine(InputCheck(key));
    //            }
    //            break;
    //    }
    //}
    public void BreakGrabs(int key)
    {
        if (breakTimer < breakTime)
        {
            if(aiState == AIState.GRAB) 
            {
                breakTimer += Time.deltaTime;
            }
            else
            {
                breakTimer = 0;
            }
            
            if (Input.GetMouseButtonDown(key))
            {
                arms[key].SetActive(false);
                aiState = AIState.DOWNED;
            }
            else
            {
                switch (key)
                {
                    case 0:
                        if (Input.GetKey(KeyCode.Mouse1))
                        {

                            arms[key].SetActive(false);
                            aiState = AIState.GRABBED;
                            GameManager.instance.GameOver();
                            GameManager.instance.gameState = GameManager.GameState.GAMEOVER;
                        }
                        break;
                    case 1:
                        if (Input.GetKey(KeyCode.Mouse0))
                        {
                            arms[key].SetActive(false);
                            aiState = AIState.GRABBED;
                            GameManager.instance.GameOver();
                            GameManager.instance.gameState = GameManager.GameState.GAMEOVER;
                        }
                        break;
                }
            }
        }
        else
        {
            arms[key].SetActive(false);
            aiState = AIState.GRABBED;
            GameManager.instance.GameOver();
            GameManager.instance.gameState = GameManager.GameState.GAMEOVER;
        }       
    }
}
