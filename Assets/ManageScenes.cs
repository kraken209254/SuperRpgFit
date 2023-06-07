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
        SceneManager.LoadScene(4);
    }
    public void loadMedicina()
    {
        SceneManager.LoadScene(3);
    }
    public void loadDormir()
    {
        SceneManager.LoadScene(2);
    }
    public void loadAlimentacion()
    {
        SceneManager.LoadScene(1);
    }
    public void loadReturn()
    {
        SceneManager.LoadScene(0);
    }
}
