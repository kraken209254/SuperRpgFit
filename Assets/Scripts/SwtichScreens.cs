using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwtichScreens : MonoBehaviour
{

    public GameObject primeraScreen;
    public GameObject segundaScreen;
    public float tiempoFinal;

    public void SwitchScreen()
    {
        tiempoFinal = CheckForNumber.sec2 + CheckForNumber.sec1 * 10 + CheckForNumber.min2 * 60 + CheckForNumber.min1 * 10 * 60 + CheckForNumber.hora2 * 3600 + CheckForNumber.hora1 * 10 * 3600;
        primeraScreen.SetActive(false);
        segundaScreen.SetActive(true);
        FillCircleOverTime.timer = tiempoFinal;
        FillCircleOverTime.coolingDown = true;

    }
}
