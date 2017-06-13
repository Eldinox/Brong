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
    public Image circleShield;
    public Image circleGlue;
    public Image circleControlChange;

	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
        startposition = true;
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

        if(Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.U) && (startposition || Paddle2Script.glued))
        {
            rb.velocity = new Vector2(0,0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-600, -300));
            startposition = false;
        }   

        if(Input.GetKey(KeyCode.H) && Input.GetKey(KeyCode.U) && (startposition || Paddle2Script.glued))
        {
            rb.velocity = new Vector2(0,0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(600, -300));
            startposition = false;
        }  

        if(Input.GetKey(KeyCode.U) && (startposition || Paddle2Script.glued))
        {
            rb.velocity = new Vector2(0,0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0,-300));
            startposition = false; 
            Paddle2Script.glued = false;
        }

        if (startposition == false)
        {
            rb.velocity = ballSpeed * (rb.velocity.normalized);
        }
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

    public void ResetPowerups2()
    {
        if (Paddle2Script.paddleSize > 0.75f)
        {
            playerPaddle2.transform.localScale = new Vector2(0.15f, 0.16f);
            Paddle2Script.paddleSize = 0.75f;
        }

        if (Paddle2Script.paddleSize < 0.75f && EndGame.endgameStarted)
        {
            playerPaddle2.transform.localScale = new Vector2(0.15f, 0.16f);
            Paddle2Script.paddleSize = 0.75f;
        }
        Paddle2Script.shieldstatus = false;
        Paddle2Script.gluestatus = false;
        Paddle2Script.glued = false;
        circleControlChange.fillAmount = 0;
        circleGlue.fillAmount = 0;
        circleShield.fillAmount = 0;
    }

	void OnCollisionEnter2D(Collision2D collision)
    {

         if(collision.transform.tag =="SideTag" &&  rb.velocity.y > -1)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 0.5f);
            Debug.Log("Velobug");
        }

        if(collision.transform.tag =="SideTag" && rb.velocity.y < 1 )
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 0.5f);
            Debug.Log("Velobug");
        }

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
