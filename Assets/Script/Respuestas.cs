using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Respuestas : MonoBehaviour
{
     public TMP_InputField Texto1_Respuesta;
     public TMP_InputField Texto2_Respuesta;
     public TextMeshProUGUI respuestaDeHorario;

     [HideInInspector] public float[] horariosHechosF;
     [HideInInspector] public float horarioCalculadoF;

      private void Respuesta(){
        Debug.Log(Texto1_Respuesta.text);
        Debug.Log(Texto2_Respuesta.text);
    }

    // Start is called before the first frame update
    void Start()
    {
        horariosHechosF = new float[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecuperarHora(TMP_InputField inpField)
    {
        float horarioFloat = 0;

        string[] horaArray = inpField.text.Split(':');

        float[] horaMinuto = { 0, 0 };

        if (float.TryParse(horaArray[0], out horaMinuto[0])) { }
        else
        {
            inpField.text = null;
        }

        if (float.TryParse(horaArray[1], out horaMinuto[1]))
        {
            if (horaMinuto[1] > 59)
            {
                horaMinuto[1] = 59;
                inpField.text = horaArray[0] + ":" + "59";
            }

        }
        else
        {
            inpField.text = null;
        }


        float minutos = horaMinuto[1] * 0.01666667f;
        horarioFloat = horaMinuto[0] + minutos;


        if (inpField.gameObject.name == "Texto1_Respuesta")
        {
            horariosHechosF[0] = horarioFloat;
        }
        if (inpField.gameObject.name == "Texto2_Respuesta")
        {
            horariosHechosF[1] = horarioFloat;
        }
        // Hora que te levantaste - Hora en la que dormiste
        //horarioCalculadoF = horariosHechosF[1] - horariosHechosF[0];

        if (horariosHechosF[1] >= horariosHechosF[0])
        {
            horarioCalculadoF = horariosHechosF[1] - horariosHechosF[0];
        }
        else
        {
            horarioCalculadoF = (24f - horariosHechosF[0]) + horariosHechosF[1];
        }

        string convertidoAString = "" + horarioCalculadoF;
        string[] splitPorPunto = convertidoAString.Split(".");


        float minutosCalculados = 0;

        if (splitPorPunto.Length == 2)
        {
            if (float.TryParse(splitPorPunto[1], out minutosCalculados))
            {
                minutosCalculados *= 6;
            }
        }
        else { minutosCalculados = 0; }

        string horarioCalculado = splitPorPunto[0] + ":" + minutosCalculados;
        Debug.Log("Horario Individual: " + horarioFloat);
        Debug.Log("Horario Calculado: " + horarioCalculado);
        respuestaDeHorario.text = horarioCalculado;
        }
}
