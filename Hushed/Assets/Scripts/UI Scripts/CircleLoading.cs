using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleLoading : MonoBehaviour
{
    RectTransform rect;
    public float speed;
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rect.Rotate(0, 0 , 0 - (speed * Time.deltaTime));
    }
}
