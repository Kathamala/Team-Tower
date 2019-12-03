using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Berrante2 : MonoBehaviour
{
    [SerializeField] AudioSource berrante;

    public SpriteRenderer radial;
    public GameObject player2;
    public GameObject player4;
    public GameObject soldiers;
    public GameObject arqueiros;

    public Transform troopSpawnPoint;

    public Text berranteWaitTime;

    public bool gerado = false;
    public float secondsToSpawn = 7f;
    private float counter = 0;

    Coroutine co;

    private void FixedUpdate()
    {
        if (PlayerManager.playerAmount == 2)
        {
            player2 = PlayerManager.playersPrefabs[1];
        }
        if (PlayerManager.playerAmount == 4)
        {
            player4 = PlayerManager.playersPrefabs[3];
        }

        if (player2.GetComponent<PlayerController>().berr2 || player4.GetComponent<PlayerController>().berr2)
        {
            gerado = false;
            radial.enabled = true;
        }

        if (player2.GetComponent<PlayerController>().invokeTroops2 || player4.GetComponent<PlayerController>().invokeTroops2)
        {
            player2.GetComponent<PlayerController>().invokeTroops2 = false;
            player4.GetComponent<PlayerController>().invokeTroops2 = false;
            player2.GetComponent<PlayerController>().canMakeTroops2 = false;
            player4.GetComponent<PlayerController>().canMakeTroops2 = false;
            co = StartCoroutine(wait());
        }

        if (gerado)
        {
            berranteWaitTime.gameObject.SetActive(true);
            berranteWaitTime.text = Mathf.Round(counter) + "";
            counter -= Time.deltaTime;
        }
        else
        {
            berranteWaitTime.gameObject.SetActive(false);
        }

        //Stop
        if (player2.GetComponent<PlayerController>().stopBerrante || player4.GetComponent<PlayerController>().stopBerrante || Areas.healthCastle2Bot == 0)
        {
            berrante.Stop();
            StopCoroutine(co);
            player2.GetComponent<PlayerController>().canMakeTroops2 = true;
            player4.GetComponent<PlayerController>().canMakeTroops2 = true;
            gerado = false;
            player2.GetComponent<PlayerController>().stopBerrante = false;
            player4.GetComponent<PlayerController>().stopBerrante = false;
        }
    }

    private IEnumerator wait()
    {
        player2.GetComponent<PlayerController>().berr2 = false;
        player4.GetComponent<PlayerController>().berr2 = false;
        berrante.Play(0);
        if (!gerado)
        {
            gerado = true;
            radial.enabled = false;
            counter = secondsToSpawn;
            if (RadialAnimBer2.troopType != 0)
            {
                yield return new WaitForSeconds(secondsToSpawn);
            }
            if (RadialAnimBer2.troopType == 1)
            {
                Instantiate(arqueiros, troopSpawnPoint.position, troopSpawnPoint.rotation);
            }
            else if (RadialAnimBer2.troopType == 2)
            {
                Instantiate(soldiers, troopSpawnPoint.position, troopSpawnPoint.rotation);
            }
            gerado = false;
            player2.GetComponent<PlayerController>().canMakeTroops2 = true;
            player4.GetComponent<PlayerController>().canMakeTroops2 = true;
        }
    }
}
