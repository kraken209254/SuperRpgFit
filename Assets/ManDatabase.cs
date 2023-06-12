

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using System.Net;
using System.Text;

public class ManDatabase : MonoBehaviour
{

    readonly string postURL = "http://localhost:5500/api/ejercicio/guardar";


    public void SendEjercicio(string ejercicio, string tiempo, string dia, string idPaciente)
    {
        // Crea un objeto JSON con los datos de la comida, ingredientes y agua
        string json = "{\"Ejercicio\": \"" + ejercicio + "\", \"Tiempo\": \"" + tiempo + "\", \"dia\": \"" + dia + "\", \"IDPaciente\": \"" + idPaciente  + "\"" + "}";
        Debug.Log(json);
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

    }

}






