﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player2Control : MonoBehaviour 
{
	/*public GameObject playerPaddle;
    public GameObject upperBorder;
    public Rigidbody rbball;
    public Rigidbody rbball2;
    public GameObject shield;
    public GameObject CCCloak2;
    public GameObject GGCloak2;
    public static float speed = 6f;
    static public float rightLimit;
    static public float leftLimit;
    public static int player2Score = 000;
    public static Rigidbody ItemInstance;
    public static bool shieldstatus = false;
    public static bool firstballisHere = false;
    public static bool isClone = false;
    public static bool powerballstatus = false;
    public static bool powerballCollected = false;
    public static bool gluestatus = false;
    public static bool glued = false;
    public static bool controlChange = false;
    public float controlChangeTime = 5f;
    public float shieldTime = 8f;
    public float glueTime = 12f;
    float contactPointGlue;
    public Image circleShield;
    public Image circleGlue;
    public Image circleControlChange;
    float speedItemTimerShield = 8f;
    float speedItemTimerGlue = 12f;
    float speedItemTimerControlChange = 5f;

    void Start(){}
    void Update()
    {
        /*leftLimit = upperBorder.GetComponent<Renderer>().bounds.min.x + (transform.localScale.x / 2);  // 0,75 ist die hälfte unsere paddle (-6,25 + 0,75 = -5,5) ==> genau unesere limit
        rightLimit = upperBorder.GetComponent<Renderer>().bounds.max.x - (transform.localScale.x / 2);
      
        #region Shield
        // shield activation      
        if (shieldstatus)
        {
            shieldTime -= Time.deltaTime;
            circleShield.fillAmount = speedItemTimerShield / 8;
            speedItemTimerShield -= Time.deltaTime;          
            shield.SetActive(true);

            if (shieldTime < 0)
            {
                speedItemTimerShield = 8;
                circleShield.fillAmount = 0;
                shieldstatus = false;
                shieldTime = 8f;
            }
        }
        else
        {
            shield.SetActive(false);
        }
        #endregion

        #region Controlchange
        // ControllChange
        if (controlChange)
        {
            CCCloak2.SetActive(true);
            controlChangeTime -= Time.deltaTime;
            circleControlChange.fillAmount = speedItemTimerControlChange / 5;
            speedItemTimerControlChange -= Time.deltaTime;

            if (controlChangeTime < 0)
            {
                speedItemTimerControlChange = 5;
                circleControlChange.fillAmount = 0;
                controlChange = false;
                controlChangeTime = 5f;
            }

            if (Input.GetKey(KeyCode.H)  && transform.position.x > leftLimit + 0.1)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.K) && transform.position.x < rightLimit - 0.1)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
        }
        else
        {
            // Steuerung
            if (Input.GetKey(KeyCode.H) && transform.position.x < rightLimit)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.K) && transform.position.x > leftLimit + 0.1)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            CCCloak2.SetActive(false);
        }
        #endregion

        #region Glue
        // glue activation      
        if (gluestatus)
        {
            GGCloak2.SetActive(true);
            glueTime -= Time.deltaTime;
            circleGlue.fillAmount = speedItemTimerGlue / 12;
            speedItemTimerGlue -= Time.deltaTime;

            if (glued == true)
            {
                if (isClone == false)
                {
                    rbball2.transform.position = new Vector3((transform.position.x + contactPointGlue), 4.44f, -0.7f);
                }

                if (isClone)
                {
                    ItemInstance.transform.position = new Vector3((transform.position.x + contactPointGlue), 4.44f, -0.7f);
                }
            }

            if (glueTime < 0)
            {
                speedItemTimerGlue = 12f;
                gluestatus = false;
                circleGlue.fillAmount = 0;
                glueTime = 12f;
                gluestatus = false;
                firstballisHere = false;
            }
        }
        else
        {
            GGCloak2.SetActive(false);
        }
        #endregion
    }

    void OnCollisionEnter(Collision collision) 
    {
        #region Items
        //Big Paddle
        if (collision.transform.tag == "bigPaddle")
        {
            if (this.gameObject.transform.localScale.x <= 3)
            {
                GetComponent<AudioSource>().Play();
                this.gameObject.transform.localScale += new Vector3(1f, 0, 0);
            }
        }

        //Small Paddle
        if (collision.transform.tag == "smallPaddle")
        {
            if (this.gameObject.transform.localScale.x >= 1)
            {
                GetComponent<AudioSource>().Play();
                this.gameObject.transform.localScale -= new Vector3(1, 0, 0);
            }
        }

        //Mehr Bälle
        if (collision.transform.tag == "mehrbaelle")
        {
            GetComponent<AudioSource>().Play();
            DestroyObjectsBottomBorder.ballCount2++;
            ItemInstance = Instantiate(rbball2, new Vector3(transform.position.x, transform.position.y-1f, transform.position.z), Quaternion.identity) as Rigidbody;
            ItemInstance.AddForce(0, -150, 0);
        }

        //Steuerung umkehren
        if (collision.transform.tag == "ControlChange")
        {
            GetComponent<AudioSource>().Play();
            Player1Control.controlChange  = true;
        }

        //Shield
        if (collision.transform.tag == "shieldItem" && shieldstatus == true)
        {
            GetComponent<AudioSource>().Play();
            speedItemTimerShield = 8f;
            shieldTime = 8f;
            shieldstatus = true;
            circleShield.fillAmount = 0;
        }

        if (collision.transform.tag == "shieldItem")
        {
            GetComponent<AudioSource>().Play();
            shieldstatus = true;
        }

        //Powerball
        if (collision.transform.tag == "powerballItem")
        {
            GetComponent<AudioSource>().Play();
            powerballCollected = true;
        }

        if(powerballCollected == true && collision.transform.tag == "ball2" || powerballCollected == true && BallsScript2.startposition == true)    
        {
            powerballstatus = true;
            powerballCollected = false;
        }

        //Glue
        if (collision.transform.tag == "glueItem")
        {
            GetComponent<AudioSource>().Play();
            speedItemTimerGlue = 12f;
            glueTime = 12f;
            gluestatus = true;
            circleShield.fillAmount = 0;
        }

        if (collision.transform.tag == "glueItem")
        {
            GetComponent<AudioSource>().Play();
            gluestatus = true;
        }

        if (collision.transform.tag == "ball2" && gluestatus == true)
        {
            if ((collision.gameObject.name.Contains("(Clone)")))
            {
                isClone = true;
            }
            else
            {
                isClone = false;
            }

            foreach (ContactPoint contact in collision.contacts)
            {
                contactPointGlue = contact.point.x - transform.position.x;
            }
            glued = true;
        }
        #endregion
    }*/
}