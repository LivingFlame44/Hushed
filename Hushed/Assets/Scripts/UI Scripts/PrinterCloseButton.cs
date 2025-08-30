using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PrintCloseButton : MonoBehaviour
{
    [Header("Animation Settings")]
    public float fadeDuration = 0.3f;

    [Header("References")]
    public Button closeButton;
    public CanvasGroup canvasGroup; // Add CanvasGroup to your panel

    void Start()
    {
        // Auto-get canvas group if not assigned
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(FadeOutPanel);
        }
    }

    public void FadeOutPanel()
    {
        if (closeButton != null)
            closeButton.interactable = false;

        canvasGroup.DOFade(0f, fadeDuration)
            .OnComplete(() => {
                gameObject.SetActive(false);
                canvasGroup.alpha = 1f; // Reset for next time
                if (closeButton != null)
                    closeButton.interactable = true;
            });
    }

    void OnDestroy()
    {
        if (canvasGroup != null)
            canvasGroup.DOKill();
    }
}