using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe")]
public class Recipe : ScriptableObject
{
    public Evidence craftedEvidence;

    public int[] evidenceRecipe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
