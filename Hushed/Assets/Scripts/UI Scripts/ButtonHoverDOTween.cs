using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonHoverDOTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Highlight Fill")]
    public Image highlightImage;
    public float paintDuration = 0.25f;

    [Header("Text Fade")]
    public Image whiteImage;
    public Image blackImage;
    public float fadeDuration = 0.25f;

    [Header("Button Group")]
    public ButtonGroupManager buttonGroupManager;

    private bool isSelected = false;

    private void Start()
    {
        //ResetToDefaultState();
    }

    private void OnEnable()
    {
        ResetToDefaultState();
    }

    private void ResetToDefaultState()
    {
        if (highlightImage != null)
            highlightImage.fillAmount = 0f;

        if (blackImage != null)
            blackImage.color = new Color(blackImage.color.r, blackImage.color.g, blackImage.color.b, 0f);

        if (whiteImage != null)
            whiteImage.color = new Color(whiteImage.color.r, whiteImage.color.g, whiteImage.color.b, 1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSelected) return;

        AnimateHoverState();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isSelected) return;

        AnimateNormalState();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Notify button group manager that this button was selected
        if (buttonGroupManager != null)
        {
            buttonGroupManager.OnButtonSelected(this);
        }

        Select();
    }

    public void Select()
    {
        isSelected = true;
        AnimateSelectedState();
    }

    public void Deselect()
    {
        isSelected = false;
        AnimateNormalState();
    }

    private void AnimateHoverState()
    {
        // Kill any ongoing animations to prevent conflicts
        highlightImage?.DOKill();
        whiteImage?.DOKill();
        blackImage?.DOKill();

        if (highlightImage != null)
            highlightImage.DOFillAmount(1f, paintDuration).SetEase(Ease.OutQuad).SetUpdate(true);

        if (blackImage != null)
            blackImage.DOFade(1f, fadeDuration).SetUpdate(true);
        if (whiteImage != null)
            whiteImage.DOFade(0f, fadeDuration).SetUpdate(true);
    }

    private void AnimateNormalState()
    {
        // Kill any ongoing animations
        highlightImage?.DOKill();
        whiteImage?.DOKill();
        blackImage?.DOKill();

        if (highlightImage != null)
            highlightImage.DOFillAmount(0f, paintDuration).SetEase(Ease.InQuad).SetUpdate(true);

        if (blackImage != null)
            blackImage.DOFade(0f, fadeDuration).SetUpdate(true);
        if (whiteImage != null)
            whiteImage.DOFade(1f, fadeDuration).SetUpdate(true);
    }

    private void AnimateSelectedState()
    {
        // Kill any ongoing animations
        highlightImage?.DOKill();
        whiteImage?.DOKill();
        blackImage?.DOKill();

        if (highlightImage != null)
            highlightImage.DOFillAmount(1f, paintDuration).SetEase(Ease.OutQuad).SetUpdate(true);

        if (blackImage != null)
            blackImage.DOFade(1f, fadeDuration).SetUpdate(true);
        if (whiteImage != null)
            whiteImage.DOFade(0f, fadeDuration).SetUpdate(true);
    }
}