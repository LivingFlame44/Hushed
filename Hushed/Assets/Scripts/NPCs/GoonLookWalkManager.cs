using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoonLookWalkManager : MonoBehaviour
{
    public GameObject[] gooners;
    public BoxCollider detectCollider;

    public GameObject player;

    public UnityEvent onDetectEvent;
    public Vector3 startingPos;

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
        StartCoroutine(LookBackInterval());
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
        int randomNum = Random.Range(2, 6);
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
        detectCollider.enabled = false;
        gooners[0].GetComponent<Animator>().SetBool("looking", false);
        gooners[1].GetComponent<Animator>().SetBool("looking", false);
        StartCoroutine(LookBackInterval());
        state = GoonerState.Walking;
    }

    public void Walk()
    {
        this.gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onDetectEvent.Invoke();
            Debug.Log("Detected Player");
        }
    }
}
