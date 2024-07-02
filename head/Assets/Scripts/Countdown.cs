using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    public GameObject ball;
    public static GameObject countdown;
    public GameObject gameObject;
    public Text countdownText;
    public static float setTime=4;
    public int intSetTime;

    void Start()
    {
        ball.SetActive(false);
        countdown = gameObject;
    }
    void Update()
    {

        countdownText.text = intSetTime.ToString();

        if (Ball.redWin == true || Ball.blueWin == true)
        {
            countdown.SetActive(false);
        }

        if (setTime >= 0)
        {
            setTime -= Time.deltaTime;

            if (setTime > 3)
            {
                intSetTime = 3;
            }
            if (setTime <= 3)
            {
                intSetTime = 2;
            }
            if (setTime <= 2)
            {
                intSetTime = 1;
            }
            if (setTime <= 1)
            {
                intSetTime = 0;
            }
            if (setTime <= 0)
            {
                ball.SetActive(true);
                countdown.SetActive(false);
            }
        }
    }
}
