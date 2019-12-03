using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Berrante1 : MonoBehaviour
{
    [SerializeField] AudioSource berrante;

    public SpriteRenderer radial;
    public GameObject player1;
    public GameObject player3;
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
        if (PlayerManager.playerAmount == 1)
        {
            player1 = PlayerManager.playersPrefabs[0];
        }
        if (PlayerManager.playerAmount == 3)
        {
            player3 = PlayerManager.playersPrefabs[2];
        }

        if (player1.GetComponent<PlayerController>().berr1 || player3.GetComponent<PlayerController>().berr1)
        {
            gerado = false;
            radial.enabled = true;
        }

        if (player1.GetComponent<PlayerController>().invokeTroops1 || player3.GetComponent<PlayerController>().invokeTroops1)
        {
            player1.GetComponent<PlayerController>().invokeTroops1 = false;
            player3.GetComponent<PlayerController>().invokeTroops1 = false;
            player1.GetComponent<PlayerController>().canMakeTroops1 = false;
            player3.GetComponent<PlayerController>().canMakeTroops1 = false;
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
        if (player1.GetComponent<PlayerController>().stopBerrante || player3.GetComponent<PlayerController>().stopBerrante || Areas.healthCastle1Bot == 0)
        {
            berrante.Stop();
            StopCoroutine(co);
            player1.GetComponent<PlayerController>().canMakeTroops1 = true;
            player3.GetComponent<PlayerController>().canMakeTroops1 = true;
            gerado = false;
            player1.GetComponent<PlayerController>().stopBerrante = false;
            player3.GetComponent<PlayerController>().stopBerrante = false;
        }
    }

    private IEnumerator wait()
    {
        player1.GetComponent<PlayerController>().berr1 = false;
        player3.GetComponent<PlayerController>().berr1 = false;
        berrante.Play(0);
        if (!gerado)
        {
            gerado = true;
            radial.enabled = false;
            counter = secondsToSpawn;
            if (RadialAnimBer1.troopType != 0)
            {
                yield return new WaitForSeconds(secondsToSpawn);
            }
            if (RadialAnimBer1.troopType == 1)
            {
                Instantiate(arqueiros, troopSpawnPoint.position, troopSpawnPoint.rotation);
            }
            else if (RadialAnimBer1.troopType == 2)
            {
                Instantiate(soldiers, troopSpawnPoint.position, troopSpawnPoint.rotation);
            }
            gerado = false;
            RadialAnimBer1.troopType = 0;
            player1.GetComponent<PlayerController>().canMakeTroops1 = true;
            player3.GetComponent<PlayerController>().canMakeTroops1 = true;
        }
    }
}