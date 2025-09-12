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
        }).SetUpdate(true);

        // Fade Settings panel in
        settingsCanvasGroup.DOFade(1f, fadeDuration).SetUpdate(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(true); // Ensure panel active during fade
        mainMenuCanvasGroup.gameObject.SetActive(true);

        // Fade MainMenu in
        mainMenuCanvasGroup.DOFade(1f, fadeDuration).SetUpdate(true);

        // Fade Settings panel out
        settingsCanvasGroup.DOFade(0f, fadeDuration).OnComplete(() =>
        {
            settingsPanel.SetActive(false);
        }).SetUpdate(true);
    }
}
