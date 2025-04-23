using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;

public class EvidenceCraftingManager : MonoBehaviour
{
    public static EvidenceCraftingManager instance;
    public int[] evidenceInSlotList = {0,0,0};

    public GameObject[] evidenceSlotList;

    private string folderPath = "Recipe";
    public Recipe[] recipes;

    public GameObject newEvidenceBtn, duplicateEvidenceErrorText;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Awake()
    {
        recipes = Resources.LoadAll<Recipe>(folderPath);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEvidence(Evidence evidence)
    {
        for(int i = 0; i < evidenceInSlotList.Length; i++)
        {
            if (evidenceInSlotList[i] == 0)
            {
                evidenceInSlotList[i] = evidence.evidenceID;
                break;
            }
        }
    }

    public void RemoveEvidence(Evidence evidence)
    {
        for (int i = 0; i < evidenceInSlotList.Length; i++)
        {
            if (evidenceInSlotList[i] == evidence.evidenceID)
            {
                evidenceInSlotList[i] = 0;
                break;
            }
        }
    }

    public void CheckRecipe()
    {
        //loops through all recipes
        duplicateEvidenceErrorText.SetActive(false);
        newEvidenceBtn.SetActive(false);
        foreach (var recipe in recipes)
        {
            List<bool> isPresentlist = new List<bool>();

            //Checks if all requirement is present in slot list
            foreach(int evidenceNumRequirement in recipe.evidenceRecipe)
            {
                if (evidenceInSlotList.Contains(evidenceNumRequirement))
                {
                    isPresentlist.Add(true);
                }
                else
                {
                    isPresentlist.Add(false);
                    break;
                }
            }

            //Show Crafted Evidence
            if (AllTrueChecker(isPresentlist))
            {
                newEvidenceBtn.SetActive(true);
                ShowCraftedEvidence(recipe.craftedEvidence);
            }
        }
    }

    public bool AllTrueChecker(List<bool> list)
    {
        foreach (bool b in list)
        {
            if (b == false)
            {
                return false;
            }  
        }

        return true;
    }

    public void ShowCraftedEvidence(Evidence evidence)
    {
        newEvidenceBtn.SetActive(true);

        newEvidenceBtn.GetComponentInChildren<TextMeshProUGUI>().text = evidence.evidenceName;

        //assign new image
        newEvidenceBtn.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        newEvidenceBtn.GetComponentInChildren<Button>().onClick.AddListener(() => CollectCraftedEvidence(evidence));
    }

    public void CollectCraftedEvidence(Evidence evidence)
    {
        if (EvidenceManager.instance.ownedEvidenceList.Contains(evidence))
        {
            duplicateEvidenceErrorText.SetActive(true);
        }
        else
        {
            for (int i = 0; i < evidenceInSlotList.Length; i++)
            {
                evidenceInSlotList[i] = 0;
            }

            ReturnAllEvidence();
            newEvidenceBtn.SetActive(false);

            EvidenceManager.instance.UnlockNewEvidence(evidence.evidenceID);
        }
        
    }

    public void ReturnAllEvidence()
    {
        foreach(GameObject obj in evidenceSlotList) 
        {
            if(obj.transform.childCount > 0)
            {
                obj.GetComponentInChildren<EvidencePanel>().ReturnEvidence();
            }  
        }
    }
}
