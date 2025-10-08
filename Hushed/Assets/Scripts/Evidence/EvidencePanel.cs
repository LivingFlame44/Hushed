using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EvidencePanel : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public bool inSlot;
    public Evidence evidence;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private GameObject prevParent;
    private int siblingIndex;

    Vector2 startPos;

    public GameObject removeBtn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(inSlot)
        {
            return;
        }
        else
        {
            canvasGroup.alpha = 0.7f;
            canvasGroup.blocksRaycasts = false;
            startPos = transform.position;

            prevParent = this.gameObject.transform.parent.gameObject;
            siblingIndex = transform.GetSiblingIndex();
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(inSlot)
        {
            return;
        }
        else
        {
            rectTransform.anchoredPosition += eventData.delta;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (inSlot)
        {
            return;
        }
        else
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

            var hits = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, hits);

            // FIXED: Add null checks to prevent NullReferenceException
            var hit = hits.FirstOrDefault(t => t.gameObject != null && (
                t.gameObject.CompareTag("KeyQuestionSlot") ||
                (t.gameObject.CompareTag("EvidenceSlot") &&
                 t.gameObject.GetComponent<EvidenceSlot>() != null &&
                 t.gameObject.GetComponent<EvidenceSlot>().isTaken == false)
            ));

            foreach (var result in hits)
            {
                if (result.gameObject != null)
                {
                    Debug.Log($"Hit: {result.gameObject.name} (Tag: {result.gameObject.tag})");
                }
                else
                {
                    Debug.Log($"Hit: NULL GameObject");
                }
            }

            // FIXED: Also check if hit.gameObject is not null
            if (hit.isValid && hit.gameObject != null)
            {
                if (hit.gameObject.CompareTag("EvidenceSlot"))
                {
                    EvidenceSlot evidenceSlot = hit.gameObject.GetComponent<EvidenceSlot>();
                    if (evidenceSlot != null)
                    {
                        evidenceSlot.isTaken = true;

                        transform.SetParent(hit.gameObject.transform);
                        this.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                        this.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                        this.gameObject.GetComponent<RectTransform>().localPosition = new Vector2(0, -20);

                        removeBtn.SetActive(true);
                        inSlot = true;

                        EvidenceCraftingManager.instance.AddEvidence(evidence);
                        EvidenceCraftingManager.instance.CheckRecipe();
                        return;
                    }
                }
                else
                {
                    // Check if parent exists and has KeyQuestionPanel
                    if (hit.gameObject.transform.parent != null)
                    {
                        KeyQuestionPanel keyQuestionPanel = hit.gameObject.transform.parent.GetComponent<KeyQuestionPanel>();
                        if (keyQuestionPanel != null)
                        {
                            if (keyQuestionPanel.keyQuestion.questionAnswerID == evidence.evidenceID
                                && evidence.evidenceType == Evidence.EvidenceType.EvidenceAnswer)
                            {
                                Debug.Log("Key question answered successfully!");
                                keyQuestionPanel.AnswerKeyQuestion();
                            }
                            else
                            {
                                Debug.LogWarning("Evidence ID mismatch or wrong type.");
                            }
                        }
                        else
                        {
                            Debug.LogError("Parent object missing KeyQuestionPanel: " + hit.gameObject.transform.parent.name);
                        }
                    }
                    else
                    {
                        Debug.LogError("No parent found for: " + hit.gameObject.name);
                    }
                }
            }

            // Return to original position if no valid slot was found
            transform.SetParent(prevParent.transform);
            transform.SetSiblingIndex(siblingIndex);
            transform.position = startPos;

            Debug.Log("Go Back Original");
        }
    }

    public void ReturnEvidence()
    {
        gameObject.GetComponentInParent<EvidenceSlot>().isTaken = false;

        transform.SetParent(prevParent.transform);
        transform.SetSiblingIndex(siblingIndex);
        transform.position = startPos;

        EvidenceCraftingManager.instance.RemoveEvidence(evidence);

        removeBtn.SetActive(false);
        inSlot = false;

        EvidenceCraftingManager.instance.CheckRecipe();
    }

    public void ViewEvidenceInfo()
    {
        EvidenceManager.instance.ViewEvidenceInfo(evidence.evidenceImage, evidence.evidenceName, evidence.evidenceType, evidence.evidenceInfo);
    }


}
