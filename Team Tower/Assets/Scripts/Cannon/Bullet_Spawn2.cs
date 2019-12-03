using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Spawn2 : MonoBehaviour
{
    public GameObject cannonBall;
    public GameObject fireBall;
    public GameObject gosmaBall;
    public GameObject player2;
    public GameObject player4;
    public Transform spawnPoint;

    public static bool cannonAim2 = false;
    public static int bulletType = 0;

    [SerializeField] AudioSource cannonFire;

    void FixedUpdate()
    {
        if (PlayerManager.playerAmount == 2)
        {
            player2 = PlayerManager.playersPrefabs[1];
        }
        if (PlayerManager.playerAmount == 4)
        {
            player4 = PlayerManager.playersPrefabs[3];
        }

        if (player2.GetComponent<PlayerController>().shot2 || player4.GetComponent<PlayerController>().shot2)
        {
            cannonAim2 = true;
        }
        else
        {
            cannonAim2 = false;
        }
        if (Cannon2Aim_Anim.shotEnabled2)
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
            player2.GetComponent<PlayerController>().shot2 = false;
            player4.GetComponent<PlayerController>().shot2 = false;
            cannonAim2 = false;
            Cannon2Aim_Anim.shotEnabled2 = false;
            bulletType = 0;
        }
    }
}
