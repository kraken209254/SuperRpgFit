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

    private const float reductionAmount = 0.31f;
    private const float ingredientIncreaseAmount = 0.06f;
    private const float requiredWaterAmount = 500f;
    private const float maxHungerLevel = 1f;
    private const float maxThirstLevel = 1f;

    private float hungerLevel = 0.71f;
    private float thirstLevel = 0.71f;
    private bool isHungerFull = false;
    private bool isThirstFull = false;
    private bool hasSubmittedFood = false;
    private string currentFood = "";
    private int ingredientCount = 0;
    private DateTime currentDateTime;
    private DateTime currentDay;

    [SerializeField]
    private Text hungerText;

    [SerializeField]
    private Text thirstText;

    [SerializeField]
    private Text currentTimeText;

    [SerializeField]
    private Text mealText;

    private bool hasReducedLevels = false;

    private void Start()
    {
        submitFoodButton.onClick.AddListener(SubmitFoodInput);

        for (int i = 0; i < submitIngredientButtons.Length; i++)
        {
            int index = i;
            submitIngredientButtons[index].onClick.AddListener(() => SubmitIngredientInput(index));
        }

        submitWaterButton.onClick.AddListener(SubmitWaterInput);

        UpdateHungerAndThirstBars(); // Update the bars initially
        UpdateHungerAndThirstText(); // Update the text initially

        UpdateCurrentDay();
        DisplayMealText();
    }

    private void Update()
    {
        currentDateTime = DateTime.Now;

        if (currentDateTime.Minute == 0)
        {
            ReduceLevelsAtSpecifiedTimes();
        }

        currentTimeText.text = currentDateTime.ToString("HH:mm:ss");

        DisplayMealText(); // Moved this line to ensure it gets called every frame
    }

    private void ReduceLevelsAtSpecifiedTimes()
    {
        if (currentDateTime.Hour == 9 && !hasReducedLevels)
        {
            ReduceHungerAndThirstLevels();
            Debug.Log($"Reduced levels at 09:00. Current time: {currentDateTime.ToString("HH:mm:ss")}");
        }
        else if (currentDateTime.Hour == 14 && !hasReducedLevels)
        {
            ReduceHungerAndThirstLevels();
            Debug.Log($"Reduced levels at 14:00. Current time: {currentDateTime.ToString("HH:mm:ss")}");
        }
        else if (currentDateTime.Hour == 20 && !hasReducedLevels)
        {
            ReduceHungerAndThirstLevels();
            Debug.Log($"Reduced levels at 20:00. Current time: {currentDateTime.ToString("HH:mm:ss")}");
        }
    }

    private void ReduceHungerAndThirstLevels()
    {
        float reductionAmountPerHour = 1f - reductionAmount;

        hungerLevel *= reductionAmountPerHour;
        thirstLevel *= reductionAmountPerHour;

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
        UpdateHungerAndThirstText();

        hasReducedLevels = true;
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
                ClearIngredientInputFields();
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
                UpdateHungerAndThirstText();
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
        UpdateHungerAndThirstText();
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

    private void UpdateHungerAndThirstText()
    {
        hungerText.text = $"{(int)(hungerLevel * 100)}/{(int)(maxHungerLevel * 100)}";
        thirstText.text = $"{(int)(thirstLevel * 100)}/{(int)(maxThirstLevel * 100)}";
    }

    private void DisplayMealText()
    {
        TimeSpan currentTime = currentDateTime.TimeOfDay;

        TimeSpan breakfastTime = new TimeSpan(9, 0, 0);
        TimeSpan lunchTime = new TimeSpan(14, 0, 0);
        TimeSpan dinnerTime = new TimeSpan(20, 0, 0);

        if (currentTime >= breakfastTime && currentTime < lunchTime)
        {
            mealText.text = "Desayuno";
        }
        else if (currentTime >= lunchTime && currentTime < dinnerTime)
        {
            mealText.text = "Almuerzo";
        }
        else if (currentTime >= dinnerTime || currentTime < breakfastTime)
        {
            mealText.text = "Cena";
        }
        else
        {
            mealText.text = "";
        }
    }

}