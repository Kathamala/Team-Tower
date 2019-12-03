using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon1Aim_Anim : MonoBehaviour
{
    Animator anim;

    public Animator GrassL;
    public Animator GrassLM;
    public Animator GrassM;
    public Animator GrassRM;
    public Animator GrassR;
    public Animator Castle2Top;
    public Animator Castle2Mid;
    public Animator Castle2Bot;

    public GameObject player1;
    public GameObject player3;

    public float blendIdle = 0;
    public float blendSpeed = 3.0f;
    public float blendMax = 100.0f;

    public static bool shotEnabled1 = false;

    public float what = 0.0f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (PlayerManager.playerAmount == 1)
        {
            player1 = PlayerManager.playersPrefabs[0];
        }
        if (PlayerManager.playerAmount == 3)
        {
            player3 = PlayerManager.playersPrefabs[2];
        }
    }


    void FixedUpdate()
    {
        if (!Bullet_Spawn.cannonAim1)
        {
            GrassL.SetBool("cannonMode", false);
            GrassLM.SetBool("cannonMode", false);
            GrassM.SetBool("cannonMode", false);
            GrassRM.SetBool("cannonMode", false);
            GrassR.SetBool("cannonMode", false);
            Castle2Bot.SetBool("cannonMode", false);
            Castle2Mid.SetBool("cannonMode", false);
            Castle2Top.SetBool("cannonMode", false);
            return;
        }

        float aim1 = player1.GetComponent<PlayerController>().cont;
        float aim2 = player3.GetComponent<PlayerController>().cont;
        anim.SetFloat("Aim", blendIdle);
        //Blink Blue Ground
        GrassL.SetFloat("angle", blendIdle);
        GrassL.SetBool("cannonMode", Bullet_Spawn.cannonAim1);
        GrassLM.SetFloat("angle", blendIdle);
        GrassLM.SetBool("cannonMode", Bullet_Spawn.cannonAim1);
        GrassM.SetFloat("angle", blendIdle);
        GrassM.SetBool("cannonMode", Bullet_Spawn.cannonAim1);
        GrassRM.SetFloat("angle", blendIdle);
        GrassRM.SetBool("cannonMode", Bullet_Spawn.cannonAim1);
        GrassR.SetFloat("angle", blendIdle);
        GrassR.SetBool("cannonMode", Bullet_Spawn.cannonAim1);
        //Blink Blue on Castle2
        Castle2Bot.SetFloat("angle", blendIdle);
        Castle2Bot.SetBool("cannonMode", Bullet_Spawn.cannonAim1);
        Castle2Mid.SetFloat("angle", blendIdle);
        Castle2Mid.SetBool("cannonMode", Bullet_Spawn.cannonAim1);
        Castle2Top.SetFloat("angle", blendIdle);
        Castle2Top.SetBool("cannonMode", Bullet_Spawn.cannonAim1);

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
        if (player1.GetComponent<PlayerController>().fire1 || player3.GetComponent<PlayerController>().fire1)
        {
            shotEnabled1 = true;
            player1.GetComponent<PlayerController>().fire1 = false;
            player3.GetComponent<PlayerController>().fire1 = false;
            GrassL.SetBool("cannonMode", false);
            GrassLM.SetBool("cannonMode", false);
            GrassM.SetBool("cannonMode", false);
            GrassRM.SetBool("cannonMode", false);
            GrassR.SetBool("cannonMode", false);
            Castle2Bot.SetBool("cannonMode", false);
            Castle2Mid.SetBool("cannonMode", false);
            Castle2Top.SetBool("cannonMode", false);
        }
    }
}
