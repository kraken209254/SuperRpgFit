using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SwtichScreens : MonoBehaviour
{

    public GameObject primeraScreen;
    public GameObject segundaScreen;
    public GameObject MenuScreen;
    public GameObject MenuEjercicio;
    public GameObject deporteNombre;
    public float tiempoFinal;
    TextMeshProUGUI textDeporte;

    private void Start()
    {
        textDeporte = deporteNombre.GetComponent<TextMeshProUGUI>();
    }
    public void SwitchScreen()
    {
        tiempoFinal = CheckForNumber.sec2 + CheckForNumber.sec1 * 10 + CheckForNumber.min2 * 60 + CheckForNumber.min1 * 10 * 60 + CheckForNumber.hora2 * 3600 + CheckForNumber.hora1 * 10 * 3600;
        primeraScreen.SetActive(false);
        segundaScreen.SetActive(true);
        FillCircleOverTime.timer = tiempoFinal;
        FillCircleOverTime.coolingDown = true;


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
