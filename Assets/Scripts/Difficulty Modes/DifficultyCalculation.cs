using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultyCalculation : MonoBehaviour
{
    [SerializeField] private List<DifficultySetting> settings = new List<DifficultySetting>();
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text rewardText;

    void Update()
    {
        CalculateCostAndReward();
    }

    void CalculateCostAndReward()
    {
        float totalCost = 0f;
        float totalRewardBoost = 1f;

        foreach(DifficultySetting setting in settings)
        {
            InputField settingField = setting.Field;
            float currentCost = 0f;
            float currentRewardBoost = 0f;

            if (settingField is SelectionField)
            {   
                SelectionField selectionField = settingField as SelectionField;
                bool enabled = selectionField.GetCurrentValue() == "ON";
                currentCost = enabled ? setting.MaxCost : 0f;
                currentRewardBoost = enabled ? setting.MaxRewardBoost : 0f;
            }

            if (settingField is SliderField)
            {
                SliderField sliderField = settingField as SliderField;
                float currentValue = sliderField.GetCurrentValue();
                float maxValue = sliderField.GetMaxValue();
                float minValue = sliderField.GetMinValue();
                float distanceFromMinValue = currentValue - minValue;
                float totalDistance = maxValue - minValue;
                float percentage = (distanceFromMinValue / totalDistance);
                currentCost = setting.MaxCost * percentage;
                currentRewardBoost = setting.MaxRewardBoost * percentage;
            }

            totalCost += currentCost;
            totalRewardBoost += currentRewardBoost;
        }

        totalCost = Mathf.Floor(totalCost);
        totalRewardBoost = Mathf.Round(totalRewardBoost * 10f) / 10f;

        costText.text = "" + totalCost;
        rewardText.text = "" + totalRewardBoost +"<color=#d95763>X</color>";
    }
}
