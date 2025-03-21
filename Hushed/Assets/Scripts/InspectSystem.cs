using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectSystem : MonoBehaviour
{
    public GameObject inspectSystem;
    public Transform objectToInspect;

    public float rotationSpeed = 50f;

    private Vector3 previousMousePos;
    // Start is called before the first frame update
    void Start()
    {
        
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
            objectToInspect.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
