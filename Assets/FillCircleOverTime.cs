using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillCircleOverTime : MonoBehaviour
{
	public Image cooldown;
	public bool coolingDown;
	public float waitTime = 30.0f;

    // Update is called once per frame

    private void Start()
    {
		cooldown.fillAmount = 0; 

	}
    void Update()
	{
		if (coolingDown == true)
		{
			//Reduce fill amount over 30 seconds
			cooldown.fillAmount += 1.0f / waitTime * Time.deltaTime;
		}
	}
}
