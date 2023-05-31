using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
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
    public Button submitFoodAndIngredientsButton; // New button for submitting food and ingredients

    private const float reductionAmount = 0.3f;
    private const float ingredientIncreaseAmount = 0.06f;
    private const float requiredWaterAmount = 500f;

    private float hungerLevel = 0.7f;
    private float thirstLevel = 0.7f;
    private bool isHungerFull = false;
    private bool isThirstFull = false;
    private bool hasSubmittedFood = false;
    private bool isFoodInputFrozen = false;
    private string currentFood = "";
    private int ingredientCount = 0;
    private DateTime currentDateTime;
    private DateTime currentDay;

    private void Start()
    {
        submitFoodButton.onClick.AddListener(SubmitFoodInput);

        for (int i = 0; i < submitIngredientButtons.Length; i++)
        {
            int index = i;
            submitIngredientButtons[index].onClick.AddListener(() => SubmitIngredientInput(index));
        }

        submitWaterButton.onClick.AddListener(SubmitWaterInput);

        hungerBar.value = hungerLevel;
        thirstBar.value = thirstLevel;

        UpdateCurrentDay();

        submitFoodAndIngredientsButton.onClick.AddListener(SubmitFoodAndIngredientsInput); // Add listener for new button
    }

    private void Update()
    {
        // Track real-time and reduce hunger and thirst levels accordingly
        currentDateTime = DateTime.Now;
        ReduceLevelsAtSpecifiedTimes();
    }

    private void ReduceLevelsAtSpecifiedTimes()
    {
        if (currentDateTime.Hour == 9 && currentDateTime.Minute == 0)
        {
            ReduceHungerAndThirstLevels();
        }
        else if (currentDateTime.Hour == 14 && currentDateTime.Minute == 0)
        {
            ReduceHungerAndThirstLevels();
        }
        else if (currentDateTime.Hour == 20 && currentDateTime.Minute == 0)
        {
            ReduceHungerAndThirstLevels();
        }
    }

    private void ReduceHungerAndThirstLevels()
    {
        float reductionAmountPerHour = 0.1f;

        hungerLevel -= reductionAmountPerHour;
        thirstLevel -= reductionAmountPerHour;

        if (hungerLevel <= 0f)
        {
            hungerLevel = 0f;
            DisplayHungerPopup();
        }

        if (thirstLevel <= 0f)
        {
            thirstLevel = 0f;
            DisplayThirstPopup();
        }

        UpdateHungerAndThirstBars();
    }

    private void UpdateCurrentDay()
    {
        currentDay = currentDateTime.Date;
    }

    private void SubmitFoodInput()
    {
        string food = foodInputField.text;

        if (!string.IsNullOrEmpty(food))
        {
            hasSubmittedFood = true;
            currentFood = food;
            ingredientCount = 0;
            IncreaseHungerLevel();
            isFoodInputFrozen = true;
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

            if (ingredientCount >= 4)
            {
                hasSubmittedFood = false;
                ClearFoodInputField();
            }
        }
    }

    private void SubmitWaterInput()
    {
        float waterAmount;
        if (float.TryParse(waterInputField.text, out waterAmount))
        {
            if (waterAmount <= 500f)
            {
                float waterIncrease = (waterAmount / requiredWaterAmount) * reductionAmount;
                thirstLevel += waterIncrease;

                if (thirstLevel >= 1f)
                {
                    thirstLevel = 1f;
                    DisplayThirstPopup();
                }

                UpdateHungerAndThirstBars();
            }
            else
            {
                // Handle value exceeding the limit (e.g., show an error message)
            }
        }
        else
        {
            // Handle invalid input (non-numeric or empty)
        }

        waterInputField.text = "";
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

    private void UpdateHungerAndThirstBars()
    {
        hungerBar.value = hungerLevel;
        thirstBar.value = thirstLevel;
    }

    private void SubmitFoodAndIngredientsInput()
    {
        if (!hasSubmittedFood)
        {
            string food = foodInputField.text;

            if (!string.IsNullOrEmpty(food))
            {
                hasSubmittedFood = true;
                currentFood = food;
                ingredientCount = 0;
                isFoodInputFrozen = true;
                ClearFoodInputField();
            }
        }
        else
        {
            string ingredient = ingredientInputFields[ingredientCount].text;

            if (!string.IsNullOrEmpty(ingredient) && ingredientCount < 4)
            {
                IncreaseHungerLevel();
                ClearIngredientInputField(ingredientCount);
                ingredientCount++;

                if (ingredientCount >= 4)
                {
                    hasSubmittedFood = false;
                    isFoodInputFrozen = false;
                    ClearFoodInputField();
                }
            }
        }
    }
}
