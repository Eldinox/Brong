using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle2Script : MonoBehaviour 
{
	public GameObject upperBorder;
	public static float speed = 6f;
    public static float rightLimit;
    public static float leftLimit;
    public static float paddleSize = 0.75f;
    public static int player2Score = 0;

    void Start (){}
	
	void Update () 
	{
		leftLimit = -upperBorder.transform.localScale.x / 2 + paddleSize;
        rightLimit = upperBorder.transform.localScale.x / 2 - paddleSize;

        if (Input.GetKey(KeyCode.H) && transform.position.x < rightLimit - 0.1)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.K) && transform.position.x > leftLimit + 0.1)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
	}

     void OnCollisionEnter2D(Collision2D collision)
    {
        #region ITEMS
        //Big Paddle
        if (collision.transform.tag == "bigPaddle")
        {
            if (this.gameObject.transform.localScale.x <= 0.34f)
            {
                //GetComponent<AudioSource>().Play();
                
                //paddlesize += 0.1f;
                Debug.Log("BigPaddle");
                this.gameObject.transform.localScale += new Vector3(0.1f, 0, 0);
            }
        }
        #endregion
    }
}