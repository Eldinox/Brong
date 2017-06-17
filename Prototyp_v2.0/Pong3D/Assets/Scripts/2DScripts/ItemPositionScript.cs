using UnityEngine;
using System.Collections;

public class ItemPositionScript : MonoBehaviour 
{
   
    public GameObject shield;
    public GameObject glue;
    public GameObject controlchange;
    public GameObject shield2;
    public GameObject glue2;
    public GameObject controlchange2;

    // Use this for initialization
    void Start ()
    {
        shield.GetComponent<SpriteRenderer>().enabled = false;
        glue.GetComponent<SpriteRenderer>().enabled = false;
        controlchange.GetComponent<SpriteRenderer>().enabled = false;
        shield2.GetComponent<SpriteRenderer>().enabled = false;
        glue2.GetComponent<SpriteRenderer>().enabled = false;
        controlchange2.GetComponent<SpriteRenderer>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Paddle1Script.shieldstatus)
        {
            shield.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (Paddle1Script.shieldstatus == false)
        {
            shield.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (Paddle1Script.gluestatus)
        {
            glue.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (Paddle1Script.gluestatus == false)
        {
            glue.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (Paddle1Script.controlChange)
        {
            controlchange.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (Paddle1Script.controlChange == false)
        {
            controlchange.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (Paddle2Script.shieldstatus)
            {
                shield2.GetComponent<SpriteRenderer>().enabled = true;
            }
        else if (Paddle2Script.shieldstatus == false)
            {
                shield2.GetComponent<SpriteRenderer>().enabled = false;
            }

        if (Paddle2Script.gluestatus)
            {
                glue2.GetComponent<SpriteRenderer>().enabled = true;
            }
        else if (Paddle2Script.gluestatus == false)
            {
                //gameObject.SetActive(false);
                glue2.GetComponent<SpriteRenderer>().enabled = false;
            }

         if (Paddle2Script.controlChange)
            {
                controlchange2.GetComponent<SpriteRenderer>().enabled = true;
            }
         else if (Paddle2Script.controlChange == false)
            {
                controlchange2.GetComponent<SpriteRenderer>().enabled = false;
            }
    }     
}