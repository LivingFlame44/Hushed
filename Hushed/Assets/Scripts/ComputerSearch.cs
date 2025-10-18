using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ComputerSearch : MonoBehaviour
{
    public TMP_InputField searchBar;
    public string keyword;
    public GameObject panelToOpen, noResultPanel;

    public List<UnityEvent> librarySearchEvents = new List<UnityEvent>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Search();
        }
    }

    public void Search()
    {
        if(searchBar.text.ToLower() == keyword )
        {
            librarySearchEvents[0].Invoke();
        }

        else 
        {
            librarySearchEvents[1].Invoke();
        }
    }
}
