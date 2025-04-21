using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
public class Clues : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private GameObject prevParent;
    private int siblingIndex;

    public UnityEvent onAnswerEvent;

    public string answer;
    public GameObject newImage;
    public GameObject droppableSlot;

    public string clueName, clueInfo;
    public Sprite clueImage;
    public ClueCategory clueCategory;

    public Image displayImage;
    public TextMeshProUGUI displayName, displayInfo, displayCategory;

    public GameObject[] allClues;
    public enum ClueTypes
    {
        UNDECIDED,
        FACTUAL,
        AMBIGUOUS,
        FALSE
    }

    public enum ClueCategory
    {
        WITNESS_STATEMENTS,
        PHYSICAL_EVIDENCE,
        TIMELINE

    }

    public ClueTypes trueClueType;
    public ClueTypes playerClueType;

    Vector2 startPos;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();


    }
    // Start is called before the first frame update
    void Start()
    {
        switch (clueCategory)
        {
            case ClueCategory.WITNESS_STATEMENTS:
                displayCategory.text = "Witness Statement";
                break;

            case ClueCategory.PHYSICAL_EVIDENCE:
                displayCategory.text = "Physical Evidence";
                break;

            case ClueCategory.TIMELINE:
                displayCategory.text = "Timeline";
                break;
        }

        displayName.text = clueName;
        displayInfo.text = clueInfo;

        displayImage.sprite = clueImage;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.7f;
        canvasGroup.blocksRaycasts = false;
        startPos = transform.position;

        prevParent = this.gameObject.transform.parent.gameObject;
        siblingIndex = transform.GetSiblingIndex();
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        var hits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, hits);

        var hit = hits.FirstOrDefault(t => t.gameObject.CompareTag("ClueSlot") && t.gameObject == droppableSlot);

        if (hit.isValid)
        {
            newImage.SetActive(true);
            newImage.GetComponent<TextMeshProUGUI>().text = answer;
            this.gameObject.SetActive(false);
            hit.gameObject.SetActive(false);
            hit.gameObject.GetComponent<ClueSlot>().asnwerCorrect = true;
            onAnswerEvent.Invoke();
            RemoveClues();
            //foreach(GameObject i in allClues)
            //{
            //    i.SetActive(false);
            //}
            return;
        }

        transform.SetParent(prevParent.transform);
        transform.SetSiblingIndex(siblingIndex);
        transform.position = startPos;
    }

    

    public void RemoveClues()
    {
        var clues = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == this.gameObject.name);
        foreach (var clue in clues)
        {
            clue.gameObject.SetActive(false);
        }
    }
}
