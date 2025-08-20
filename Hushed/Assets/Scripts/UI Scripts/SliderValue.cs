using UnityEngine;
using UnityEngine.UI;
using TMPro; // Use this if you're using TextMeshPro

public class SliderValueDisplay : MonoBehaviour
{
    [Header("UI References")]
    public Slider slider;           // The slider
    public TMP_Text valueText;      // If using TextMeshPro
    // public Text valueText;       // Uncomment if using legacy UI Text

    private void Start()
    {
        if (slider != null)
        {
            slider.minValue = 0;
            slider.maxValue = 100;
            slider.wholeNumbers = true; // Only allow whole numbers
            slider.onValueChanged.AddListener(UpdateValueText);
            UpdateValueText(slider.value); // Show initial value
        }
    }

    private void UpdateValueText(float value)
    {
        if (valueText != null)
            valueText.text = value.ToString("0"); // no decimals
    }
}
