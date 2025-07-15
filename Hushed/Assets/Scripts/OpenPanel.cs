using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenPanel : Interactable
{
    public GameObject panelToOpen;
    public string textDisplay;
    // Start is called before the first frame update

    public override void ShowText()
    {
        base.ShowText();
        base.interactText.GetComponentInChildren<TextMeshPro>().text = $"[F] {textDisplay}";
    }

    public override void Interact()
    {
        panelToOpen.SetActive(true);
    }
}
