using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialAnim2 : MonoBehaviour
{

    public GameObject player2;
    public GameObject player4;

    private Vector2 radial1;
    private Vector2 radial2;

    public static int bulletType;

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

        if (GetComponent<SpriteRenderer>().enabled)
        {
            if (player2.GetComponent<PlayerController>().furn2)
            {
                radial1 = player2.GetComponent<PlayerController>().radialValues;
                GetComponent<Animator>().SetFloat("x", radial1.x);
                GetComponent<Animator>().SetFloat("y", radial1.y);
            }
            else if (player4.GetComponent<PlayerController>().furn2)
            {
                radial2 = player4.GetComponent<PlayerController>().radialValues;
                GetComponent<Animator>().SetFloat("x", radial2.x);
                GetComponent<Animator>().SetFloat("y", radial2.y);
            }
        }

        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CannonBall"))
        {
            bulletType = 1;
        }
        else if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("FireBall"))
        {
            bulletType = 2;
        }
        else if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("GosmaBall"))
        {
            bulletType = 3;
        }
        else
        {
            bulletType = 0;
        }
    }
}
