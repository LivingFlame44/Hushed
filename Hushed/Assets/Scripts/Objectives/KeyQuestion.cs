using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "KeyQuestion", menuName = "ScriptableObjects/KeyQuestion")]
public class KeyQuestion : ScriptableObject
{
    public int questionID;
    public string questionName;
    public int questionAnswerID;
    public string questionAnswer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
