using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderDestroyObjectsScript : MonoBehaviour 
{
	public BallPhysikScript bs;
	public Ball2PhysikScript bs2;

	void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.transform.tag == "Matchball" && col.gameObject.transform.position.y > 4)

        {
            Debug.Log("matchballhere1");
            EndGame.player2Life -= 1;
        }

        if (col.gameObject.transform.tag == "Matchball" && col.gameObject.transform.position.y < 4)
        {
            Debug.Log("matchballhere2");
            EndGame.player1Life -= 1;
        }

        if (col.gameObject.transform.tag == "ball")
        {
           
            if (col.gameObject.transform.position.y < 0)  
            {
                bs.Standby(); 
                bs.ResetPowerups();
                Debug.Log("Ballweg");
            }
            else if (col.gameObject.transform.position.y > 0)
            {
                if(Paddle2Script.player2Score > 500)
                {
                    Debug.Log("Punkte");
                	Paddle2Script.player2Score -= 500;
                	Paddle1Script.player1Score += 500;
                }
                bs.Serve();
            }
        }
        if (col.gameObject.transform.tag == "ball2")
        {
            if (col.gameObject.transform.position.y < 4)  
            {
                if(Paddle1Script.player1Score > 500)
                {
                    Debug.Log("Punkte");
                	Paddle1Script.player1Score -= 500;
                	Paddle2Script.player2Score += 500;
                }   
                bs2.Serve();
            }
            else if (col.gameObject.transform.position.y > -4)
            {
                bs2.Standby();
                bs2.ResetPowerups2();
            }
        }
/*
        if (col.gameObject.transform.tag == "ball2")
        {
            if (col.gameObject.transform.position.y < -4)
            {
                if (col.gameObject.name.Contains("(Clone)"))
                {
                    Destroy(col.gameObject);

                    if (Player1Control.player1Score >= 500)
                    {
                        Player2Control.player2Score += 500;
                        Player1Control.player1Score -= 500;
                    }
                    else
                    {
                        int restPunkte;
                        restPunkte = Player1Control.player1Score;
                        Player2Control.player2Score += restPunkte;
                        Player1Control.player1Score -= restPunkte;
                    }
                    ballCount2--;
                }
                else
                {
                    if (Player1Control.player1Score >= 500)
                    {
                        Player2Control.player2Score += 500;
                        Player1Control.player1Score -= 500;
                    }
                    else
                    {
                        int restPunkte;
                        restPunkte = Player1Control.player1Score;
                        Player2Control.player2Score += restPunkte;
                        Player1Control.player1Score -= restPunkte;
                    }
                    bs2.Standby();
                    ballCount2--;
                }

                if (ballCount2 == 0)
                {
                    ballCount2++;
                    bs2.Serve();
                }
            }
            else if (col.gameObject.transform.position.y > 4)
            {
                if (col.gameObject.name.Contains("(Clone)"))
                {
                    Destroy(col.gameObject);
                    ballCount2--;
                }
                else
                {
                    bs2.Standby();
                    ballCount2--;
                }
                
                if (ballCount2 == 0)
                {
                    GetComponent<AudioSource>().Play();
                    ballCount2++;
                    bs2.ResetPowerups2();
                    servestatus2 = true;
                }
            }*/
        }
}
