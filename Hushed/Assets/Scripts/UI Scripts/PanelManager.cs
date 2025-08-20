using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class PanelManager : MonoBehaviour
{
    [Header("Panel References")]
    public RectTransform graphicsPanel;
    public RectTransform audioPanel;
    public RectTransform controlsPanel;

    [Header("Button References")]
    public ButtonHoverDOTween graphicsButton;
    public ButtonHoverDOTween audioButton;
    public ButtonHoverDOTween controlsButton;

    [Header("Button Group Manager")]
    public ButtonGroupManager buttonGroupManager;

    [Header("Animation Settings")]
    public float slideDistance = 300f;
    public float animationDuration = 0.3f;

    private RectTransform currentPanel;
    private bool isAnimating = false;
    private Dictionary<RectTransform, Vector2> originalPositions = new Dictionary<RectTransform, Vector2>();

    private void Start()
    {
        // Store original positions and setup panels
        InitializePanel(graphicsPanel);
        InitializePanel(audioPanel);
        InitializePanel(controlsPanel);

        // Hide all panels at start BUT keep them active for animations
        HidePanelForAnimation(graphicsPanel);
        HidePanelForAnimation(audioPanel);
        HidePanelForAnimation(controlsPanel);
    }

    private void InitializePanel(RectTransform panel)
    {
        if (panel == null) return;

        // Store original position
        originalPositions[panel] = panel.anchoredPosition;

        // Ensure CanvasGroup exists
        CanvasGroup cg = panel.GetComponent<CanvasGroup>();
        if (cg == null)
        {
            cg = panel.gameObject.AddComponent<CanvasGroup>();
        }

        // Set initial state for animation
        cg.alpha = 0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    private void HidePanelForAnimation(RectTransform panel)
    {
        if (panel == null) return;

        // Keep panel active but hidden and non-interactable
        panel.gameObject.SetActive(true);

        CanvasGroup cg = panel.GetComponent<CanvasGroup>();
        if (cg != null)
        {
            cg.alpha = 0f;
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }

        // Position off-screen
        if (originalPositions.ContainsKey(panel))
        {
            panel.anchoredPosition = originalPositions[panel] - new Vector2(slideDistance, 0);
        }
    }

    // Button click methods - UPDATED WITH BUTTON SELECTION
    public void ShowGraphicsPanel() => ShowPanel(graphicsPanel, graphicsButton);
    public void ShowAudioPanel() => ShowPanel(audioPanel, audioButton);
    public void ShowControlsPanel() => ShowPanel(controlsPanel, controlsButton);

    private void ShowPanel(RectTransform panelToShow, ButtonHoverDOTween buttonToSelect)
    {
        if (isAnimating || currentPanel == panelToShow) return;

        isAnimating = true;

        // If there's a current panel, animate it out
        if (currentPanel != null)
        {
            AnimatePanelOut(currentPanel, () =>
            {
                // After current panel hides, show new panel
                AnimatePanelIn(panelToShow);
                currentPanel = panelToShow;

                // Select the corresponding button
                if (buttonToSelect != null && buttonGroupManager != null)
                {
                    buttonGroupManager.OnButtonSelected(buttonToSelect);
                }

                isAnimating = false;
            });
        }
        else
        {
            // No current panel, just show the new one
            AnimatePanelIn(panelToShow);
            currentPanel = panelToShow;

            // Select the corresponding button
            if (buttonToSelect != null && buttonGroupManager != null)
            {
                buttonGroupManager.OnButtonSelected(buttonToSelect);
            }

            isAnimating = false;
        }
    }

    private void AnimatePanelIn(RectTransform panel)
    {
        CanvasGroup cg = panel.GetComponent<CanvasGroup>();

        // Make panel interactable during animation
        cg.interactable = true;
        cg.blocksRaycasts = true;

        // Animate in: slide from left and fade in
        Vector2 originalPos = originalPositions[panel];
        panel.DOAnchorPos(originalPos, animationDuration).SetEase(Ease.OutCubic);
        cg.DOFade(1f, animationDuration);
    }

    private void AnimatePanelOut(RectTransform panel, System.Action onComplete)
    {
        CanvasGroup cg = panel.GetComponent<CanvasGroup>();

        // Make panel non-interactable during animation
        cg.interactable = false;
        cg.blocksRaycasts = false;

        // Animate out: slide right and fade out
        Vector2 originalPos = originalPositions[panel];
        panel.DOAnchorPos(originalPos + new Vector2(slideDistance, 0), animationDuration);
        cg.DOFade(0f, animationDuration).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }
}