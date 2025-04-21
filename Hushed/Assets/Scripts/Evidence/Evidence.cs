using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "Evidence", menuName = "ScriptableObjects/Evidence")]

public class Evidence : ScriptableObject

{
    public string evidenceName;
    public int evidenceID;
    public EvidenceType evidenceType;
    public IconType iconType;

    public Sprite evidenceImage;
    public string evidenceInfo;
    public enum EvidenceType
    {      
        Evidence,
        EvidenceAnswer,
        Clue,
        SupportingClue,
        None
    }

    public enum IconType
    {
        Photo,
        Document,
        Person,
        DrawingOrNotes,
        None
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
