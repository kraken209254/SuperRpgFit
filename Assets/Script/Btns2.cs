using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Btns2 : MonoBehaviour
{
    public Button Descanso;
    public Button Si;
    public Button No;

    public Button[] Btns;
    public GameObject prefab, contentBox;

    public float distanciaRecorrida,distanciaRecorrida2, distanciaRecorridaAgregar;

    public GameObject agregarGrupo;
    private Vector3 sasvedMedicamentoPosition;

    // Start is called before the first frame update
    void Start()
    {
         Descanso.onClick.AddListener(Cambio2);
    }

    void Cambio2()
    {
        SceneManager.LoadScene("Dormir");
    }

    public void changeSi(){
        Si.GetComponent<Image>().color = Color.green;
    }

    public void changeNo(){
        No.GetComponent<Image>().color = Color.green;
    }

    public void ChangeSi_No(Button Si_No){
        foreach (Button One in Btns)
        {
             if(Si_No != One){One.GetComponent<Image>().color = Color.white;}
            else{One.GetComponent<Image>().color = Color.green;}
        }
    }

    public void AgrMedicamentos()
    {
        GameObject newPrefab = Instantiate(prefab);

        newPrefab.transform.SetParent(contentBox.transform);

        if(sasvedMedicamentoPosition != Vector3.zero)
        {
            newPrefab.GetComponent<RectTransform>().anchoredPosition = new Vector3(sasvedMedicamentoPosition.x, sasvedMedicamentoPosition.y - distanciaRecorrida, sasvedMedicamentoPosition.z);
        }
        else
        {newPrefab.GetComponent<RectTransform>().anchoredPosition = new Vector3(sasvedMedicamentoPosition.x, sasvedMedicamentoPosition.y - distanciaRecorrida2, sasvedMedicamentoPosition.z);}
        

        sasvedMedicamentoPosition = newPrefab.GetComponent<RectTransform>().anchoredPosition;

        agregarGrupo.GetComponent<RectTransform>().anchoredPosition = new Vector2(agregarGrupo.GetComponent<RectTransform>().anchoredPosition.x, agregarGrupo.GetComponent<RectTransform>().anchoredPosition.y - distanciaRecorridaAgregar);

        newPrefab.GetComponent<Respuestas2>().Texto21_Respuesta.text = null;
        newPrefab.GetComponent<Respuestas2>().Texto22_Respuesta.text = null;
        newPrefab.GetComponent<Respuestas2>().HrConsumo1.text = null;
        newPrefab.GetComponent<Respuestas2>().HrConsumo2.text = null;
        newPrefab.GetComponent<Respuestas2>().HrConsumo3.text = null;
        newPrefab.GetComponent<Respuestas2>().Texto25_Respuesta.text = null;
        newPrefab.GetComponent<Respuestas2>().Texto25H_Respuesta.text = null;

        Si.GetComponent<Image>().color = Color.white;
        No.GetComponent<Image>().color = Color.white;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
