using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class SwtichScreens : MonoBehaviour
{

    public GameObject primeraScreen;
    public GameObject segundaScreen;
    public GameObject MenuScreen;
    public GameObject MenuEjercicio;
    public GameObject deporteNombre;
    public float tiempoFinal;
    TextMeshProUGUI textDeporte;
    public GameObject ManagerDb;
    private ManDatabase dbManagerScript;
    public GameObject TiempoDeporte;
    private FillCircleOverTime TiempoDeporteScript;
    public string formattedDate;

    private void Start()
    {
        textDeporte = deporteNombre.GetComponent<TextMeshProUGUI>();
        dbManagerScript = ManagerDb.GetComponent<ManDatabase>();
        TiempoDeporteScript = TiempoDeporte.GetComponent<FillCircleOverTime>();
        DateTime currentDate = DateTime.Today;
        formattedDate = currentDate.ToString("yyyy-MM-dd");
    }
    public void SwitchScreen()
    {
        tiempoFinal = CheckForNumber.sec2 + CheckForNumber.sec1 * 10 + CheckForNumber.min2 * 60 + CheckForNumber.min1 * 10 * 60 + CheckForNumber.hora2 * 3600 + CheckForNumber.hora1 * 10 * 3600;
        primeraScreen.SetActive(false);
        segundaScreen.SetActive(true);
        FillCircleOverTime.timer = tiempoFinal;
        FillCircleOverTime.coolingDown = true;
        dbManagerScript.SendEjercicio(textDeporte.text, tiempoFinal.ToString(), formattedDate, "4");


    }

    public void SwitchMenu_Sport()
    {
        primeraScreen.SetActive(true);
        MenuEjercicio.SetActive(false);
        deporteNombre.SetActive(true);

        textDeporte.text = gameObject.GetComponent<TextMeshProUGUI>().text;
    }

    public void returnHome()
    {
        SceneManager.LoadScene("Menu");
    }
}
