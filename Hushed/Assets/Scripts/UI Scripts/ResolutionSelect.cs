using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class ResolutionSelect : MonoBehaviour
{
    [Header("UI References")]
    public RectTransform resolutionTextRect; // The RectTransform of the text
    public Text resolutionText;              // The UI Text itself
    public Button leftButton;                // <
    public Button rightButton;               // >

    private Resolution[] resolutions;
    private int currentIndex = 0;

    private Vector3 originalPosition;

    private void Start()
    {
        // Save the original anchored position of the text
        originalPosition = resolutionTextRect.anchoredPosition;

        // Predefined list of resolutions (width, height)
        List<Vector2Int> resList = new List<Vector2Int>
        {
            new Vector2Int(1920, 1080),
            new Vector2Int(1680, 1050),
            new Vector2Int(1600, 900),
            new Vector2Int(1440, 900),
            new Vector2Int(1366, 768),
            new Vector2Int(1280, 1024),
            new Vector2Int(1280, 800),
            new Vector2Int(1280, 720),
            new Vector2Int(1024, 768),
            new Vector2Int(800, 600)
        };

        // Convert to Resolution array
        resolutions = new Resolution[resList.Count];
        for (int i = 0; i < resList.Count; i++)
        {
            resolutions[i] = new Resolution
            {
                width = resList[i].x,
                height = resList[i].y,
                refreshRate = Screen.currentResolution.refreshRate // keep current refresh
            };
        }

        // Find current resolution index
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentIndex = i;
                break;
            }
        }

        UpdateTextInstant();

        leftButton.onClick.AddListener(() => ChangeResolution(-1));
        rightButton.onClick.AddListener(() => ChangeResolution(1));
    }

    private void ChangeResolution(int direction)
    {
        currentIndex += direction;

        if (currentIndex < 0) currentIndex = resolutions.Length - 1;
        if (currentIndex >= resolutions.Length) currentIndex = 0;

        AnimateText(direction);
    }

    private void AnimateText(int direction)
    {
        // Slide out to the left or right
        float slideDistance = 25f; // <-- adjusted here
        Vector3 targetPos = originalPosition + new Vector3(direction > 0 ? slideDistance : -slideDistance, 0, 0);

        // Animate out
        resolutionTextRect.DOAnchorPos(targetPos, 0.2f).OnComplete(() =>
        {
            // Update text while offscreen
            resolutionText.text = resolutions[currentIndex].width + "x" + resolutions[currentIndex].height;

            // Move instantly to opposite side
            resolutionTextRect.anchoredPosition = originalPosition + new Vector3(direction > 0 ? -slideDistance : slideDistance, 0, 0);

            // Animate back to center
            resolutionTextRect.DOAnchorPos(originalPosition, 0.2f);
        });
    }

    private void UpdateTextInstant()
    {
        resolutionText.text = resolutions[currentIndex].width + "x" + resolutions[currentIndex].height;
        resolutionTextRect.anchoredPosition = originalPosition;
    }

    public void ApplyResolution()
    {
        Resolution res = resolutions[currentIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
