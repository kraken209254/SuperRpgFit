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
        GameObject instance = Instantiate(prefab, contentBox.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
