using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
public class SettingsManager : MonoBehaviour
{
    Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private string currentRefreshRate;
    public TMP_Dropdown resDropdown;

    public Slider brightnessSlider;
    public PostProcessProfile brightness;
    public PostProcessLayer layer;

    AutoExposure exposure;
    // Start is called before the first frame update
    void Start()
    {
        brightness.TryGetSettings(out exposure);
        AdjustBrightness(brightnessSlider.value);

        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();
        currentRefreshRate = Screen.currentResolution.refreshRateRatio.ToString();
        resDropdown.ClearOptions();

        

        for(int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRateRatio.ToString() == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> resStrings = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < filteredResolutions.Count; i++)
        {
            string option = filteredResolutions[i].width.ToString() + " x " + filteredResolutions[i].height.ToString();
            resStrings.Add(option);

            if (filteredResolutions[i].width == Screen.currentResolution.width && filteredResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        

        resDropdown.AddOptions(resStrings);
        resDropdown.value = currentResolutionIndex;
        resDropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdjustBrightness(float value)
    {
        if(value != 0)
        {
            exposure.keyValue.value = value;
        }
        else
        {
            exposure.keyValue.value = .05f;
        }
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetResolution(int resIndex)
    {
        Resolution resolution = filteredResolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
