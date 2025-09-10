using UnityEngine;

public class AppSwitcher : MonoBehaviour
{
    public GameObject currentPanel;
    public GameObject nextPanel;

    public void SwitchPanel()
    {
        if (currentPanel != null)
            currentPanel.SetActive(false);

        if (nextPanel != null)
            nextPanel.SetActive(true);
    }
}
