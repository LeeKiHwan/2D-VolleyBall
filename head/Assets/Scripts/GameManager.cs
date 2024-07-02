using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject ball, player1, player2, jumpIcon1, jumpIcon2;
    public Text jumpText1, jumpText2;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Ball.redWin || Ball.blueWin) {
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                SceneManager.LoadScene("Main");
                Ball.redWin = false;
                Ball.blueWin = false;
                Countdown.setTime = 4;
                Countdown.countdown.SetActive(true);
                ball.transform.position = new Vector2(3.5f, 1.5f);
                ball.SetActive(true);
                Player1Movement.jumpLimited = 0;
                Player2Movement.jumpLimited = 0;
            }
        }

        jumpText1.text = "" + Player1Movement.jumpLimited;
        jumpText2.text = "" + Player2Movement.jumpLimited;
        if (Player1Movement.jumpLimited == 0)
        {
            jumpText1.gameObject.SetActive(false);
            jumpIcon1.SetActive(false);
        }
        if (Player2Movement.jumpLimited == 0)
        {
            jumpText2.gameObject.SetActive(false);
            jumpIcon2.SetActive(false);
        }
    }

    
}
