using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    public Slider hungerBar;
    public Slider thirstBar;
    public InputField foodInputField;
    public InputField ingredientInputField1;
    public InputField ingredientInputField2;
    public InputField ingredientInputField3;
    public InputField ingredientInputField4;
    public InputField waterInputField;
    public Button submitFoodButton;
    public Button submitIngredientsButton;
    public Button submitWaterButton;
    public GameObject hungerPopupPanel;
    public GameObject thirstPopupPanel;

    private const float reductionAmount = 0.30f;
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

    readonly string postURL = "http://localhost:8090/api/comida/guardar";

    private void Start()
    {
        submitFoodButton.onClick.AddListener(SubmitFoodInput);
        submitIngredientsButton.onClick.AddListener(SubmitIngredientsInput);
        submitWaterButton.onClick.AddListener(SubmitWaterInput);

        UpdateHungerAndThirstBars();
        UpdateHungerAndThirstText();

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

        DisplayMealText();
    }

    private void ReduceLevelsAtSpecifiedTimes()
    {
        if (currentDateTime.Hour == 9 && !hasReducedLevels)
        {
            ReduceHungerAndThirstLevels();
            //Debug.Log($"Reduced levels at 09:00. Current time: {currentDateTime.ToString("HH:mm:ss")}");
        }
        else if (currentDateTime.Hour == 14 && !hasReducedLevels)
        {
            ReduceHungerAndThirstLevels();
            //Debug.Log($"Reduced levels at 14:00. Current time: {currentDateTime.ToString("HH:mm:ss")}");
        }
        else if (currentDateTime.Hour == 20 && !hasReducedLevels)
        {
            ReduceHungerAndThirstLevels();
            //Debug.Log($"Reduced levels at 20:00. Current time: {currentDateTime.ToString("HH:mm:ss")}");
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
            StartCoroutine(SendFoodData(currentFood));
        }
    }

    private IEnumerator SendFoodData(string food)
    {
        WWWForm form = new WWWForm();
        form.AddField("comida", food);

        using (UnityWebRequest www = UnityWebRequest.Post(postURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error sending food data: " + www.error);
            }
            else
            {
                Debug.Log("Food data sent successfully");
            }
        }
    }


    private void SubmitIngredientsInput()
    {
        string ingredient1 = ingredientInputField1.text;
        string ingredient2 = ingredientInputField2.text;
        string ingredient3 = ingredientInputField3.text;
        string ingredient4 = ingredientInputField4.text;

        if (hasSubmittedFood && (!string.IsNullOrEmpty(ingredient1) || !string.IsNullOrEmpty(ingredient2) || !string.IsNullOrEmpty(ingredient3) || !string.IsNullOrEmpty(ingredient4)))
        {
            // Calcular la cantidad de ingredientes no vacíos
            int nonEmptyIngredientCount = 0;
            if (!string.IsNullOrEmpty(ingredient1))
                nonEmptyIngredientCount++;
            if (!string.IsNullOrEmpty(ingredient2))
                nonEmptyIngredientCount++;
            if (!string.IsNullOrEmpty(ingredient3))
                nonEmptyIngredientCount++;
            if (!string.IsNullOrEmpty(ingredient4))
                nonEmptyIngredientCount++;

            IncreaseHungerLevel(nonEmptyIngredientCount);
            ClearIngredientInputFields();
            StartCoroutine(SendIngredientsData(ingredient1, ingredient2, ingredient3, ingredient4));
            hasSubmittedFood = false;
            ClearFoodInputField();
        }
    }

    private IEnumerator SendIngredientsData(string ingredient1, string ingredient2, string ingredient3, string ingredient4)
    {
        WWWForm form = new WWWForm();
        form.AddField("ingrediente1", ingredient1);
        form.AddField("ingrediente2", ingredient2);
        form.AddField("ingrediente3", ingredient3);
        form.AddField("ingrediente4", ingredient4);

        using (UnityWebRequest www = UnityWebRequest.Post(postURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error sending ingredients data: " + www.error);
            }
            else
            {
                Debug.Log("Ingredients data sent successfully");
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

                StartCoroutine(SendWaterData(waterAmount));
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

    private IEnumerator SendWaterData(float waterAmount)
    {
        WWWForm form = new WWWForm();
        form.AddField("MLAgua", waterAmount.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(postURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error sending water data: " + www.error);
            }
            else
            {
                Debug.Log("Water data sent successfully");
            }
        }
    }

    private void IncreaseHungerLevel(int ingredientCount = 1)
    {
        float ingredientIncrease = ingredientIncreaseAmount * ingredientCount;
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
        ingredientInputField1.text = "";
        ingredientInputField2.text = "";
        ingredientInputField3.text = "";
        ingredientInputField4.text = "";
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
            mealText.text = "Comida";
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