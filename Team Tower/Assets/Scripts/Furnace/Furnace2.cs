using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Furnace2 : MonoBehaviour
{
    [SerializeField] AudioSource furnaceAudio;

    public SpriteRenderer radial;
    public GameObject cannonBall;
    public GameObject fireBall;
    public GameObject gosmaBall;
    public Transform fSpawnPoint;
    public GameObject player2;
    public GameObject player4;
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
        if (PlayerManager.playerAmount == 2)
        {
            player2 = PlayerManager.playersPrefabs[1];
        }
        if (PlayerManager.playerAmount == 4)
        {
            player4 = PlayerManager.playersPrefabs[3];
        }

        if (player2.GetComponent<PlayerController>().furn2 || player4.GetComponent<PlayerController>().furn2)
        {
            gerado = false;
            radial.enabled = true;
        }

        if (player2.GetComponent<PlayerController>().makeBullet2 || player4.GetComponent<PlayerController>().makeBullet2)
        {
            player2.GetComponent<PlayerController>().makeBullet2 = false;
            player4.GetComponent<PlayerController>().makeBullet2 = false;
            player2.GetComponent<PlayerController>().canMakeBullet2 = false;
            player4.GetComponent<PlayerController>().canMakeBullet2 = false;
            co = StartCoroutine(makeBullet());
        }

        if (gerado)
        {
            furnaceWaitTime.gameObject.SetActive(true);
            furnaceWaitTime.text = Mathf.Round(counter) + "";
            counter -= Time.deltaTime;
        }
        else
        {
            furnaceWaitTime.gameObject.SetActive(false);
        }

        //Stop
        if (player2.GetComponent<PlayerController>().stopFurnace || player4.GetComponent<PlayerController>().stopFurnace || Areas.healthCastle2Mid == 0)
        {
            StopCoroutine(co);
            furnaceAudio.Stop();
            player2.GetComponent<PlayerController>().canMakeBullet2 = true;
            player4.GetComponent<PlayerController>().canMakeBullet2 = true;
            gerado = false;
            RadialAnim2.bulletType = 0;
            player2.GetComponent<PlayerController>().stopFurnace = false;
            player4.GetComponent<PlayerController>().stopFurnace = false;
        }
    }

    private IEnumerator makeBullet()
    {
        player2.GetComponent<PlayerController>().furn2 = false;
        player4.GetComponent<PlayerController>().furn2 = false;
        furnaceAudio.Play(0);
        if (!gerado)
        {
            gerado = true;
            radial.enabled = false;
            if (RadialAnim2.bulletType == 1)
            {
                counter = cannonBallWaitTime;
                yield return new WaitForSeconds(counter);
                Instantiate(cannonBall, fSpawnPoint.position, fSpawnPoint.rotation);
            }
            else if (RadialAnim2.bulletType == 2)
            {
                counter = fireBallWaitTime;
                yield return new WaitForSeconds(counter);
                Instantiate(fireBall, fSpawnPoint.position, fSpawnPoint.rotation);
            }
            else if (RadialAnim2.bulletType == 3)
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
            player2.GetComponent<PlayerController>().canMakeBullet2 = true;
            player4.GetComponent<PlayerController>().canMakeBullet2 = true;
            gerado = false;
            RadialAnim2.bulletType = 0;
            furnaceAudio.Stop();
        }
    }
}
