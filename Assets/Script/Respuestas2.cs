using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Respuestas2 : MonoBehaviour
{
    public TMP_InputField Texto21_Respuesta;
    public TMP_InputField Texto22_Respuesta;
    public TMP_InputField HrConsumo1;
    public TMP_InputField HrConsumo2;
    public TMP_InputField HrConsumo3;
    public TMP_InputField Texto25_Respuesta;
    public TMP_InputField Texto25H_Respuesta;

    private void Respuesta(){
        Debug.Log(Texto21_Respuesta.text);
        Debug.Log(Texto22_Respuesta.text);
        Debug.Log(HrConsumo1.text);
        Debug.Log(HrConsumo2.text);
        Debug.Log(HrConsumo3.text);
        Debug.Log(Texto25_Respuesta.text);
        Debug.Log(Texto25H_Respuesta.text);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
