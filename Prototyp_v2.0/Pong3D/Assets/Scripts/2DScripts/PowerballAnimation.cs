using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerballAnimation : MonoBehaviour {
    public Sprite[] PowerBallSprites;
    int i=0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (i < 10 )
        {
            this.GetComponent<SpriteRenderer>().sprite = PowerBallSprites[i];
        }
        i++;
        if (i == 10)
        {
            i = 0;
        }

        
	}
}
