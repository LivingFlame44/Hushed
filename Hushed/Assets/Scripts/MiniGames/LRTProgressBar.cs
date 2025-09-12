using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRTProgressBar : MonoBehaviour
{
    public GameObject uiArrow, player;
    public GameObject uiEndPos, uiStartPos;
    public GameObject playerStartPos, playerEndPos;

    [Header("Settings")]
    public bool useXAxis = true;
    public bool useYAxis = false;
    public bool useZAxis = false;

    private RectTransform uiArrowRect;
    private RectTransform uiStartRect;
    private RectTransform uiEndRect;
    private Vector3 playerStartPosition;
    private Vector3 playerEndPosition;

    void Start()
    {
        // Get RectTransform components
        uiArrowRect = uiArrow.GetComponent<RectTransform>();
        uiStartRect = uiStartPos.GetComponent<RectTransform>();
        uiEndRect = uiEndPos.GetComponent<RectTransform>();

        // Cache player positions
        playerStartPosition = playerStartPos.transform.position;
        playerEndPosition = playerEndPos.transform.position;

        // Initialize arrow position
        UpdateArrowPosition();
    }

    void Update()
    {
        UpdateArrowPosition();
    }

    void UpdateArrowPosition()
    {
        if (player == null || uiArrow == null) return;

        // Calculate player's progress (0 to 1)
        float progress = CalculatePlayerProgress();

        // Update UI arrow position based on progress
        Vector2 newUIPosition = CalculateUIPosition(progress);
        uiArrowRect.anchoredPosition = newUIPosition;
    }

    float CalculatePlayerProgress()
    {
        Vector3 currentPlayerPos = player.transform.position;

        // Calculate total distance between start and end
        float totalDistance = CalculateAxisDistance(playerStartPosition, playerEndPosition);

        // Calculate current distance from start
        float currentDistance = CalculateAxisDistance(playerStartPosition, currentPlayerPos);

        // Ensure progress is clamped between 0 and 1
        float progress = Mathf.Clamp01(currentDistance / totalDistance);

        return progress;
    }

    float CalculateAxisDistance(Vector3 start, Vector3 end)
    {
        float distance = 0f;

        if (useXAxis) distance += Mathf.Abs(end.x - start.x);
        if (useYAxis) distance += Mathf.Abs(end.y - start.y);
        if (useZAxis) distance += Mathf.Abs(end.z - start.z);

        // If no axes selected, default to X axis
        if (distance == 0f) distance = Mathf.Abs(end.x - start.x);

        return distance;
    }

    Vector2 CalculateUIPosition(float progress)
    {
        // Get UI start and end positions in local space
        Vector2 startUIPos = uiStartRect.anchoredPosition;
        Vector2 endUIPos = uiEndRect.anchoredPosition;

        // Interpolate between start and end positions based on progress
        Vector2 newPosition = Vector2.Lerp(startUIPos, endUIPos, progress);

        return newPosition;
    }

    // Optional: Visualize the progress in the inspector
    void OnDrawGizmosSelected()
    {
        if (playerStartPos != null && playerEndPos != null && player != null)
        {
            // Draw player path
            Gizmos.color = Color.green;
            Gizmos.DrawLine(playerStartPos.transform.position, playerEndPos.transform.position);

            // Draw current player position
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(player.transform.position, 0.2f);

            // Calculate and display progress percentage
            float progress = CalculatePlayerProgress();
            Vector3 textPos = player.transform.position + Vector3.up * 2f;
#if UNITY_EDITOR
            UnityEditor.Handles.Label(textPos, $"Progress: {progress * 100f:F1}%");
#endif
        }
    }

    // Public method to get current progress (useful for other scripts)
    public float GetCurrentProgress()
    {
        return CalculatePlayerProgress();
    }

    // Public method to check if player has reached the end
    public bool HasPlayerReachedEnd()
    {
        return CalculatePlayerProgress() >= 0.99f;
    }
}
