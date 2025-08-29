using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanitorWalk : MonoBehaviour
{
    public float speed;
    public JanitorState state;
    public enum JanitorState
    {
        Processing,
        Walking,
        Looking,
        Idle
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        this.gameObject.GetComponent<Animator>().SetBool("isWalking", true);
        state = JanitorState.Walking;
    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case JanitorState.Walking:
                WalkLeft();
                break;
            case JanitorState.Looking:
                state = JanitorState.Looking;
                break;
            case JanitorState.Idle:
                break;
            case JanitorState.Processing:

                break;
        }
    }

    public void WalkLeft()
    {
        this.gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    public void Idle()
    {
        this.gameObject.GetComponent<Animator>().SetBool("isWalking", false);
        state = JanitorState.Idle;
    }
}
