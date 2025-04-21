using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

public class InspectSystem : MonoBehaviour
{
    public GameObject inspectSystem;
    public Transform objectToInspect;

    public delegate void OnInspectStop();
    public OnInspectStop onInspectStop;

    public float rotationSpeed = 50f;

    private Vector3 previousMousePos;

    Vector2 mousescroll;
    float size;
    float currentSize;
    private float velocity = 1;
    // Start is called before the first frame update
    void Start()
    {
        size = 1;
        currentSize = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            previousMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 deltaMousePosition = Input.mousePosition - previousMousePos;
            float rotationX = deltaMousePosition.y * rotationSpeed * Time.deltaTime;
            float rotationY = -deltaMousePosition.x * rotationSpeed * Time.deltaTime;

            Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
            objectToInspect.rotation = rotation * objectToInspect.rotation;

            previousMousePos = Input.mousePosition;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            //objectToInspect.GetComponent<InspectableObjectEvents>().inspectableObjectEvent.Invoke();
            objectToInspect.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }

        Zoom();
    }

    
    public void Zoom()
    {
        
        //mousescroll = Input.mouseScrollDelta;
        size += Input.GetAxisRaw("Mouse ScrollWheel") * 0.5f;
        size = Mathf.Clamp(size, 1, 1.35f);
        currentSize = Mathf.SmoothDamp(currentSize, size, ref velocity, .1f);
        objectToInspect.localScale = new Vector2(currentSize, currentSize);
        
    }
}
