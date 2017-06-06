using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public bool gameEnded = false;
    public static bool endgameStarted = false;
    public float scoreTimer = 0f;
    public static int player1Life;
    public static int player2Life;
    public BallPhysikScript bs;
    public Ball2PhysikScript bs2;
    bool LifesCount = false;
    bool IconTime = false;
    public Image Healthbar1;
    public Image Healthbar2;
    public Image BattleModeIcon;
    public GameObject Score1;
    public GameObject Score2;
    public Rigidbody2D rbball;
    public Rigidbody2D rbball2;
    public GameObject MatchBall;
    public GameObject playerPaddle;
    public GameObject playerPaddle2;
    float battlemodeTimer = 0;
    public bool coinFlipMB = false;
    float lifeBorder1;
    float lifeBorder2;
    public GameObject WinLose1;
    public GameObject WinLose2;
    public GameObject Level1;
    public GameObject Level2;
    public GameObject Level3;
    public int Levelcount = 0;


    // Use this for initialization
    void Start(){}
    // Update is called once per frame
    void Update()
    {
        lifeBorder1 = player1Life * 0.19999f;
        lifeBorder2 = player2Life * 0.19999f;
        Debug.Log("countLevel" +Levelcount);
       
        if ((Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.U)) && gameEnded == true)
        { 
           /* BrickPhysikScript.brickZähler = 104;
            BrickPhysikScript.brickZähler = 112;
            BrickPhysikScript.brickZähler = 187;*/
            endgameStarted = false;
            Application.LoadLevel("MainMenu");
        }

        if (MatchBallScript.P1Torkassiert == true )
        {
            player1Life = player1Life -( 1/2);
            lifeBorder1 = player1Life * 0.19999f;
            Healthbar1.fillAmount = lifeBorder1;
            MatchBallScript.P1Torkassiert = false;
            
            if(player1Life < 1)
            {   
                WinLose2.SetActive(true);
                playerPaddle.SetActive(false);
                playerPaddle2.SetActive(false);
                Score1.SetActive(false);
                Score2.SetActive(false);
                MatchBall.SetActive(false);
                Healthbar1.fillAmount = 0;
                Healthbar2.fillAmount = 0;
                gameEnded = true;
            }
        }

        if (MatchBallScript.P2Torkassiert == true)
        {
            player2Life = player2Life -(1/2);
            lifeBorder2 = player2Life * 0.19999f;
            Healthbar2.fillAmount = lifeBorder2;
            MatchBallScript.P2Torkassiert = false;

            if(player2Life < 1)
            {
                WinLose1.SetActive(true);
                playerPaddle.SetActive(false);
                playerPaddle2.SetActive(false);
                Score1.SetActive(false);
                Score2.SetActive(false);
                MatchBall.SetActive(false);
                Healthbar1.fillAmount = 0;
                Healthbar2.fillAmount = 0;
                gameEnded = true;
            }
        }

        if(BrickPhysikScript.brickZähler == 0 && endgameStarted == false)
        {
            //GameObject.FindGameObjectWithTag("Level2").SetActive(true);
            
            //BrickPhysikScript.brickZähler = 10;
            Levelcount++;
            if (Levelcount == 1)
            {
                Level1.SetActive(false);
                Level2.SetActive(true);
                BallPhysikScript.startposition = true;
                Ball2PhysikScript.startposition = true;


            }
            if (Levelcount == 2)
            {
                BallPhysikScript.startposition = true;
                Ball2PhysikScript.startposition = true;
                Level2.SetActive(false);
                Level3.SetActive(true);
            }
            if (Levelcount == 3)
            {
                BallPhysikScript.startposition = true;
                Ball2PhysikScript.startposition = true;
                Level3.SetActive(false);
                endgameStarted = true;
            }
        }

        if (Levelcount == 3)
        {
            startEndgame();
            endgameStarted = true;
            Debug.Log("lifes1 =" + player1Life);
            Debug.Log("lifes2 =" + player2Life);

            if (LifesCount == false)
            {
                countLifes();

                if (player1Life < 1)
                {
                    WinLose2.SetActive(true);
                    MatchBall.SetActive(false);
                    Healthbar1.fillAmount = 0;
                    Healthbar2.fillAmount = 0;
                    playerPaddle.SetActive(false);
                    playerPaddle2.SetActive(false);
                    Score1.SetActive(false);
                    Score2.SetActive(false);
                    gameEnded = true;
                }

                if (player2Life < 1)
                {
                    WinLose1.SetActive(true);
                    playerPaddle.SetActive(false);
                    playerPaddle2.SetActive(false);
                    MatchBall.SetActive(false);
                    Healthbar1.fillAmount = 0;
                    Healthbar2.fillAmount = 0;
                    Score1.SetActive(false);
                    Score2.SetActive(false);
                    gameEnded = true;
                }
            }

            if (LifesCount == true )
            {
                scoreTimer += Time.deltaTime;

                if (Paddle1Script.player1Score > 0)
                {
                    Paddle1Script.player1Score -= 50;
                }

                if (Healthbar1.fillAmount < lifeBorder1)
                {
                    Healthbar1.fillAmount += 0.005f;
                }

                if (Paddle2Script.player2Score > 0)
                {
                    Paddle2Script.player2Score -= 50;
                }

                if (Healthbar2.fillAmount < lifeBorder2)
                {
                    Healthbar2.fillAmount += 0.005f;
                }
            }
        }

        if (IconTime == true)
        {
            BattleModeIcon.fillAmount += 0.05f;
        }
        else
        {
            BattleModeIcon.fillAmount -= 0.05f;
        }
    }

    void countLifes()
    {
        if (Paddle1Script.player1Score >= 6000)
        {
            Paddle1Script.player1Score = 5000;
        }

        if (Paddle2Script.player2Score >= 6000)
        {
            Paddle2Script.player2Score = 5000;
        }

        player1Life = (Paddle1Script.player1Score / 1000);
        player2Life = (Paddle2Script.player2Score / 1000);
        LifesCount = true;
    }

    void startEndgame()
    {
        BallPhysikScript.startposition = false;
        Ball2PhysikScript.startposition = false;
        rbball.velocity = new Vector2(0, 0);
        rbball2.velocity = new Vector2(0, 0);
        rbball.transform.position = new Vector2(20, rbball.transform.position.y);
        rbball2.transform.position = new Vector2(-20, rbball2.transform.position.y);
        battlemodeTimer += Time.deltaTime;
        bs.ResetPowerups();
        bs2.ResetPowerups2();

        if (battlemodeTimer > 2 && battlemodeTimer < 4)
        {
            IconTime = true;
        }

        if (battlemodeTimer > 4)
        {
            IconTime = false;
        }

        if (battlemodeTimer > 5)
        {
            Score1.SetActive(false);
            Score2.SetActive(false);
            Paddle1Script.speed = 8f;
            Paddle2Script.speed = 8f;
        }

        if (battlemodeTimer > 6 && battlemodeTimer < 7)
        {
            if(player1Life > player2Life)
            {
                MatchBall.transform.position = new Vector2(playerPaddle2.transform.position.x, 4.3f);
                MatchBall.SetActive(true);
            }

            if (player1Life < player2Life)
            {
                MatchBall.transform.position = new Vector2(playerPaddle.transform.position.x, -4.5f);
                MatchBall.SetActive(true);
            }

            if (player1Life == player2Life && coinFlipMB == false)
            {
                int random = Random.Range(0, 2);
                
                if (random == 0)
                {
                    MatchBall.transform.position = new Vector2(playerPaddle2.transform.position.x, 4.3f);
                    MatchBall.SetActive(true);
                    coinFlipMB = true;
                }
                else if (random == 1)
                {
                    MatchBall.transform.position = new Vector2(playerPaddle.transform.position.x, -4.5f);
                    MatchBall.SetActive(true);
                    coinFlipMB = true;
                }
            }
        }
    }
}