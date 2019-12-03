using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialAnimBer2 : MonoBehaviour
{
    public GameObject player2;
    public GameObject player4;

    private Vector2 radial1;
    private Vector2 radial2;

    public static int troopType;

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
            if (player2.GetComponent<PlayerController>().berr2)
            {
                radial1 = player2.GetComponent<PlayerController>().radialValues;
                GetComponent<Animator>().SetFloat("y", radial1.y);
            }
            else if (player4.GetComponent<PlayerController>().berr2)
            {
                radial2 = player4.GetComponent<PlayerController>().radialValues;
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
