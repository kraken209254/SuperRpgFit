using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using System.Net;
using System.Text;

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
    private const float ingredientIncreaseAmount = 0.30f;
    private const float requiredWaterAmount = 500f;
    private const float maxHungerLevel = 1f;
    private const float maxThirstLevel = 1f;

    private float hungerLevel = 1f;
    private float thirstLevel = 1f;
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

    readonly string postURL = "http://localhost:5500/api/comida/guardar";

    private void Start()
    {
        submitFoodButton.onClick.AddListener(SubmitFoodInput);
        //submitIngredientsButton.onClick.AddListener(SubmitIngredientsInput);
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
        string ingredient1 = ingredientInputField1.text;
        string ingredient2 = ingredientInputField2.text;
        string ingredient3 = ingredientInputField3.text;
        string ingredient4 = ingredientInputField4.text;

        if (!string.IsNullOrEmpty(food))
        {
            hasSubmittedFood = true;
            currentFood = food;
            ingredientCount = 4; // Se han ingresado los 4 ingredientes
            IncreaseHungerLevel(ingredientCount); // Aumenta el nivel de hambre
            ClearFoodInputField();
            //ClearIngredientInputFields();
            //SendFoodData(currentFood, ingredient1, ingredient2, ingredient3, ingredient4);
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

                SendFoodData(currentFood, ingredientInputField1.text, ingredientInputField2.text, ingredientInputField3.text, ingredientInputField4.text, waterAmount);
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

    private void SendFoodData(string comida, string ingrediente1, string ingrediente2, string ingrediente3, string ingrediente4, float waterAmount)
    {
        // Crea un objeto JSON con los datos de la comida, ingredientes y agua
        string json = "{\"Comida\": \"" + comida + "\", \"Ingrediente1\": \"" + ingrediente1 + "\", \"Ingrediente2\": \"" + ingrediente2 + "\", \"Ingrediente3\": \"" + ingrediente3 + "\", \"Ingrediente4\": \"" + ingrediente4 + "\", \"Agua\": " + waterAmount.ToString() + ", \"IDPaciente\": 7}";


        // Crea una solicitud POST a la API
        WebRequest request = WebRequest.Create(postURL);
        request.Method = "POST";
        request.ContentType = "application/json";

        // Convierte el objeto JSON en un arreglo de bytes
        byte[] data = Encoding.UTF8.GetBytes(json);

        // Establece los datos de la solicitud
        request.ContentLength = data.Length;
        using (Stream stream = request.GetRequestStream())
        {
            stream.Write(data, 0, data.Length);
        }

        // Envía la solicitud y obtén la respuesta
        using (WebResponse response = request.GetResponse())
        {
            // Maneja la respuesta de la API si es necesario
            // ...
        }

        ClearIngredientInputFields();
    }

    private void IncreaseHungerLevel(int ingredientCount)
    {
        float ingredientIncrease = ingredientIncreaseAmount * (ingredientCount + 1); // +1 para incluir la comida
        hungerLevel += ingredientIncrease;

        if (hungerLevel >= maxHungerLevel)
        {
            hungerLevel = maxHungerLevel;
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
        Invoke(nameof(HideHungerPopup), 1f);
    }

    private void DisplayThirstPopup()
    {
        thirstPopupPanel.SetActive(true);
        Invoke(nameof(HideThirstPopup), 1f);
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





