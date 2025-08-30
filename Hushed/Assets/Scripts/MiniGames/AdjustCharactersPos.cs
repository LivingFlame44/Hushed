using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCharactersPos : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("NPC"))
        {
            if(Mathf.Clamp(other.gameObject.transform.position.z, other.gameObject.transform.position.z, 3.35f) != 3.35f)
            {
                other.gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }
    }
}
