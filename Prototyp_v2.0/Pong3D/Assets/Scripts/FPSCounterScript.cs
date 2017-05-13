using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounterScript : MonoBehaviour 
{
	public Text fpsText;
	int counter = 0;

	void Start (){}
	void Update () 
	{
		counter++;
		if(counter >= 60)
		{
			fpsText.text = (1.0/Time.deltaTime).ToString();
			counter = 0;
		}
	}
}
