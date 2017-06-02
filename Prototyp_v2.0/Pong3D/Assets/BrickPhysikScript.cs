using UnityEngine;
using System.Collections;

public class BrickPhysikScript : MonoBehaviour 
{
	public Rigidbody2D[] RbitemPrefab;
    private int chanceItem;
    private int i;
    //private GameObject[] bricks;
    public static int brickZähler;

    void Awake()
    {
        brickZähler = GameObject.FindGameObjectsWithTag("brick").Length;
        EndGame.endgameStarted = false; 
        Debug.Log("Bricks = "+brickZähler);
    }

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
            else if(playernumber == 2 && Player2Control.powerballstatus == false && Player2Control.powerballCollected == false && Player2Control.gluestatus == false)
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
        else if(chanceItem > 95 && chanceItem <= 100 )
        {
            i = 6;
        }
   	}
    #endregion

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "ball")
        {
            Paddle1Script.player1Score += 50;
            Destroy(gameObject);
            brickZähler--;
        }

		if (other.transform.tag == "ball2")
        {
            Paddle2Script.player2Score += 50;
            Destroy(gameObject);
            brickZähler--;
		}
	}

    void OnCollisionEnter2D(Collision2D col)
    {
		int random = Random.Range(0, 5);
        
      	if (col.transform.tag == "ball" && random == 1)
        {
            Rigidbody2D ItemInstance;
            itemChance(1);
         
            if (DestroyObjectsBottomBorder.ballCount1 > 1 &&  (i == 4 || i == 6))
            {
                itemChance(1);
            }

            if (brickZähler > 5)
            {
                ItemInstance = Instantiate(RbitemPrefab[i], new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as Rigidbody2D;
                ItemInstance.AddForce(new Vector2(0, -150));
            }
        }

        if (col.transform.tag == "ball2" && random == 1)
        {
            Rigidbody2D ItemInstance;
            itemChance(2);

            if (DestroyObjectsBottomBorder.ballCount2 > 1 && (i == 4 || i == 6))
            {
                itemChance(2);
            }

            if (brickZähler > 5)
            {
                ItemInstance = Instantiate(RbitemPrefab[i], new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as Rigidbody2D;
                ItemInstance.AddForce(new Vector2(0, 150));
            }     
        }

		if (col.transform.tag == "ball")
        {
            Paddle1Script.player1Score += 50;
            Destroy(gameObject);
            brickZähler--;
        }

        if (col.transform.tag == "ball2")
        {
            Paddle2Script.player2Score += 50;
            Destroy(gameObject);
            brickZähler--;
        }
	}
}