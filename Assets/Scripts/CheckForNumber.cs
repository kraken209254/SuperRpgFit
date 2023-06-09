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
        Debug.Log("Drag Begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
    }
   
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Ended");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.transform.parent.tag);
        switch (eventData.pointerCurrentRaycast.gameObject.transform.parent.tag)
        {
            case "Hora1":
                hora1 = int.Parse(eventData.pointerCurrentRaycast.gameObject.GetComponent<TextMeshProUGUI>().text);
                break;
            case "Hora2":
                hora2 = int.Parse(eventData.pointerCurrentRaycast.gameObject.GetComponent<TextMeshProUGUI>().text);
                Debug.Log(hora2);

                break;
            case "Min1":
                min1 = int.Parse(eventData.pointerCurrentRaycast.gameObject.GetComponent<TextMeshProUGUI>().text);
                Debug.Log(min1);
                break;
            case "Min2":
                min2 = int.Parse(eventData.pointerCurrentRaycast.gameObject.GetComponent<TextMeshProUGUI>().text);
                Debug.Log(min2);
                break;
            case "Sec1":
                sec1 = int.Parse(eventData.pointerCurrentRaycast.gameObject.GetComponent<TextMeshProUGUI>().text);
                Debug.Log(sec1);

                break;
            case "Sec2":
                sec2 = int.Parse(eventData.pointerCurrentRaycast.gameObject.GetComponent<TextMeshProUGUI>().text);
                Debug.Log(sec2);

                break;

        }
       
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exit");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Mouse Up");
    }
}
