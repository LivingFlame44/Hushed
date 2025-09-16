using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SavingUI : MonoBehaviour
{
    public GameObject savePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowSaveUI()
    {
        savePanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -50), 0.50f);
        StartCoroutine(HideSaveUITimer());
    }

    public IEnumerator HideSaveUITimer()
    {
        yield return new WaitForSeconds(2.25f);
        savePanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2(480, -50), 0.50f);
        StartCoroutine(ResetSaveUITimer());

    }

    public IEnumerator ResetSaveUITimer()
    {
        yield return new WaitForSeconds(0.50f);
        savePanel.GetComponent<RectTransform>().localPosition = new Vector2(-1920, -50);
    }

}
