using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
public class Interactable : MonoBehaviour
{
    public GameObject interactText;
    public string interactMessage;
    public bool priorityText;

    public bool interactEnabled = false;
    public bool interacted = false;


    [Header("Animation Settings")]
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private float moveDistance = 0.7f;
    [SerializeField] private Ease fadeEase = Ease.OutCubic;
    [SerializeField] private Ease moveEase = Ease.OutCubic;
    [SerializeField] private bool playOnStart = true;

    private Vector3 originalPosition; // The final position (where text should end up)
    private Vector3 startPosition;    // The bottom position (where text starts)
    private SpriteRenderer spriteRenderer;
    private Tween currentTween;
    // Start is called before the first frame update
    void Start()
    {
        //interactText = this.gameObject.transform.GetChild(0).gameObject;

        //// Get SpriteRenderer
        //spriteRenderer = interactText.GetComponent<SpriteRenderer>();

        //if (spriteRenderer == null)
        //{
        //    Debug.LogError("SpriteFadeInMoveUpDOTween: No SpriteRenderer found on this GameObject!");
        //    return;
        //}

        //// Store the ORIGINAL position (where text should end up)
        //originalPosition = interactText.transform.position;

        //// Calculate START position (bottom position)
        //startPosition = originalPosition + Vector3.down * moveDistance;

        //// Set initial alpha to transparent and position to bottom
        //interactText.transform.position = startPosition;
        //SetSpriteAlpha(0f);

        if (playOnStart)
        {
            StartAnimation();
        }
    }

    private void Awake()
    {
        interactText = this.gameObject.transform.GetChild(0).gameObject;

        // Store the ORIGINAL position immediately
        originalPosition = interactText.transform.position;

        // Calculate START position (bottom position)
        startPosition = originalPosition + Vector3.down * moveDistance;

        // Get SpriteRenderer
        spriteRenderer = interactText.GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on this GameObject!");
            return;
        }

        // Set initial alpha to transparent and position to bottom
        interactText.transform.position = startPosition;
        SetSpriteAlpha(0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactEnabled && interacted == false)
            {
                Interact();
            }      
        }
    }

    public void OnTriggerStay(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (interacted == false)
            {
                ShowText();
                interactEnabled = true;
            }   
        }    
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (interacted == false)
            {
                ShowText();
                StartAnimation();
                interactEnabled = true;
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            interactText.SetActive(false);
            interactEnabled = false;
        }
    }

    public virtual void ShowText()
    {
        if(this.isActiveAndEnabled)
        {
            interactText.SetActive(true);
            if (!string.IsNullOrEmpty(interactMessage))
            {
                if (priorityText)
                {
                    StartCoroutine(SetTextNextFrame());
                }
                else
                {
                    //Debug.Log($"Before: {interactText.GetComponent<TextMeshPro>().text}");
                    interactText.GetComponentInChildren<TextMeshPro>().text = interactMessage;
                    //Debug.Log($"After: {interactText.GetComponent<TextMeshPro>().text}");
                }

            }
        }
        

    }

    IEnumerator SetTextNextFrame()
    {
        yield return null;
        if (interactText != null && interactMessage != null)
        {
            interactText.GetComponentInChildren<TextMeshPro>().text = interactMessage;
        }
    }

    public virtual void Interact()
    {
        Debug.Log("This is base class");
        interacted = true;
        interactText.SetActive(false);
    }

    public void InteractAgain()
    {
        interacted = false;
    }

    // Improved version with proper sequencing
    public void StartAnimation()
    {
        // COMPREHENSIVE DEBUGGING
        Debug.Log($"=== START ANIMATION ===");
        Debug.Log($"Duration: {duration}");
        Debug.Log($"Start Position: {startPosition}");
        Debug.Log($"Target Position: {originalPosition}");
        Debug.Log($"Distance: {Vector3.Distance(startPosition, originalPosition)}");

        // Force kill all tweens
        DOTween.Kill(interactText.transform);
        DOTween.Kill(spriteRenderer);
        currentTween?.Kill();

        // Reset to start position (bottom) and invisible
        interactText.transform.position = startPosition;
        SetSpriteAlpha(0f);

        // FORCE the duration to be 0.7f temporarily to test
        float animationDuration = 0.7f; // Force the value
        Debug.Log($"Using duration: {animationDuration}");

        // Animate from bottom (startPosition) to original position
        currentTween = DOTween.Sequence()
            .Append(interactText.transform.DOMove(originalPosition, animationDuration).SetEase(moveEase))
            .Join(spriteRenderer.DOFade(1f, animationDuration).SetEase(fadeEase))
            .OnStart(() => Debug.Log($"Animation STARTED at {Time.time}"))
            .OnUpdate(() => {
                if (currentTween != null)
                    Debug.Log($"Progress: {currentTween.ElapsedPercentage():F1}%");
            })
            .OnComplete(() => Debug.Log($"Animation COMPLETED at {Time.time}"));
    }

    public void StopAnimation()
    {
        currentTween?.Kill();
    }

    private void SetSpriteAlpha(float alpha)
    {
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }
    }

    void OnDestroy()
    {
        currentTween?.Kill();
    }
}
