using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EvidenceManager : MonoBehaviour
{
    public static EvidenceManager instance;

    public Evidence[] evidences;
    private string folderPath = "Evidence";

    public List<Evidence> ownedEvidenceList = new List<Evidence>();

    public GameObject evidenceContainerPanel, evidenceAnswerContainerPanel;

    public GameObject evidancePanelPrefab;

    public Color[] colors;
    public Sprite[] icons;

    public GameObject evidenceViewInfoPanel;
    public Image evidenceImage;
    public TextMeshProUGUI evidenceName, evidenceInfo, evidenceType;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Awake()
    {
        evidences = Resources.LoadAll<Evidence>(folderPath);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPage(GameObject panel)
    {
        float posLimit = Mathf.Clamp((panel.transform.GetComponent<RectTransform>().sizeDelta.x - 412f), 0 , panel.transform.GetComponent<RectTransform>().sizeDelta.x);
        posLimit = -posLimit;
        Debug.Log(posLimit);
        panel.transform.localPosition = new Vector2(panel.transform.localPosition.x - 104f, panel.transform.localPosition.y);
        if (panel.transform.localPosition.x > -104)
        {
            panel.transform.localPosition = new Vector2(-104f, panel.transform.localPosition.y);
        }
        if (panel.transform.localPosition.x < posLimit - 208)
        {
            panel.transform.localPosition = new Vector2(posLimit - 208, panel.transform.localPosition.y);
        }
        //panel.transform.localPosition = new Vector2(-Mathf.Clamp(Mathf.Abs(panel.transform.localPosition.x - 104f), 0, posLimit), panel.transform.localPosition.y);
        Debug.Log(panel.transform.localPosition);
    }

    public void PreviousPage(GameObject panel)
    {
        float posLimit = Mathf.Clamp(-Mathf.Abs(panel.transform.GetComponent<RectTransform>().sizeDelta.x - 412), panel.transform.GetComponent<RectTransform>().sizeDelta.x - 412, 0);
        panel.transform.localPosition = new Vector2(panel.transform.localPosition.x + 104, panel.transform.localPosition.y);
        Debug.Log(panel.transform.localPosition);
    }

    public void UnlockNewEvidence(int idNum)
    {
        Evidence evidence = evidences[idNum - 1];
        ownedEvidenceList.Add(evidence);

        GameObject evidencePanel;
        switch (evidence.evidenceType)
        {
            case Evidence.EvidenceType.EvidenceAnswer:
                evidencePanel = Instantiate(evidancePanelPrefab, evidenceAnswerContainerPanel.transform);
                break;

            default:
                evidencePanel = Instantiate(evidancePanelPrefab, evidenceContainerPanel.transform);
                break;
        }
        evidencePanel.transform.SetSiblingIndex(0);

        evidencePanel.GetComponent<EvidencePanel>().evidence = evidence;
        evidencePanel.GetComponentInChildren<TextMeshProUGUI>().text = evidence.evidenceName;
        evidencePanel.transform.GetChild(0).GetComponent<Image>().color = SetColor(evidence.evidenceType);
        evidencePanel.transform.GetChild(1).GetComponent<Image>().sprite = SetIcon(evidence.iconType);
    }

    public Color SetColor(Evidence.EvidenceType type)
    {
        switch (type)
        {
            case Evidence.EvidenceType.Clue:
                return colors[2];
                
            case Evidence.EvidenceType.SupportingClue:
                return colors[1];
                
            default:
                return colors[0];
                
        }
    }

    public Sprite SetIcon(Evidence.IconType type)
    {
        switch (type)
        {
            case Evidence.IconType.Document:
                return icons[0];

            case Evidence.IconType.Person:
                return icons[1];

            case Evidence.IconType.Photo:
                return icons[2];

            case Evidence.IconType.DrawingOrNotes:
                return icons[3];

            default:
                return null;

        }
    }

    public void ViewEvidenceInfo(Sprite image,string name, Evidence.EvidenceType type, string info)
    {
        evidenceViewInfoPanel.SetActive(true);
        evidenceImage.sprite = image;
        evidenceName.text = name;
        switch (type)
        { 
            case Evidence.EvidenceType.Clue:
                evidenceType.text = "Clue";
                break;

        case Evidence.EvidenceType.SupportingClue:
                evidenceType.text = "Supporting Clue";
                break;

            default:
                evidenceType.text = "Evidence";
                break;
        }
        evidenceInfo.text = info;
    }
}
