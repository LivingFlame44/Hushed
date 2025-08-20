using UnityEngine;
using System.Collections.Generic;

public class ButtonGroupManager : MonoBehaviour
{
    public List<ButtonHoverDOTween> buttons = new List<ButtonHoverDOTween>();
    private ButtonHoverDOTween currentlySelectedButton;

    private void Start()
    {
        // Deselect all buttons at start
        DeselectAllButtons();
    }

    public void OnButtonSelected(ButtonHoverDOTween selectedButton)
    {
        // Deselect previously selected button
        if (currentlySelectedButton != null && currentlySelectedButton != selectedButton)
        {
            currentlySelectedButton.Deselect();
        }

        // Set new selected button
        currentlySelectedButton = selectedButton;
    }

    public void DeselectAllButtons()
    {
        foreach (var button in buttons)
        {
            if (button != null)
            {
                button.Deselect();
            }
        }
        currentlySelectedButton = null;
    }
}