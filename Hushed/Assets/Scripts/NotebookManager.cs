using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NotebookManager : MonoBehaviour
{
    public bool noteBookOpened;
    public RectTransform noteBook;
    public GameObject openBtn;
    public GameObject closeBtn;

    public GameObject[] clues;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (!noteBookOpened)
            {
                OpenNotebook();
            }
            else
            {
                CloseNotebook();
            }
        }
    }

    public void OpenNotebook()
    {
        noteBook.DOAnchorPos(new Vector2(0, 0), 0.25f);
        noteBookOpened = true;
    }

    public void CloseNotebook()
    {
        noteBook.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1660, 0), 0.25f);
        noteBookOpened = false;
    }
}
