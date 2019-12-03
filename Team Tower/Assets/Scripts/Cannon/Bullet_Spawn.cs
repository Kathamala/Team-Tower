using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Spawn : MonoBehaviour
{
    public GameObject cannonBall;
    public GameObject fireBall;
    public GameObject gosmaBall;
    public GameObject player1;
    public GameObject player3;
    public Transform spawnPoint;

    public static bool cannonAim1 = false;
    public static int bulletType = 0;

    [SerializeField] AudioSource cannonFire;

    void FixedUpdate()
    {
        if (PlayerManager.playerAmount == 1)
        {
            player1 = PlayerManager.playersPrefabs[0];
        }
        if (PlayerManager.playerAmount == 3)
        {
            player3 = PlayerManager.playersPrefabs[2];
        }


        if (player1.GetComponent<PlayerController>().shot1 || player3.GetComponent<PlayerController>().shot1)
        {
            cannonAim1 = true;
        }
        else
        {
            cannonAim1 = false;
        }
        if (Cannon1Aim_Anim.shotEnabled1)
        {
            if (bulletType == 1)
            {
                Instantiate(cannonBall, spawnPoint.position, spawnPoint.rotation);
                cannonFire.Play(0);
            }
            if (bulletType == 2)
            {
                Instantiate(fireBall, spawnPoint.position, spawnPoint.rotation);
                cannonFire.Play(0);
            }
            if (bulletType == 3)
            {
                Instantiate(gosmaBall, spawnPoint.position, spawnPoint.rotation);
                cannonFire.Play(0);
            }
            player1.GetComponent<PlayerController>().shot1 = false;
            player3.GetComponent<PlayerController>().shot1 = false;
            cannonAim1 = false;
            Cannon1Aim_Anim.shotEnabled1 = false;
            bulletType = 0;
        }
    }
}
