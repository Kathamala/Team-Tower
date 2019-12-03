using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon2Aim_Anim : MonoBehaviour
{
    Animator anim;

    public Animator GrassL;
    public Animator GrassLM;
    public Animator GrassM;
    public Animator GrassRM;
    public Animator GrassR;

    public Animator Castle1Top;
    public Animator Castle1Mid;
    public Animator Castle1Bot;

    public GameObject player2;
    public GameObject player4;

    public float blendIdle = 0;
    public float blendSpeed = 3.0f;
    public float blendMax = 100.0f;

    public static bool shotEnabled2 = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (PlayerManager.playerAmount == 2)
        {
            player2 = PlayerManager.playersPrefabs[1];
        }
        if (PlayerManager.playerAmount == 4)
        {
            player4 = PlayerManager.playersPrefabs[3];
        }
    }

    void FixedUpdate()
    {
        if (!Bullet_Spawn2.cannonAim2)
        {
            GrassL.SetBool("cannonMode2", false);
            GrassLM.SetBool("cannonMode2", false);
            GrassM.SetBool("cannonMode2", false);
            GrassRM.SetBool("cannonMode2", false);
            GrassR.SetBool("cannonMode2", false);
            Castle1Bot.SetBool("cannonMode2", false);
            Castle1Mid.SetBool("cannonMode2", false);
            Castle1Top.SetBool("cannonMode2", false);
            return;
        }

        float aim1 = -player2.GetComponent<PlayerController>().cont;
        float aim2 = -player4.GetComponent<PlayerController>().cont;
        anim.SetFloat("Aim", blendIdle);
        //Blink Blue Ground
        GrassL.SetFloat("angle2", blendIdle);
        GrassL.SetBool("cannonMode2", Bullet_Spawn2.cannonAim2);
        GrassLM.SetFloat("angle2", blendIdle);
        GrassLM.SetBool("cannonMode2", Bullet_Spawn2.cannonAim2);
        GrassM.SetFloat("angle2", blendIdle);
        GrassM.SetBool("cannonMode2", Bullet_Spawn2.cannonAim2);
        GrassRM.SetFloat("angle2", blendIdle);
        GrassRM.SetBool("cannonMode2", Bullet_Spawn2.cannonAim2);
        GrassR.SetFloat("angle2", blendIdle);
        GrassR.SetBool("cannonMode2", Bullet_Spawn2.cannonAim2);
        //Blink Red on Castle1
        Castle1Bot.SetFloat("angle2", blendIdle);
        Castle1Bot.SetBool("cannonMode2", Bullet_Spawn2.cannonAim2);
        Castle1Mid.SetFloat("angle2", blendIdle);
        Castle1Mid.SetBool("cannonMode2", Bullet_Spawn2.cannonAim2);
        Castle1Top.SetFloat("angle2", blendIdle);
        Castle1Top.SetBool("cannonMode2", Bullet_Spawn2.cannonAim2);

        if (aim1 < 0 || aim2 < 0)
        {
            anim.SetFloat("Aim", blendIdle -= blendSpeed);
        }
        if (aim1 > 0 || aim2 > 0)
        {
            anim.SetFloat("Aim", blendIdle += blendSpeed);
        }

        if (blendIdle < -blendMax)
        {
            blendIdle = -blendMax;
        }
        if (blendIdle > blendMax)
        {
            blendIdle = blendMax;
        }
        if (player2.GetComponent<PlayerController>().fire2 || player4.GetComponent<PlayerController>().fire2)
        {
            shotEnabled2 = true;
            player2.GetComponent<PlayerController>().fire2 = false;
            player4.GetComponent<PlayerController>().fire2 = false;
            GrassL.SetBool("cannonMode2", false);
            GrassLM.SetBool("cannonMode2", false);
            GrassM.SetBool("cannonMode2", false);
            GrassRM.SetBool("cannonMode2", false);
            GrassR.SetBool("cannonMode2", false);
            Castle1Bot.SetBool("cannonMode2", false);
            Castle1Mid.SetBool("cannonMode2", false);
            Castle1Top.SetBool("cannonMode2", false);
        }
    }
}
