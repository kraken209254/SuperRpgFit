using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button returnmenu;

    void Start()
    {
        returnmenu.onClick.AddListener(Return);
    }

    void Return()
    {
        SceneManager.LoadScene("");
    }

}
