using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LRTChaseManager : MonoBehaviour
{
    public float circleTimer;
    public float currentTimer;

    public GameObject goons;
    public GameObject player;

    public Image fillCircle;
    public GameObject circlePanel;
    public TextMeshProUGUI letterToPress;

    public bool isChasing;
    public bool wrongKeyPressed;

    public KeyCode curKey;

    public KeyCode[] keys;
    // Start is called before the first frame update
    public float goonSpeed, playerSpeed;
    public GoonerState goonerState;
    public PlayerState playerState;
    public CircleState circleState;
    public enum GoonerState
    {
        Processing,
        Walking,
        Normal,
        Faster,
        Idle,
        Sprint
    }

    public enum PlayerState
    {
        Processing,
        Walking,
        Faster,
        Idle
    }

    public enum CircleState
    {     
        Inactive,
        Active
    }
    // Start is called before the first frame update
    void Start()
    {
        //StartWalk();
        //circleState = CircleState.Active;
        playerState = PlayerState.Walking;
        StartCoroutine(IntervalTimer());
        goonerState = GoonerState.Faster;

        goons.transform.GetChild(0).GetComponent<Animator>().SetBool("isRunning", true);
        goons.transform.GetChild(1).GetComponent<Animator>().SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
        float goonDistance = Vector3.Distance(goons.transform.position, player.transform.position);
        if (goonDistance >= 7 && !isChasing)
        {
            goonerState = GoonerState.Normal;
        }

        switch (goonerState)
        {
            case GoonerState.Walking:
                player.GetComponent<Animator>().SetBool("isRunning", false);
                goonSpeed = 1;
                GoonRun();
                break;
            case GoonerState.Normal:
                goonSpeed = 3;
                GoonRun();
                break;
            case GoonerState.Idle:
                goons.transform.GetChild(0).GetComponent<Animator>().SetBool("isRunning", false);
                goons.transform.GetChild(1).GetComponent<Animator>().SetBool("isRunning", false);
                break;
            case GoonerState.Faster:
                goonSpeed = 5;
                GoonRun();
                break;
            case GoonerState.Processing:
                break;
            case GoonerState.Sprint:

                break;
        }

        switch (playerState)
        {
            case PlayerState.Walking:
                player.GetComponent<Animator>().SetBool("isRunning", true);
                playerSpeed = 3f;
                PlayerRun();
                break;
            case PlayerState.Idle:
                player.GetComponent<Animator>().SetBool("isRunning", false);
                break;
            case PlayerState.Processing:
                break;
        }

        switch (circleState)
        {
            case CircleState.Inactive:            
                break;
            case CircleState.Active:
                if (Input.GetKeyUp(curKey) & !wrongKeyPressed)
                {
                    isChasing = false;
                    goonerState = GoonerState.Walking;
                    Debug.Log("Pressed");
                    ResetCircle();
                }
                else if(Input.anyKeyDown && !IsValidInput())
                {
                    wrongKeyPressed = true;
                    ResetCircle();
                }
                    currentTimer += Time.deltaTime;
                fillCircle.fillAmount = 1f - (currentTimer / circleTimer);
                if(currentTimer >= circleTimer)
                {
                    goonerState = GoonerState.Faster;
                    Debug.Log("Press Failed");
                    ResetCircle();
                }
                break;
        }
    }

    public void GoonRun()
    {
        goons.transform.Translate(Vector3.right * goonSpeed * Time.deltaTime);

    }

    public void StartKeyEvent()
    {
        isChasing = true;
        goonerState = GoonerState.Faster;
        GenerateRandomKey();
        circlePanel.SetActive(true);
        circleState = CircleState.Active;
    }

    public void ResetCircle()
    {
        circlePanel.SetActive(false);
        circleState = CircleState.Inactive;
        currentTimer = 0f;
        fillCircle.fillAmount = 1f;
        StartCoroutine(IntervalTimer());
    }

    public void GenerateRandomKey()
    {
        int randomNum = Random.Range(0, 4);
        curKey = keys[randomNum];
        letterToPress.text = curKey.ToString();

    }

    public void PlayerRun()
    {
        player.gameObject.transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
    }

    public IEnumerator IntervalTimer()
    {
        yield return new WaitForSeconds(3f);
        StartKeyEvent();
    }

    public void ReloadScene()
    {
        if (AsyncManager.instance == null)
        {
            SceneManager.LoadScene("Level 2.3");
        }
        else
        {
            AsyncManager.instance.LoadScene("Level 2.3");
        }

    }

    private bool IsValidInput()
    {
        // Check if the input is the correct key, mouse buttons, or escape
        return Input.GetKeyDown(curKey) ||
               Input.GetMouseButtonDown(0) ||
               Input.GetMouseButtonDown(1) ||
               Input.GetMouseButtonDown(2) ||
               Input.GetKeyDown(KeyCode.Escape);
    }
}
