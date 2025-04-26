using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class InspectItemPress : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    public bool isClicked;

    public int eventOnClickNum;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(isClicked == false)
        {
            InspectSystem.instance.inspectClickEvents[eventOnClickNum].Invoke();
            isClicked = true;
        }
        //var hits = new List<RaycastResult>();
        //EventSystem.current.RaycastAll(eventData, hits);

        //var hit = hits.FirstOrDefault(t => (t.gameObject.CompareTag("ClickableItem")));

        //if (hit.isValid)
        //{
        //    if (hit.gameObject.GetComponent<InspectItemPress>().isClicked == false)
        //    {
                

        //        hit.gameObject.GetComponent<InspectItemPress>().isClicked = true;
        //    }

        //}
    }

}
