using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider hungerBar;
    public Slider thirstBar;
    public InputField foodInputField;
    public InputField[] ingredientInputFields;
    public InputField waterInputField;
    public Button submitFoodButton;
    public Button[] submitIngredientButtons;
    public Button submitWaterButton;
    public GameObject hungerPopupPanel;
    public GameObject thirstPopupPanel;

    private const float reductionAmount = 0.3f;
    private const float ingredientIncreaseAmount = 0.06f;
    private const float requiredWaterAmount = 500f;

    private float hungerLevel = 0.7f;
    private float thirstLevel = 0.7f;
    private bool isHungerFull = false;
    private bool isThirstFull = false;
    private bool hasSubmittedFood = false;
    private int ingredientCount = 0;

    private void Start()
    {
        // Set up button click listeners
        submitFoodButton.onClick.AddListener(SubmitFoodInput);

        for (int i = 0; i < submitIngredientButtons.Length; i++)
        {
            int index = i;
            submitIngredientButtons[index].onClick.AddListener(() => SubmitIngredientInput(index));
        }

        submitWaterButton.onClick.AddListener(SubmitWaterInput);

        // Set initial hunger and thirst levels
        hungerBar.value = hungerLevel;
        thirstBar.value = thirstLevel;
    }

    private void Update()
    {
        // Track real-time and reduce hunger and thirst levels accordingly
        DateTime currentTime = DateTime.Now;
        ReduceLevelsAtSpecifiedTimes(currentTime);
    }

    private void ReduceLevelsAtSpecifiedTimes(DateTime currentTime)
    {
        if (currentTime.Hour == 9 && currentTime.Minute == 0)
        {
            ReduceHungerAndThirstLevels();
        }
        else if (currentTime.Hour == 14 && currentTime.Minute == 0)
        {
            ReduceHungerAndThirstLevels();
        }
        else if (currentTime.Hour == 20 && currentTime.Minute == 0)
        {
            ReduceHungerAndThirstLevels();
        }
    }

    private void ReduceHungerAndThirstLevels()
    {
        hungerLevel -= reductionAmount;
        thirstLevel -= reductionAmount;
        UpdateHungerAndThirstBars();

        if (hungerLevel <= 0)
        {
            hungerLevel = 0;
            DisplayHungerPopup();
        }

        if (thirstLevel <= 0)
        {
            thirstLevel = 0;
            DisplayThirstPopup();
        }
    }

    private void UpdateHungerAndThirstBars()
    {
        hungerBar.value = hungerLevel;
        thirstBar.value = thirstLevel;
    }

    private void SubmitFoodInput()
    {
        string food = foodInputField.text;

        if (!string.IsNullOrEmpty(food))
        {
            hasSubmittedFood = true;
            ingredientCount = 0;
            IncreaseHungerLevel();
            ClearFoodInputField();
            ClearIngredientInputFields();
        }
    }

    private void SubmitIngredientInput(int index)
    {
        string ingredient = ingredientInputFields[index].text;

        if (hasSubmittedFood && !string.IsNullOrEmpty(ingredient) && ingredientCount < 4)
        {
            IncreaseHungerLevel();
            ClearIngredientInputField(index);
            ingredientCount++;
        }
    }

    private void SubmitWaterInput()
    {
        float waterAmount = float.Parse(waterInputField.text);

        if (waterAmount > 0)
        {
            float waterIncrease = (waterAmount / requiredWaterAmount) * ingredientIncreaseAmount * 5;
            thirstLevel += waterIncrease;

            if (thirstLevel >= 1f)
            {
                thirstLevel = 1f;
                DisplayThirstPopup();
            }

            UpdateHungerAndThirstBars();
            ClearWaterInputField();
        }
    }

    private void IncreaseHungerLevel()
    {
        float ingredientIncrease = ingredientIncreaseAmount;
        hungerLevel += ingredientIncrease;

        if (hungerLevel >= 1f)
        {
            hungerLevel = 1f;
            DisplayHungerPopup();
        }

        UpdateHungerAndThirstBars();
    }

    private void ClearFoodInputField()
    {
        foodInputField.text = "";
    }

    private void ClearIngredientInputFields()
    {
        foreach (InputField inputField in ingredientInputFields)
        {
            inputField.text = "";
        }
    }

    private void ClearIngredientInputField(int index)
    {
        ingredientInputFields[index].text = "";
    }

    private void ClearWaterInputField()
    {
        waterInputField.text = "";
    }

    private void DisplayHungerPopup()
    {
        hungerPopupPanel.SetActive(true);
        Invoke(nameof(HideHungerPopup), 5f);
    }

    private void DisplayThirstPopup()
    {
        thirstPopupPanel.SetActive(true);
        Invoke(nameof(HideThirstPopup), 5f);
    }

    private void HideHungerPopup()
    {
        hungerPopupPanel.SetActive(false);
    }

    private void HideThirstPopup()
    {
        thirstPopupPanel.SetActive(false);
    }
}

