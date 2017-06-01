using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball2PhysikScript : MonoBehaviour 
{
	public Rigidbody2D rb;
    public int ballSpeed = 4;
    static public bool startposition = true;
    public GameObject playerPaddle;
    public GameObject playerPaddle2;
    public float gameTimer = 0;
    public Text scoreText;

	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () 
	{
		gameTimer += Time.deltaTime;
        if (gameTimer > 30)
        {
            if (ballSpeed < 6)
            {
                ballSpeed += 1;
            }
            gameTimer = 0;
        }

        if(transform.position.x < -15 && transform.position.y > 10)
        {
        	Serve();
        }

        scoreText.text = ((int)Paddle2Script.player2Score).ToString();

        if (startposition == true)
        {
            transform.position = new Vector2(playerPaddle2.transform.position.x, 4.45f);
        }

        if(Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.U) && startposition)// || Player1Control.glued && Player1Control.isClone == false) )
        {
            rb.velocity = new Vector2(0,0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-600, 300));
            startposition = false;
        }   

        if(Input.GetKey(KeyCode.H) && Input.GetKey(KeyCode.U) && startposition)// || Player1Control.glued && Player1Control.isClone == false) )
        {
            rb.velocity = new Vector2(0,0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(600, 300));
            startposition = false;
        }  

        if(Input.GetKey(KeyCode.U) && (startposition) && transform.position.y == 4.45f)
        {
            rb.velocity = new Vector2(0,0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
            startposition = false; 
        }/*
        else if(Input.GetKey(KeyCode.W) && (Player1Control.glued && Player1Control.isClone == false))
        {
            rb.velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().AddForce(0, 300, 0);
            startposition = false;
            Player1Control.glued = false;
        }
        else if (Input.GetKey(KeyCode.W) && (Player1Control.glued && Player1Control.isClone))
        {
            Player1Control.ItemInstance.velocity = new Vector3(0, 0, 0);
            Player1Control.ItemInstance.AddForce(0, 300, 0);
            startposition = false;
            Player1Control.glued = false;
        }
*/
        if (startposition == false)
        {
            rb.velocity = ballSpeed * (rb.velocity.normalized);
        }
/*
        if ((gameObject.name.Contains("(Clone)")))
        {
            rb.velocity = ballSpeed * (rb.velocity.normalized);
        }*/
	}

	public void Serve()
    {
        startposition = true;
    }

    public void Standby()
    {
        transform.position = new Vector2(-20, 0);
        rb.velocity = new Vector2(0,300);
    }

	void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("Brick"))
        {
            GetComponent<AudioSource>().Play();
        }

        foreach (ContactPoint2D contact in collision.contacts)
        {
            if(collision.transform.tag == "Player1Paddle")
            {
                float winkel = contact.point.x - playerPaddle.transform.position.x;
                float winkelX = winkel / Paddle1Script.paddleSize;
                rb.velocity = new Vector2(winkelX * 5, rb.velocity.y);
            }

            if(collision.transform.tag == "Player2Paddle")
            {
                float winkel = contact.point.x - playerPaddle2.transform.position.x;
                float winkelX2 = winkel / Paddle2Script.paddleSize;
                rb.velocity = new Vector2(winkelX2 * 5, rb.velocity.y);
            }
        }
    }
}
