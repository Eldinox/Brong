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
    public GameObject Fireball;
    public GameObject Firepaddle;
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

        if(transform.position.x > 15 && transform.position.y > 10)
        {
            Serve();
        }

        scoreText.text = ((int)Paddle1Script.player1Score).ToString();

        if (Paddle1Script.powerballCollected == true)
        {
            Firepaddle.SetActive(true);
           
        }

        if (Paddle1Script.powerballstatus == true)
        {
            Firepaddle.SetActive(false);
            Fireball.SetActive(true);

            if (transform.position.x > 5.9 || transform.position.x < -5.9)
            {
                transform.GetComponent<Collider2D>().isTrigger = false;
            }
            else
            {
                transform.GetComponent<Collider2D>().isTrigger = true;
            }

            if (transform.position.y > 4.5)
            {
                transform.GetComponent<Collider2D>().isTrigger = false;
                Fireball.SetActive(false);

                Paddle1Script.powerballstatus = false;
            }
        }

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
            //
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
        if (Paddle1Script.paddleSize > 0.75f )
        {
            playerPaddle.transform.localScale = new Vector2(0.15f, 0.16f);
            Paddle1Script.paddleSize = 0.75f;
        }

        if (Paddle1Script.paddleSize < 0.75f && EndGame.endgameStarted)
        {
            playerPaddle.transform.localScale = new Vector2(0.15f, 0.16f);
            Paddle1Script.paddleSize = 0.75f;
        }
       
        Paddle1Script.gluestatus = false;
        Paddle1Script.glued = false;
        Paddle1Script.powerballstatus = false;
        Paddle1Script.powerballCollected = false;
        circleControlChange.fillAmount = 0;
        circleGlue.fillAmount = 0;
        circleShield.fillAmount = 0;
        Fireball.SetActive(false);
        Firepaddle.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("brick"))
        {
            GetComponent<AudioSource>().Play();
        }

        foreach (ContactPoint2D contact in collision.contacts)
        {


       /*      if(collision.transform.tag =="SideTag" &&  rb.velocity.y > -0.5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 5f);
            Debug.Log("Velobug");
        }
*/
        if(collision.transform.tag =="SideTag")// && rb.velocity.y < 0.5f )
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 0.25f);
            Debug.Log("Velobug");
        }

        Debug.Log(rb.velocity.y);
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
