using UnityEngine;
using DG.Tweening;

public class SettingsPanelController : MonoBehaviour
{
    public GameObject settingsPanel;
    public CanvasGroup settingsCanvasGroup;
    public CanvasGroup mainMenuCanvasGroup;
    public float fadeDuration = 0.5f;

    void Awake()
    {
        settingsPanel.SetActive(false);
        if (settingsCanvasGroup != null)
            settingsCanvasGroup.alpha = 0f;
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);

        // Fade MainMenu out
        mainMenuCanvasGroup.DOFade(0f, fadeDuration).OnComplete(() =>
        {
            mainMenuCanvasGroup.gameObject.SetActive(false);
        });

        // Fade Settings panel in
        settingsCanvasGroup.DOFade(1f, fadeDuration);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(true); // Ensure panel active during fade
        mainMenuCanvasGroup.gameObject.SetActive(true);

        // Fade MainMenu in
        mainMenuCanvasGroup.DOFade(1f, fadeDuration);

        // Fade Settings panel out
        settingsCanvasGroup.DOFade(0f, fadeDuration).OnComplete(() =>
        {
            settingsPanel.SetActive(false);
        });
    }
}
