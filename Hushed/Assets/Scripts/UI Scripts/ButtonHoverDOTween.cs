using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonHoverDOTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Highlight Fill")]
    public Image highlightImage;
    public float paintDuration = 0.25f;

    [Header("Text Fade")]
    public Image whiteImage;
    public Image grayImage;
    public float fadeDuration = 0.25f;

    private void Start()
    {
        if (highlightImage != null)
            highlightImage.fillAmount = 0f;

        if (grayImage != null)
            grayImage.color = new Color(grayImage.color.r, grayImage.color.g, grayImage.color.b, 0f); // start hidden
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (highlightImage != null)
            highlightImage.DOFillAmount(1f, paintDuration).SetEase(Ease.OutQuad);

        if (grayImage != null)
            grayImage.DOFade(1f, fadeDuration); // fade black in
        if (whiteImage != null)
            whiteImage.DOFade(0f, fadeDuration); // fade white out
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (highlightImage != null)
            highlightImage.DOFillAmount(0f, paintDuration).SetEase(Ease.InQuad);

        if (grayImage != null)
            grayImage.DOFade(0f, fadeDuration); // fade black out
        if (whiteImage != null)
            whiteImage.DOFade(1f, fadeDuration); // fade white back
    }
}
