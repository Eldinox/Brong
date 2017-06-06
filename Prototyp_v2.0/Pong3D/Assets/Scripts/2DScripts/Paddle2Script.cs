using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Paddle2Script : MonoBehaviour 
{
	public GameObject upperBorder;
    public Rigidbody2D rbball2;
	public static float speed = 6f;
    public static float rightLimit;
    public static float leftLimit;
    public static float paddleSize = 0.75f;
    public static int player2Score = 0;
    public static bool controlChange = false;
    public GameObject CCCloak;
    public GameObject GGCloak;
    public GameObject shield;
    public float controlChangeTime = 5f;
    public Image circleControlChange;
    float speedItemTimerControlChange = 5f;
    public static bool shieldstatus = false;
    public static bool gluestatus = false;
    public static bool glued = false;
    public float shieldTime = 8f;
    public float glueTime = 12f;
    float contactPointGlue;
    public Image circleShield;
    public Image circleGlue;
    float speedItemTimerShield = 8f;
    float speedItemTimerGlue = 12f;

    void Start (){}
	
	void Update () 
	{
		leftLimit = -upperBorder.transform.localScale.x / 2 + paddleSize;
        rightLimit = upperBorder.transform.localScale.x / 2 - paddleSize;

        if (controlChange)
        {
            CCCloak.SetActive(true);
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

            if (Input.GetKey(KeyCode.H) && transform.position.x > leftLimit + 0.1)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.K) && transform.position.x < rightLimit - 0.1)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
        }
        else
        {
            // Steuerung
            if (Input.GetKey(KeyCode.H) && transform.position.x < rightLimit - 0.1)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
    
            if (Input.GetKey(KeyCode.K) && transform.position.x > leftLimit + 0.1)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            CCCloak.SetActive(false);
        } 

        #region shield
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

        #region Glue
        // glue activation      
        if (gluestatus)
        {
            GGCloak.SetActive(true);
            glueTime -= Time.deltaTime;
            circleGlue.fillAmount = speedItemTimerGlue / 12;
            speedItemTimerGlue -= Time.deltaTime;

            if (glued == true)
            {
                rbball2.transform.position = new Vector2((transform.position.x + contactPointGlue), 4.5f);   
            }

            if (glueTime < 0)
            {
                speedItemTimerGlue = 12f;
                gluestatus = false;
                circleGlue.fillAmount = 0;
                glueTime = 12f;
                glued = false;
            }
        }
        else
        {
            GGCloak.SetActive(false);
        }
        #endregion
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
                
                paddleSize *= 1.5f;
                this.gameObject.transform.localScale += new Vector3(0.1f, 0, 0);
            }
        }
        //Small Paddle
        if (collision.transform.tag == "smallPaddle")
        {
            if (this.gameObject.transform.localScale.x >= 0.14f)
            {
                //GetComponent<AudioSource>().Play();

                paddleSize /= 1.5f;
                this.gameObject.transform.localScale -= new Vector3(0.1f, 0, 0);
            }
        }
        //ControlChange
        if (collision.transform.tag == "ControlChange")
        {
            //GetComponent<AudioSource>().Play();
            Paddle1Script.controlChange = true;
        }
        //GlueItem
        if (collision.transform.tag == "glueItem")
        {
            //GetComponent<AudioSource>().Play();
            speedItemTimerGlue = 12f;
            glueTime = 12f;
            gluestatus = true;
            circleShield.fillAmount = 0;
        }
        if (collision.transform.tag == "ball2" && gluestatus == true)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                contactPointGlue = contact.point.x - transform.position.x;
            }
            glued = true;
        }
        //Shield
        if (collision.transform.tag == "shieldItem")
        {
            //GetComponent<AudioSource>().Play();
            speedItemTimerShield = 8f;
            shieldTime = 8f;
            shieldstatus = true;
            circleShield.fillAmount = 0;
        }
        #endregion
    }
}