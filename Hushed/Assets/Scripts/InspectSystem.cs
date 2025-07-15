using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InspectSystem : MonoBehaviour
{
    public static InspectSystem instance;

    public GameObject inspectSystem;
    public Transform objectToInspect;

    public List<UnityEvent> inspectEndEvents = new List<UnityEvent>();

    public List<UnityEvent> inspectClickEvents = new List<UnityEvent>();

    public delegate void OnInspectStop();
    public OnInspectStop onInspectStop;

    public float rotationSpeed = 25f;

    public Vector3 previousMousePos;

    Vector2 mousescroll;
    float size;
    float currentSize;
    private float velocity = 1;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }

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
            inspectEndEvents[objectToInspect.GetComponent<InspectableObjectEvents>().eventIndex].Invoke();

            previousMousePos = Vector3.zero;
            objectToInspect.transform.rotation = Quaternion.Euler(0, 0, 0);

            objectToInspect.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }

        Zoom();
    }

    private void OnDisable()
    {
        
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
