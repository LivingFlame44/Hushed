using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Item[] items;
    private string folderPath = "Items"; // Folder name inside the Assets/Resources folder

    public List<Item> inventory = new List<Item>();


    public GameObject itemContainerPanel;

    public List<GameObject> activeItemBtnList, inactiveItemBtnList = new List<GameObject>();

    public GameObject inventoryItemBtnPrefab;

    public GameObject itemInfoPanel;
    public Image itemInfoImage;
    public TextMeshProUGUI itemInfoName, itemInfoDesc;
    public Button itemInfoInspectBtn;

    

    void Awake()
    {
        items = Resources.LoadAll<Item>(folderPath);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowItemInfo(Item item)
    {
        itemInfoPanel.SetActive(true);
        itemInfoImage.sprite = item.itemImage;
        itemInfoName.text = item.name;
        itemInfoDesc.text = item.itemInfo;

        itemInfoInspectBtn.onClick.RemoveAllListeners();
        itemInfoInspectBtn.onClick.AddListener(() => NotebookManager.instance.newObjectiveTypes.InspectObject(item.itemInspectPrefab));
        itemInfoInspectBtn.onClick.AddListener(NotebookManager.instance.CloseNotebook);
    }

    public void UnlockItem(int itemID)
    {
        Item item = items[itemID];
        inventory.Add(item);

        itemContainerPanel.SetActive(false);

        GameObject itemBtn = InstantiateItemButton();

        itemBtn.GetComponent<InventoryItemButton>().item = item;
        itemBtn.GetComponent<Image>().sprite = item.itemImage;
    }

    public void RemoveItem(int itemID)
    {
        Item itemToRemove = items[itemID];  
        inventory.Remove(itemToRemove);

        for(int i = 0; i < activeItemBtnList.Count; i++)
        {
            if (activeItemBtnList[i].GetComponent<InventoryItemButton>().item == itemToRemove)
            {
                GameObject btnToRemove = activeItemBtnList[i];
                inactiveItemBtnList.Add(btnToRemove);
                activeItemBtnList.Remove(btnToRemove);
                btnToRemove.SetActive(false);
                break;
            }
        }
    }

    public GameObject InstantiateItemButton()
    {
        GameObject itemBtn;
        if (inactiveItemBtnList.Count == 0)
        {
            itemBtn = Instantiate(inventoryItemBtnPrefab, itemContainerPanel.transform);
            activeItemBtnList.Add(itemBtn);
        }
        else
        {
            itemBtn = inactiveItemBtnList[0];
            activeItemBtnList.Add(itemBtn);
            inactiveItemBtnList.Remove(itemBtn);
            itemBtn.SetActive(true);
            itemBtn.transform.SetAsLastSibling();
        }

        return itemBtn;
        
    }
}
