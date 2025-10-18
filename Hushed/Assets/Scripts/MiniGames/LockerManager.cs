using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class LockerManager : MonoBehaviour
{
    public int[] currentCode;

    
    public int[] passcode1, passcode2, passcode3;

    public int correctLockerNum;

    public int selectedLockNum;

    public GameObject[]highlightImages, openedLockerImages;

    public GameObject unlockButton;

    public UnityEvent correctLockerEvent;
    public UnityEvent wrongLockerEvent;
    public UnityEvent wrongPasscodeEvent;

    public TextMeshProUGUI[] codesText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseNumber(int index)
    {
        if (currentCode[index] == 9)
        {
            currentCode[index] = 0;
        }
        else
        {
            currentCode[index] = currentCode[index] + 1;
        }

        codesText[index].text = currentCode[index].ToString();
    }

    public void DecreaseNumber(int index)
    {       
        if (currentCode[index] == 0)
        {
            currentCode[index] = 9;
        }
        else
        {
            currentCode[index] = currentCode[index] - 1;
        }

        codesText[index].text = currentCode[index].ToString();
    }

    public void TryUnlock()
    {
        if (CheckPasscode())
        {
            if(selectedLockNum == correctLockerNum)
            {
                correctLockerEvent.Invoke();
                openedLockerImages[selectedLockNum].SetActive(true);
                unlockButton.SetActive(false);
            }
            else
            {
                wrongLockerEvent.Invoke();
                openedLockerImages[selectedLockNum].SetActive(true);
                unlockButton.SetActive(false);
            }
        }
        else
        {
            wrongPasscodeEvent.Invoke();
        }
    }

    public bool CheckPasscode()
    {
        switch (selectedLockNum)
        {
            case 0:
                for (int i = 0; i < currentCode.Length; i++)
                {
                    if (currentCode[i] != passcode1[i])
                    {
                        return false;
                    }
                }
                break;
            case 1:
                for (int i = 0; i < currentCode.Length; i++)
                {
                    if (currentCode[i] != passcode2[i])
                    {
                        return false;
                    }
                }
                break;
            case 2:
                for (int i = 0; i < currentCode.Length; i++)
                {
                    if (currentCode[i] != passcode3[i])
                    {
                        return false;
                    }
                }
                break;
        }
        
        return true;
    }

    public void SelectLocker(int index)
    {
        UnselectAll();
        selectedLockNum = index;
        highlightImages[index].SetActive(true);
        unlockButton.SetActive(true);
    }

    public void UnselectAll()
    {
        for (int i = 0; i < currentCode.Length; i++)
        {
            currentCode[i] = 0;
            highlightImages[i].SetActive(false);
            codesText[i].text = currentCode[i].ToString();
        }
    }
}
