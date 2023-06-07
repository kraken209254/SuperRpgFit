using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Btns : MonoBehaviour
{
    public Button Medicinas;
    public Button MuyMal;
    public Button Mal;
    public Button Regular;
    public Button Bien;
    public Button MuyBien;

    public Button[] Botones;

    // Start is called before the first frame update
    void Start()
    {
        Medicinas.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("Medicamentos");
    }

    public void changeMM(){
        MuyMal.GetComponent<Image>().color = Color.green;
    }

    public void changeM(){
        Mal.GetComponent<Image>().color = Color.green;
    }

    public void changeR(){
        Regular.GetComponent<Image>().color = Color.green;
    }

    public void changeB(){
        Bien.GetComponent<Image>().color = Color.green;
    }

    public void changeMB(){
        MuyBien.GetComponent<Image>().color = Color.green;
    }

    public void ChangeButton(Button thisButton)
    {
        foreach (Button individual in Botones)
        {
            if(thisButton != individual){individual.GetComponent<Image>().color = Color.white;}
            else{individual.GetComponent<Image>().color = Color.green;}
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
