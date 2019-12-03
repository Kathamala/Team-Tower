using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialAnimBer1 : MonoBehaviour
{
    public GameObject player1;
    public GameObject player3;

    private Vector2 radial1;
    private Vector2 radial2;

    public static int troopType;

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
            if (player1.GetComponent<PlayerController>().berr1)
            {
                radial1 = player1.GetComponent<PlayerController>().radialValues;
                GetComponent<Animator>().SetFloat("y", radial1.y);
            }
            else if (player3.GetComponent<PlayerController>().berr1)
            {
                radial2 = player3.GetComponent<PlayerController>().radialValues;
                GetComponent<Animator>().SetFloat("y", radial2.y);
            }
        }

        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Arqueiras"))
        {
            troopType = 1;
        }
        else if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Soldiers"))
        {
            troopType = 2;
        }
        else
        {
            troopType = 0;
        }
    }
}
