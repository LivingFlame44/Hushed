using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class GoonLookWalkManager : MonoBehaviour
{
    public GameObject[] gooners;
    public BoxCollider detectCollider, frontCollider;

    public GameObject player;
   

    public UnityEvent[] onDetectEventLists;
    public Hide[] hidingSpots;

    public Vector3 startingPos, playerStartPos;

    public float speed;
    public GoonerState state;
    public enum GoonerState
    {
        Processing,
        Walking,
        Looking,
        Idle
    }
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        playerStartPos = player.transform.position;
        //StartWalk();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case GoonerState.Walking:
                Walk();
                break;
            case GoonerState.Looking:
                state = GoonerState.Looking;
                break;
            case GoonerState.Idle:
                break;
            case GoonerState.Processing:

                break;
        }
    }

    public IEnumerator LookBackInterval()
    {
        int randomNum = Random.Range(3, 6);
        yield return new WaitForSeconds(randomNum);
        StartCoroutine(LookBack());
    }

    public IEnumerator LookBack()
    {
        state = GoonerState.Looking;
        detectCollider.enabled = true;
        gooners[0].GetComponent<Animator>().SetBool("looking", true);
        yield return new WaitForSeconds(0.3f);

        gooners[1].GetComponent<Animator>().SetBool("looking", true);
        StartCoroutine(LookBackTimer());
    }

    public IEnumerator LookBackTimer()
    {
        yield return new WaitForSeconds(2);
        StartWalk();
    }

    public void Walk()
    {
        this.gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime);

    }

    public void Idle()
    {
        state = GoonerState.Idle;
    }

    public void StartWalk()
    {
        detectCollider.enabled = false;
        gooners[0].GetComponent<Animator>().SetBool("looking", false);
        gooners[1].GetComponent<Animator>().SetBool("looking", false);
        StartCoroutine(LookBackInterval());
        state = GoonerState.Walking;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            state = GoonerState.Looking;
            StopAllCoroutines();
            gooners[0].GetComponent<Animator>().SetBool("looking", true);         
            gooners[1].GetComponent<Animator>().SetBool("looking", true);

            int randomNum = Random.Range(0, 3);
            onDetectEventLists[randomNum].Invoke();
            Debug.Log("Detected Player");
        }
    }

    public void RestartWalk()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //transform.position = startingPos;
        //player.transform.GetChild(1).transform.position = playerStartPos;      

        //foreach(Hide h in hidingSpots)
        //{
        //    h.hidingSprite.SetActive(false);
        //    h.isHiding = false;
        //}

        //StartWalk();
    }

    public void StartCutSceneWalk()
    {
        StartWalk();
        frontCollider.enabled = false;
        StopAllCoroutines();
        state = GoonerState.Walking;

    }
}
