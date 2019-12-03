using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeLeft : MonoBehaviour
{
    public PlayerManager pm;
    Text text;
    public static float timeLeft = 177f;
    private float minutes;
    private float seconds = 0;

    void Start()
    {
        text = GetComponent<Text>();
        minutes = 3;
    }

    void Update()
    {
        if (pm.IsPlayerAmountAtMaximum())
        {
            timeLeft -= Time.deltaTime;
            seconds -= Time.deltaTime;
        }

        if (seconds < 0)
        {
            if (minutes == 0)
            {
                Destroy(text);
            }
            minutes--;
            seconds = 59;
        }

        //Time Out
        if (timeLeft < 0)
        {
            timeLeft = 0;
            if (Tower1.health > Tower2.health)
            {
                Tower2.perdeu = true;
            }
            else if (Tower1.health < Tower2.health)
            {
                Tower1.perdeu = true;
            }
            else
            {
                Tower1.perdeu = true;
                Tower2.perdeu = true;
            }
            SceneManager.LoadScene("Conclusion");
        }

        if (Mathf.Round(seconds) >= 10 && Mathf.Round(seconds) <= 59)
        {
            text.text = Mathf.Round(minutes) + ":" + Mathf.Round(seconds);
        }
        else
        {
            text.text = Mathf.Round(minutes) + ":0" + Mathf.Round(seconds);
        }
    }
}
