using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teresita : MonoBehaviour
{
    public bool isSitting, isCrying;
    public Animator animator;
    
    public Vector3 standingPos;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void TeresitaCry()
    {
        isCrying = !isCrying;
        animator.SetBool("isCrying", isCrying);
    }

    public void TeresitaSit()
    {
        switch (isSitting)
        {
            case true:
                transform.localPosition = standingPos;
                break;

            case false:
                
                transform.localPosition = new Vector3(-2.77f, -1.03f, -1.09f);

                break;

        }

        isSitting = !isSitting;
        animator.SetBool("isSitting", isSitting);
    }
}
