﻿using UnityEngine;
using System.Collections;

public class StrongBlockPhys : MonoBehaviour {

    public Rigidbody[] RbitemPrefab;
    public int blockHealth = 3;
    private int chanceItem;
    private int i;
    Renderer rend;
    public Material [] texture;
   
    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }
    // Update is called once per frame
    void Update(){}
    
    #region itemChance
    void itemChance(int playernumber)
    {
        chanceItem = Random.Range(0, 100);

        //Paddle Big 25%
        if (chanceItem >= 0 && chanceItem <= 25)
        {
            i = 0;
        }
        //Paddle small 20%
        else if (chanceItem > 25 && chanceItem <= 45)
        {
            i = 1;
        }
        //Shield 15%
        else if (chanceItem > 45 && chanceItem <= 60)
        {
            i = 5;
        }
        // Add Ball 15%
        else if (chanceItem > 60 && chanceItem <= 75)
        {
            if (playernumber == 1 && Player1Control.powerballstatus == false && Player1Control.powerballCollected == false && Player1Control.gluestatus == false)
            {
                i = 2;
            }
            else if (playernumber == 2 && Player2Control.powerballstatus == false && Player2Control.powerballCollected == false && Player2Control.gluestatus == false)
            {
                i = 2;
            }
            else
            {
                itemChance(playernumber);
            }
        }
        //Control Change 10%
        else if (chanceItem > 75 && chanceItem <= 85)
        {
            i = 3;
        }
        //Glue 10%
        else if (chanceItem > 85 && chanceItem <= 95)
        {
            i = 4;
        }
        //PowerBall 5%
        else if (chanceItem > 95 && chanceItem <= 100)
        {
            i = 6;
        }
    }
    #endregion

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "ball")
        {
            Player1Control.player1Score += 200;
            Destroy(gameObject);
            BlockPhys.brickZähler--;
        }

        if (other.transform.tag == "ball2")
        {
            Player2Control.player2Score += 200;
            Destroy(gameObject);
            BlockPhys.brickZähler--;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        blockHealth --;
        if (blockHealth == 0)
        {
            int random = Random.Range(0,3);
          
            if (col.transform.tag == "ball" && random == 1)
            {
                Rigidbody ItemInstance;
                itemChance(1);

                if (DestroyObjectsBottomBorder.ballCount1 > 1 && (i == 4 || i == 6))
                {
                    itemChance(1);
                }

                if (BlockPhys.brickZähler > 5)
                {
                    ItemInstance = Instantiate(RbitemPrefab[i], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as Rigidbody;
                    ItemInstance.AddForce(0, -150, 0);
                }
            }

            if (col.transform.tag == "ball2" && random == 1)
            {
                Rigidbody ItemInstance;
                itemChance(2);

                if (DestroyObjectsBottomBorder.ballCount2 > 1 && (i == 4 || i == 6))
                {
                    itemChance(2);
                }
         
                if (BlockPhys.brickZähler > 5)
                {
                    ItemInstance = Instantiate(RbitemPrefab[i], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as Rigidbody;
                    ItemInstance.AddForce(0, 150, 0);
                }
            }

            if (col.transform.tag == "ball")
            {
                Player1Control.player1Score += 200;
                Destroy(gameObject);
                BlockPhys.brickZähler--; 
            }

            if (col.transform.tag == "ball2")
            {
                Player2Control.player2Score += 200;
                Destroy(gameObject);
                BlockPhys.brickZähler--;
            }
        }

        if  (blockHealth == 2)
        {
            rend.sharedMaterial=  texture[0];  
        }

        if (blockHealth == 1)
        {
            rend.sharedMaterial = texture[1];
        }
    }
}