using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Areas : MonoBehaviour
{
    /*
    public GameObject grassL;
    public GameObject grassLM;
    public GameObject grassM;
    public GameObject grassRM;
    public GameObject grassR;
    */

    [SerializeField] private HealthBar hbCastle1Top;
    [SerializeField] private HealthBar hbCastle1Mid;
    [SerializeField] private HealthBar hbCastle1Bot;
    [SerializeField] private HealthBar hbCastle2Top;
    [SerializeField] private HealthBar hbCastle2Mid;
    [SerializeField] private HealthBar hbCastle2Bot;

    public static float maxHealthAreas = 200f;

    public static float healthCastle1Top;
    public static float healthCastle1Mid;
    public static float healthCastle1Bot;
    public static float healthCastle2Top;
    public static float healthCastle2Mid;
    public static float healthCastle2Bot;

    public static int areaAtacada = 0;
    public static int fromWichCannon = 0;
    public static int bulletType = 0;

    private bool check = false;

    private void Start()
    {
        healthCastle1Top = maxHealthAreas;
        healthCastle1Mid = maxHealthAreas;
        healthCastle1Bot = maxHealthAreas;
        healthCastle2Top = maxHealthAreas;
        healthCastle2Mid = maxHealthAreas;
        healthCastle2Bot = maxHealthAreas;
    }

    private void Update()
    {
        hbCastle1Top.SetSize(healthCastle1Top / maxHealthAreas);
        hbCastle1Mid.SetSize(healthCastle1Mid / maxHealthAreas);
        hbCastle1Bot.SetSize(healthCastle1Bot / maxHealthAreas);
        hbCastle2Top.SetSize(healthCastle2Top / maxHealthAreas);
        hbCastle2Mid.SetSize(healthCastle2Mid / maxHealthAreas);
        hbCastle2Bot.SetSize(healthCastle2Bot / maxHealthAreas);

        if (healthCastle1Top < 0)
        {
            healthCastle1Top = 0;
        }
        if (healthCastle1Mid < 0)
        {
            healthCastle1Mid = 0;
        }
        if (healthCastle1Bot < 0)
        {
            healthCastle1Bot = 0;
        }
        if (healthCastle2Top < 0)
        {
            healthCastle2Top = 0;
        }
        if (healthCastle2Mid < 0)
        {
            healthCastle2Mid = 0;
        }
        if (healthCastle2Bot < 0)
        {
            healthCastle2Bot = 0;
        }

        if (areaAtacada != 0 && bulletType != 3 && !check)
        {
            StartCoroutine(impactWait());
            check = true;
        }

        //Não apareceer barra de vida se a vida for cheia (...)
        if (healthCastle1Top == maxHealthAreas)
        {
            hbCastle1Top.gameObject.SetActive(false);
        }
        else
        {
            hbCastle1Top.gameObject.SetActive(true);
        }

        if (healthCastle1Mid == maxHealthAreas)
        {
            hbCastle1Mid.gameObject.SetActive(false);
        }
        else
        {
            hbCastle1Mid.gameObject.SetActive(true);
        }

        if (healthCastle1Bot == maxHealthAreas)
        {
            hbCastle1Bot.gameObject.SetActive(false);
        }
        else
        {
            hbCastle1Bot.gameObject.SetActive(true);
        }

        if (healthCastle2Top == maxHealthAreas)
        {
            hbCastle2Top.gameObject.SetActive(false);
        }
        else
        {
            hbCastle2Top.gameObject.SetActive(true);
        }

        if (healthCastle2Mid == maxHealthAreas)
        {
            hbCastle2Mid.gameObject.SetActive(false);
        }
        else
        {
            hbCastle2Mid.gameObject.SetActive(true);
        }

        if (healthCastle2Bot == maxHealthAreas)
        {
            hbCastle2Bot.gameObject.SetActive(false);
        }
        else
        {
            hbCastle2Bot.gameObject.SetActive(true);
        }
    }

    private IEnumerator impactWait()
    {
        yield return new WaitForSeconds(0.2f);
        areaAtacada = 0;
        bulletType = 0;
        fromWichCannon = 0;
        check = false;
    }
}