using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CheckForNumber : MonoBehaviour, IPointerDownHandler, IPointerClickHandler,
    IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public static int hora1;
    public static int hora2;
    public static int min1;
    public static int min2;
    public static int sec1;
    public static int sec2;
    public GameObject[] TextosHoras;
    public List<RaycastResult> RaycastResults;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.tag == "Selector")
        {
            Debug.Log("Collision");
            switch (gameObject.transform.parent.tag)
            {
                case "Hora1":
                    hora1 = int.Parse(gameObject.GetComponent<TextMeshProUGUI>().text);
                    break;
                case "Hora2":
                    Debug.Log(hora2);
                    hora2 = int.Parse(gameObject.GetComponent<TextMeshProUGUI>().text);

                    break;
                case "Min1":
                    min1 = int.Parse(gameObject.GetComponent<TextMeshProUGUI>().text);

                    break;
                case "Min2":
                    min2 = int.Parse(gameObject.GetComponent<TextMeshProUGUI>().text);

                    Debug.Log(min2);
                    break;
                case "Sec1":
                    sec1 = int.Parse(gameObject.GetComponent<TextMeshProUGUI>().text);

                    Debug.Log(sec1);

                    break;
                case "Sec2":
                    sec2 = int.Parse(gameObject.GetComponent<TextMeshProUGUI>().text);

                    Debug.Log(sec2);

                    break;

            }
        }
    }
    public void Update()
    {
        if(gameObject.tag == "Textos")
        {
            TextosHoras[0].GetComponent<TextMeshProUGUI>().text = hora1.ToString();
            TextosHoras[1].GetComponent<TextMeshProUGUI>().text = hora2.ToString();
            TextosHoras[3].GetComponent<TextMeshProUGUI>().text = min1.ToString();
            TextosHoras[4].GetComponent<TextMeshProUGUI>().text = min2.ToString();
            TextosHoras[5].GetComponent<TextMeshProUGUI>().text = sec1.ToString();
            TextosHoras[6].GetComponent<TextMeshProUGUI>().text = sec2.ToString();
        }


    }


    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
    }
   
    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       
       
    }


    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
