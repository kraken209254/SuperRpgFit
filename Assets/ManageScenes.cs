using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loadEjercicio()
    {
        SceneManager.LoadScene("TemplateEjercicios");
    }
    public void loadMedicina()
    {
        SceneManager.LoadScene("Medicamentos");
    }
    public void loadDormir()
    {
        SceneManager.LoadScene("Dormir");
    }
    public void loadAlimentacion()
    {
        SceneManager.LoadScene("Hungerthirst");
    }
    public void loadReturn()
    {
        SceneManager.LoadScene("Menu");
    }
}
