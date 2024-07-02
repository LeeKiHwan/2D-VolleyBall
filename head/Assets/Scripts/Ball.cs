using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidbody;
    public GameObject player1, player2, giantItem, speedUpitem, doubleJumpItem,  ball, giantIcon1, giantIcon2, speedUpIcon1, speedUpIcon2, jumpIcon1, jumpIcon2;
    public float maxSpeed = 1f, sliderSpeed = 0.25f;
    public Text redScoreText, blueScoreText, winText, jumpText1, jumpText2;
    public int redScore=0, blueScore=0;
    public static bool redWin = false, blueWin = false;
    public bool blueBall = false, redBall = false, speedUp1 = false, speedUp2 = false;
    public Slider giantSlider1, giantSlider2, speedUpSlider1, speedUpSlider2;

    void Start() 
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Invoke("giantItemGenerator", Random.Range(10, 20));
        Invoke("speedUpItemGenerator", Random.Range(15, 25));
        Invoke("doubleJumpItemGenerator", Random.Range(10, 15));
        giantSlider1.value = 0;
        giantSlider2.value = 0;
    }

    private void FixedUpdate()
    {
        if (rigidbody.velocity.magnitude > maxSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
        }
    }//공 최대 속력 제한

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Platform")
        {
            if (ball.transform.position.x > 0)
            {
                redScore++;
                if (redScore == 10)
                {
                    redWin = true;
                    winText.text = "Red Win";
                    winText.gameObject.SetActive(true);
                }
                redBall = true;
                blueBall = false;
                redScoreText.text = "Red : " + redScore;
                ball.transform.position = new Vector2(-3.5f, 1.5f);
                ball.SetActive(false);
                Countdown.setTime = 4;
                Countdown.countdown.SetActive(true);
            }
            else
            {
                blueScore++;
                if (blueScore == 10)
                {
                    blueWin = true;
                    winText.text = "Blue Win";
                    winText.gameObject.SetActive(true);
                }
                blueBall = true;
                redBall = false;
                blueScoreText.text = "Blue : " + blueScore;
                ball.transform.position = new Vector2(3.5f, 1.5f);
                ball.SetActive(false);
                Countdown.setTime = 4;
                Countdown.countdown.SetActive(true);
            }
        }

        if (collision.gameObject.name == "Player1")
        {
            redBall = true;
            blueBall = false;
            if (speedUp1 == true)
            {
                maxSpeed = 10;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x * 100, rigidbody.velocity.y * 100);
            }
            else if (speedUp1 == false)
            {
                maxSpeed = 6;
            }
        }
        if (collision.gameObject.name == "Player2")
        {
            blueBall = true;
            redBall = false;
            if (speedUp2 == true)
            {
                maxSpeed = 10;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x * 100, rigidbody.velocity.y * 100);
            }
            else if (speedUp2 == false)
            {
                maxSpeed=6;
            }
        }

        if (collision.gameObject.tag == "Wall")
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x * 0.6f, rigidbody.velocity.y * 0.6f);
            if (speedUp1 == true || speedUp2 == true)
            {
                maxSpeed = 6;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x * 0.6f, rigidbody.velocity.y * 0.6f);
            } 
        }
    } // 공이 Collider 에 닿았을 때

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "giantItem")
        {
            collision.gameObject.SetActive(false);
            if (redBall == true)
            {
                player1.transform.localScale = new Vector2(1.1f, 1);
                giantSlider1.gameObject.SetActive(true);
                giantIcon1.SetActive(true);
                giantSlider1.value = 1;
            }
            if (blueBall == true)
            {
                player2.transform.localScale = new Vector2(1.1f, 1);
                giantSlider2.gameObject.SetActive(true);
                giantIcon2.SetActive(true);
                giantSlider2.value = 1;
            }
        }
        if (collision.gameObject.tag == "speedUpItem")
        {
            collision.gameObject.SetActive(false);
            if (redBall == true)
            {
                speedUp1 = true;
                speedUpSlider1.gameObject.SetActive(true);
                speedUpIcon1.SetActive(true);
                speedUpSlider1.value = 1;
            }
            if (blueBall == true)
            {
                speedUp2 = true;
                speedUpSlider2.gameObject.SetActive(true);
                speedUpIcon2.SetActive(true);
                speedUpSlider2.value = 1;
            }
        }
        if (collision.gameObject.tag == "doubleJumpItem")
        {
            collision.gameObject.SetActive(false);
            if (redBall == true)
            {
                Player1Movement.jumpLimited += 3;
                jumpIcon1.SetActive(true);
                jumpText1.gameObject.SetActive(true);
            }
            if (blueBall == true)
            {
                Player2Movement.jumpLimited += 3;
                jumpIcon2.SetActive(true);
                jumpText2.gameObject.SetActive(true);
            }
        }

    } //공이 아이템에 닿았을 때

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" && rigidbody.velocity.x == 0)
        {
            if (rigidbody.transform.position.y == 1.5f)
            {
                if (redBall)
                {
                    
                }
            }
        }
    }

    private void Update()
    {
        //플레이어1 거대화 아이템
        if (giantSlider1.value > 0)
        {
            giantSlider1.value -= sliderSpeed * Time.deltaTime;
        }
        if (giantSlider1.value == 0)
        {
            player1.transform.localScale = new Vector2(0.6f, 0.5f);
            giantSlider1.gameObject.SetActive(false);
            giantIcon1.SetActive(false);
        }
        //플레이어2 거대화 아이템
        if (giantSlider2.value > 0)
        {
            giantSlider2.value -= sliderSpeed * Time.deltaTime;
        }
        if (giantSlider2.value == 0)
        {
            player2.transform.localScale = new Vector2(0.6f, 0.5f);
            giantSlider2.gameObject.SetActive(false);
            giantIcon2.SetActive(false);
        }
        //플레이어1 스피드업 아이템
        if (speedUpSlider1.value > 0)
        {
            speedUpSlider1.value -= sliderSpeed * Time.deltaTime;
        }
        if (speedUpSlider1.value == 0)
        {
            speedUp1 = false;
            speedUpSlider1.gameObject.SetActive(false);
            speedUpIcon1.SetActive(false);
        }
        //플레이어2 스피드업 아이템
        if (speedUpSlider2.value > 0)
        {
            speedUpSlider2.value -= sliderSpeed * Time.deltaTime;
        }
        if (speedUpSlider2.value == 0)
        {
            speedUp2 = false;
            speedUpSlider2.gameObject.SetActive(false);
            speedUpIcon2.SetActive(false);
        }

        
    } //아이템 쿨타임

    void giantItemGenerator()
    {
        Instantiate(giantItem, new Vector2(Random.Range(-8.35f, 8.35f), Random.Range(-2, 2)), Quaternion.identity);
        Invoke("giantItemGenerator", Random.Range(15, 25));
    } //거대화 아이템 소환
    void speedUpItemGenerator()
    {
        Instantiate(speedUpitem, new Vector2(Random.Range(-8.35f, 8.35f), Random.Range(-2, 2)), Quaternion.identity);
        Invoke("speedUpItemGenerator", Random.Range(20, 30));
    } //스피드업 아이템 소환
    void doubleJumpItemGenerator()
    {
        Instantiate(doubleJumpItem, new Vector2(Random.Range(-8.35f, 8.35f), Random.Range(-2, 2)), Quaternion.identity);
        Invoke("doubleJumpItemGenerator", Random.Range(15, 20));
    } // 더블점프 아이템 소환
}
