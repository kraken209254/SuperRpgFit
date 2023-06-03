using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FillCircleOverTime : MonoBehaviour
{
	public Image cooldown;
	public static bool coolingDown;
	public static float timer;
	public TextMeshProUGUI textTime;

    // Update is called once per frame

    private void Start()
    {
		cooldown.fillAmount = 100; 

	}
    void Update()
	{
		if (coolingDown == true)
		{
			timer -= Time.deltaTime;
			//Reduce fill amount over 30 seconds
			cooldown.fillAmount -= 1.0f / timer * Time.deltaTime;
			int seconds = (int)(timer % 60);
			int minutes = (int)(timer / 60) % 60;
			int hours = (int)(timer / 3600) % 24;

			string timerString = string.Format("{0:0}:{1:00}:{2:00}", hours, minutes, seconds);
			textTime.text = timerString;

		}
	}
}
