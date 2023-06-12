using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FillCircleOverTime : MonoBehaviour
{
	public Image cooldown;
	public static bool coolingDown;
	public static float timer;
	public float timer2;
	public TextMeshProUGUI textTime;
	public GameObject[] arrayDiasSemana;
	int dayOfWeekNumberUnity = 0;


	// Update is called once per frame

	private void Start()
    {
		cooldown.fillAmount = 0;
		timer2 = timer;

		DateTime currDate = DateTime.Today;
		DayOfWeek currDayofWeek = currDate.DayOfWeek;
		dayOfWeekNumberUnity = ((int)currDayofWeek + 6) % 7 + 1;

	}
	void Update()
	{
		if (coolingDown == true)
		{

			//Reduce fill amount over 30 seconds
			cooldown.fillAmount += 1.0f / timer2 * Time.deltaTime;
			timer -= Time.deltaTime;

			int seconds = (int)(timer % 60);
			int minutes = (int)(timer / 60) % 60;
			int hours = (int)(timer / 3600) % 24;

			string timerString = string.Format("{0:0}:{1:00}:{2:00}", hours, minutes, seconds);
			textTime.text = timerString;

			if(timer <= 0)
            {
				coolingDown = false;
            }

            switch (dayOfWeekNumberUnity)
            {
				case 1:
					arrayDiasSemana[0].GetComponent<Image>().fillAmount += 1.0f / 86400 * Time.deltaTime;
					break;
            }

		}

		
	}
}
