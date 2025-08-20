using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class QualityOptionsSelect : MonoBehaviour
{
    [Header("UI References")]
    public RectTransform qualityTextRect; // RectTransform of the text
    public Text qualityText;              // The UI Text itself
    public Button leftButton;             // <
    public Button rightButton;            // >

    private string[] qualities = { "LOW", "MEDIUM", "HIGH" };
    private int currentIndex = 2;

    private Vector3 originalPosition;

    private void Start()
    {
        // Save starting position
        originalPosition = qualityTextRect.anchoredPosition;

        // Set to current Unity quality level
        currentIndex = QualitySettings.GetQualityLevel();
        if (currentIndex < 0 || currentIndex >= qualities.Length) currentIndex = 2; // fallback HIGH

        UpdateTextInstant();

        leftButton.onClick.AddListener(() => ChangeQuality(-1));
        rightButton.onClick.AddListener(() => ChangeQuality(1));
    }

    private void ChangeQuality(int direction)
    {
        currentIndex += direction;

        if (currentIndex < 0) currentIndex = qualities.Length - 1;
        if (currentIndex >= qualities.Length) currentIndex = 0;

        AnimateText(direction);
        QualitySettings.SetQualityLevel(currentIndex);
    }

    private void AnimateText(int direction)
    {
        float slideDistance = 15f; // how far it slides
        Vector3 targetPos = originalPosition + new Vector3(direction > 0 ? slideDistance : -slideDistance, 0, 0);

        // Animate out
        qualityTextRect.DOAnchorPos(targetPos, 0.1f).OnComplete(() =>
        {
            // Update text while offscreen
            qualityText.text = qualities[currentIndex];

            // Instantly move to opposite side
            qualityTextRect.anchoredPosition = originalPosition + new Vector3(direction > 0 ? -slideDistance : slideDistance, 0, 0);

            // Animate back to center
            qualityTextRect.DOAnchorPos(originalPosition, 0.1f);
        });
    }

    private void UpdateTextInstant()
    {
        qualityText.text = qualities[currentIndex];
        qualityTextRect.anchoredPosition = originalPosition;
    }
}
