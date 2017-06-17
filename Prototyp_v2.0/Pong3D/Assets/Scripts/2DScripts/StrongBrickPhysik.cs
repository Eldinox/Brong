using UnityEngine;
using System.Collections;

public class StrongBrickPhysik : MonoBehaviour
{

    public Rigidbody2D[] RbitemPrefab;
    public int blockHealth = 3;
    private int chanceItem;
    private int i;
    //Renderer rend;
   // public Material[] texture;

    // Use this for initialization
    void Start()
    {
       // rend = GetComponent<Renderer>();
        //rend.enabled = true;
    }
    // Update is called once per frame
    void Update() { }

    #region itemChance
    void itemChance(int playernumber)
    {
        chanceItem = Random.Range(1, 100);

        //Paddle Big 35%
        if (chanceItem > 0 && chanceItem <= 35)
        {
            i = 0; //0
        }
        //Paddle small 20%
        else if (chanceItem > 35 && chanceItem <= 55)
        {
            i = 1; //1
        }
        //Shield 15%
        else if (chanceItem > 55 && chanceItem <= 70)
        {
            i = 2;
        }
        // Add Ball 15%
        /*else if (chanceItem > 60 && chanceItem <= 75)
        {
            if (playernumber == 1 && Player1Control.powerballstatus == false && Player1Control.powerballCollected == false && Player1Control.gluestatus == false)
            {
                i = 5;
            }
            else if (playernumber == 2 && Player2Control.powerballstatus == false && Player2Control.powerballCollected == false && Player2Control.gluestatus == false)
            {
                i = 5;
            }
            else
            {
                itemChance(playernumber);
            }
        }*/
        //Control Change 15%
        else if (chanceItem > 70 && chanceItem <= 85)
        {
            i = 3;
        }
        //Glue 15%
        else if (chanceItem > 85 && chanceItem <= 95)
        {
            i = 4;
        }
        //PowerBall 5%
        else if(chanceItem > 95 && chanceItem <= 100 )
        {
            i = 5;
        }
    }
    #endregion

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "ball")
        {
            Paddle1Script.player1Score += 200;
            Destroy(gameObject);
            BrickPhysikScript.brickZähler--;
        }

        if (other.transform.tag == "ball2")
        {
            Paddle2Script.player2Score += 200;
            Destroy(gameObject);
            BrickPhysikScript.brickZähler--;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        blockHealth--;
        if (blockHealth == 0)
        {
            int random = Random.Range(0, 3);

            if (col.transform.tag == "ball" && random == 1)
            {
                Rigidbody2D ItemInstance;
                itemChance(1);

                if (BrickPhysikScript.brickZähler > 5)
                {
                    ItemInstance = Instantiate(RbitemPrefab[i], new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as Rigidbody2D;
                    ItemInstance.AddForce(new Vector2(0, -150));
                }
            }

            if (col.transform.tag == "ball2" && random == 1)
            {
                Rigidbody2D ItemInstance;
                itemChance(2);

                if (BrickPhysikScript.brickZähler > 5)
                {
                    ItemInstance = Instantiate(RbitemPrefab[i], new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as Rigidbody2D;
                    ItemInstance.AddForce(new Vector2(0, 150));
                }
            }

            if (col.transform.tag == "ball")
            {
                Paddle1Script.player1Score += 200;
                Destroy(gameObject);
                BrickPhysikScript.brickZähler--;
            }

            if (col.transform.tag == "ball2")
            {
                Paddle2Script.player2Score += 200;
                Destroy(gameObject);
                BrickPhysikScript.brickZähler--;
            }
        }

        if (blockHealth == 2)
        {
             float rot = Random.value;
             float grün = Random.value;
             float blau = Random.value;
            while(blau >= 0.9 || blau <= 0.1 || grün >= 0.9 || grün <= 0.1 || rot >= 0.9 || rot <= 0.1)
            {
                rot = Random.value;
                grün = Random.value;
                blau = Random.value;
            }
            //rend.sharedMaterial = texture[0];
            //GetComponentSpriteRenderer.color = Color.white;
            Color newColor = new Color(rot, grün, blau, 1.0f);
            GetComponent<SpriteRenderer>().color = newColor;
            
            

        }

        if (blockHealth == 1)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            //rend.sharedMaterial = texture[1];
        }
    }
}