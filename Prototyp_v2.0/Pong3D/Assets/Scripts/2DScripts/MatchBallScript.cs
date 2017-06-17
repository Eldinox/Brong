using UnityEngine;
using System.Collections;

public class MatchBallScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public int ballSpeed = 10;
    static public bool startposition = true;
    public GameObject playerPaddle;
    public GameObject playerPaddle2;
    public static bool P1Torkassiert = false;
    public static bool P2Torkassiert = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (startposition == false)
        {
            rb.velocity = ballSpeed * (rb.velocity.normalized);
        }

        if (startposition == true && transform.position.y < -4)
        {
            transform.position = new Vector2(playerPaddle.transform.position.x, -4.45f);

            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.W))
            {
                rb.velocity = new Vector2(0, 0);
                GetComponent<Rigidbody2D>().AddForce(new Vector2 (600, 300));
                startposition = false;
            }

            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.W))
            {
                rb.velocity = new Vector2(0, 0);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-600, 300));
                startposition = false;
            }

            if (Input.GetKey(KeyCode.W))
            {
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2 (0, 300));
                startposition = false;
            }
        }

        if (startposition == true && transform.position.y > 4)
        {
            transform.position = new Vector2(playerPaddle2.transform.position.x, 4.45f);
            
            if (Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.U))
            {
                rb.velocity = new Vector2(0, 0);
                GetComponent<Rigidbody2D>().AddForce(new Vector2 (600, -300));
                startposition = false;
            }

            if (Input.GetKey(KeyCode.H) && Input.GetKey(KeyCode.U))
            {
                rb.velocity = new Vector3(0, 0, 0);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-600, -300));
                startposition = false;
            }

            if (Input.GetKey(KeyCode.U))
            {
                rb.velocity = new Vector2(0, 0);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -300));
                startposition = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "BottomBorder")
        {
            startposition = true;
            P1Torkassiert = true;
        }

        if (col.transform.tag == "TopBorder")
        {
            startposition = true;
            P2Torkassiert = true;
        }

        foreach (ContactPoint2D contact in col.contacts)
        {
            if (col.transform.tag == "Player1Paddle")
            {
                GetComponent<SpriteRenderer>().color = new Color(84f, 159f,242f, 1.0f);
                float winkel = contact.point.x - playerPaddle.transform.position.x;
                float winkelX = winkel / Paddle1Script.paddleSize;
                rb.velocity = new Vector2(winkelX * 5, rb.velocity.y);
            }

            if (col.transform.tag == "Player2Paddle")
            {
                GetComponent<SpriteRenderer>().color = new Color(218f, 52f, 56f , 1.0f);
                float winkel = contact.point.x - playerPaddle2.transform.position.x;
                float winkelX2 = winkel / Paddle2Script.paddleSize;
                rb.velocity = new Vector2(winkelX2 * 5, rb.velocity.y);
            }
        }
    }
}