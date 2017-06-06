﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallPhysikScript : MonoBehaviour 
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

        if(transform.position.x > 15 && transform.position.y > 10)
        {
        	Serve();
        }

        scoreText.text = ((int)Paddle1Script.player1Score).ToString();

        if (startposition == true)
        {
            transform.position = new Vector2(playerPaddle.transform.position.x, -4.45f);
        }

        if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W) && (startposition || Paddle1Script.glued))
        {
            rb.velocity = new Vector2(0,0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(600, 300));
            startposition = false;
        }   

        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) && (startposition || Paddle1Script.glued))
        {
            rb.velocity = new Vector2(0,0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-600, 300));
            startposition = false;
        }  

        if(Input.GetKey(KeyCode.W) && (startposition || Paddle1Script.glued))
        {
            rb.velocity = new Vector2(0,0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
            startposition = false;
            Paddle1Script.glued = false; 
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
        transform.position = new Vector2(20, 0);
        rb.velocity = new Vector2(0,300);
    }

    public void ResetPowerups()
    {
        if (playerPaddle.transform.localScale.x >= 1.5f )
        {
            playerPaddle.transform.localScale = new Vector3(0.15f, 0.16f, 0.16f);
        }

        if (playerPaddle.transform.localScale.x <= 1.5f && EndGame.endgameStarted == true)
        {
            playerPaddle.transform.localScale = new Vector3(0.15f, 0.16f, 0.16f);
        }
        Paddle1Script.shieldstatus = false;
        Paddle1Script.gluestatus = false;
        Paddle1Script.glued = false;
        circleControlChange.fillAmount = 0;
        circleGlue.fillAmount = 0;
        circleShield.fillAmount = 0;
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
