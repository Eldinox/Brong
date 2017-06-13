using UnityEngine;
using System.Collections;

public class BrickPhysikScript : MonoBehaviour 
{
	public Rigidbody2D[] RbitemPrefab;
    private int chanceItem;
    private int i;
    public int BrickScore = 50;
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
        else if (chanceItem > 60 && chanceItem <= 75)
        {
            if (playernumber == 1 && Player1Control.powerballstatus == false && Player1Control.powerballCollected == false && Player1Control.gluestatus == false)
            {
                i = 5;
            }
            else if(playernumber == 2 && Player2Control.powerballstatus == false && Player2Control.powerballCollected == false && Player2Control.gluestatus == false)
            {
                i = 5;
            }
            else
            {
                itemChance(playernumber);
            }
        }
        //Control Change 15%
        else if (chanceItem > 70 && chanceItem <= 85)
        {
            i = 3;
        }
        //Glue 15%
        else if (chanceItem > 85 && chanceItem <= 100)
        {
            i = 4;
        }
        //PowerBall 5%
        /*else if(chanceItem > 95 && chanceItem <= 100 )
        {
            i = 6;
        }*/
   	}
    #endregion

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "ball")
        {
            Paddle1Script.player1Score += BrickScore;
            Destroy(gameObject);
            brickZähler--;
        }

		if (other.transform.tag == "ball2")
        {
            Paddle2Script.player2Score += BrickScore;
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
            Paddle1Script.player1Score += BrickScore;
            Destroy(gameObject);
            brickZähler--;
        }

        if (col.transform.tag == "ball2")
        {
            Paddle2Script.player2Score += BrickScore;
            Destroy(gameObject);
            brickZähler--;
        }
	}
}