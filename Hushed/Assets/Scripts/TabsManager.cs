using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] tabsImgsList;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectTab(GameObject selectedTab)
    {
        foreach (GameObject tab in tabsImgsList) 
        { 
            tab.SetActive(false);
        }

        selectedTab.SetActive(true);
    }
}
