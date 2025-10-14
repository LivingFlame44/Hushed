using UnityEngine;
using DG.Tweening;

public class SlidePanel : MonoBehaviour
{
    [Header("Main Panel Settings")]
    public CanvasGroup panel;          // The panel to open/close
    public RectTransform panelRect;    // The panel's RectTransform
    public float slideDistance = 30f;  // How far it slides
    public float animationDuration = 0.2f;

    [Header("Optional Panel to Disable")]
    public GameObject panelToDisable;  // Another panel to disable when opening this one

    private bool isOpen = false;
    private Vector2 originalPosition;

    void Start()
    {
        // Save the starting position of the panel
        originalPosition = panelRect.anchoredPosition;

        // Start hidden
        panel.alpha = 0f;
        panel.interactable = false;
        panel.blocksRaycasts = false;
        panelRect.anchoredPosition = originalPosition - new Vector2(0, slideDistance);
    }

    public void TogglePanel()
    {
        if (isOpen)
            ClosePanel();
        else
            OpenPanel();
    }

    public void OpenPanel()
    {
        isOpen = true;
        panel.DOKill(); // stop any ongoing tweens

        // Disable another panel if assigned
        if (panelToDisable != null)
            panelToDisable.SetActive(false);

        panel.interactable = true;
        panel.blocksRaycasts = true;

        // Slide up + fade in
        panelRect.DOAnchorPos(originalPosition, animationDuration).SetEase(Ease.OutQuad);
        panel.DOFade(1f, animationDuration);
    }

    public void ClosePanel()
    {
        isOpen = false;
        panel.DOKill();

        panel.interactable = false;
        panel.blocksRaycasts = false;

        // Slide down + fade out
        panelRect.DOAnchorPos(originalPosition - new Vector2(0, slideDistance), animationDuration).SetEase(Ease.InQuad);
        panel.DOFade(0f, animationDuration);
    }
}
