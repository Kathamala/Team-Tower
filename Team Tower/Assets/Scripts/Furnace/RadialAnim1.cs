using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialAnim1 : MonoBehaviour
{
    public GameObject player1;
    public GameObject player3;

    private Vector2 radial1;
    private Vector2 radial2;

    public static int bulletType;

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

        if (GetComponent<SpriteRenderer>().enabled)
        {
            if (player1.GetComponent<PlayerController>().furn1)
            {
                radial1 = player1.GetComponent<PlayerController>().radialValues;
                GetComponent<Animator>().SetFloat("x", radial1.x);
                GetComponent<Animator>().SetFloat("y", radial1.y);
            }
            else if (player3.GetComponent<PlayerController>().furn1)
            {
                radial2 = player3.GetComponent<PlayerController>().radialValues;
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
