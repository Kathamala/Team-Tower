using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Furnace1 : MonoBehaviour
{
    [SerializeField] AudioSource furnaceAudio;

    public SpriteRenderer radial;
    public GameObject cannonBall;
    public GameObject fireBall;
    public GameObject gosmaBall;
    public Transform fSpawnPoint;
    public GameObject player1;
    public GameObject player3;
    public Text furnaceWaitTime;

    public bool gerado = false;

    private int bulletType = 0; // 1 - CannonBall; 2 - FireBall; 3 - GosmaBall

    public float cannonBallWaitTime = 10f;
    public float fireBallWaitTime = 20f;
    public float gosmaBallWaitTime = 5f;
    private float counter = 0;

    Coroutine co;

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

        if (player1.GetComponent<PlayerController>().furn1 || player3.GetComponent<PlayerController>().furn1)
        {
            gerado = false;
            radial.enabled = true;
        }

        if(player1.GetComponent<PlayerController>().makeBullet1 || player3.GetComponent<PlayerController>().makeBullet1)
        {
            player1.GetComponent<PlayerController>().makeBullet1 = false;
            player3.GetComponent<PlayerController>().makeBullet1 = false;
            player1.GetComponent<PlayerController>().canMakeBullet1 = false;
            player3.GetComponent<PlayerController>().canMakeBullet1 = false;
            co = StartCoroutine(makeBullet());
        }

        if (gerado)
        {
            furnaceWaitTime.gameObject.SetActive(true);
            furnaceWaitTime.text = Mathf.Round(counter)+"";
            counter -= Time.deltaTime;
        }
        else
        {
            furnaceWaitTime.gameObject.SetActive(false);
        }

        //Stop
        if (player1.GetComponent<PlayerController>().stopFurnace || player3.GetComponent<PlayerController>().stopFurnace || Areas.healthCastle1Mid == 0)
        {
            StopCoroutine(co);
            furnaceAudio.Stop();
            player1.GetComponent<PlayerController>().canMakeBullet1 = true;
            player3.GetComponent<PlayerController>().canMakeBullet1 = true;
            gerado = false;
            RadialAnim1.bulletType = 0;
            player1.GetComponent<PlayerController>().stopFurnace = false;
            player3.GetComponent<PlayerController>().stopFurnace = false;
        }
    }

    private IEnumerator makeBullet()
    {
        player1.GetComponent<PlayerController>().furn1 = false;
        player3.GetComponent<PlayerController>().furn1 = false;
        furnaceAudio.Play(0);
        if (!gerado)
        {
            gerado = true;
            radial.enabled = false;
            if (RadialAnim1.bulletType == 1)
            {
                counter = cannonBallWaitTime;
                yield return new WaitForSeconds(counter);
                Instantiate(cannonBall, fSpawnPoint.position, fSpawnPoint.rotation);
            }
            else if (RadialAnim1.bulletType == 2)
            {
                counter = fireBallWaitTime;
                yield return new WaitForSeconds(counter);
                Instantiate(fireBall, fSpawnPoint.position, fSpawnPoint.rotation);
            }
            else if (RadialAnim1.bulletType == 3)
            {
                counter = gosmaBallWaitTime;
                yield return new WaitForSeconds(counter);
                Instantiate(gosmaBall, fSpawnPoint.position, fSpawnPoint.rotation);
            }
            else
            {
                counter = 0;
                gerado = false;
                yield return new WaitForSeconds(0.2f);
            }

            player1.GetComponent<PlayerController>().canMakeBullet1 = true;
            player3.GetComponent<PlayerController>().canMakeBullet1 = true;
            gerado = false;
            RadialAnim1.bulletType = 0;
            furnaceAudio.Stop();
        }
    }
}
