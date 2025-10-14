using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonEnable : MonoBehaviour
{
    public GameObject panelToDisable;
    public GameObject panelToEnable;

    public void Switch()
    {
        if (panelToDisable != null)
            panelToDisable.SetActive(false);

        if (panelToEnable != null)
            panelToEnable.SetActive(true);
    }
}