using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [NonSerialized] public static int playerAmount;
    [SerializeField] private GameObject playerManager;
    [SerializeField] private GameObject playerManagerUI;
    [SerializeField] private GameObject game;
    [NonSerialized] public static GameObject[] playersPrefabs; //Thiago, use essa lista para definir quem pode interagir com o canhão. lembrando que player 1 e 3 estão no time 1 e 2 e 4 no time 2.
    private int maximumPlayers;
    private void Start()
    {
        playersPrefabs = new GameObject[4];
        playerAmount = 0;
        playerManagerUI.SetActive(true);
        game.SetActive(false);
        playerManager.GetComponent<PlayerInputManager>().DisableJoining();
    }

    public void NewPlayerInstantiated()
    {
        playerAmount++;
        Debug.Log(playerAmount);
        IsPlayerAmountAtMaximum();
    }

    public void HowManyPlayers(int playersQuantity)
    {
        maximumPlayers = playersQuantity;
        playerManager.GetComponent<PlayerInputManager>().EnableJoining();
        playerManagerUI.SetActive(false);
        game.SetActive(true);
    }

    public static void AddNewPlayerPrefabToList(GameObject playerPrefab)
    {
        Debug.Log(playerPrefab);
        playersPrefabs[playerAmount-1] = playerPrefab;
        //playersPrefabs[playersPrefabs.Length] = playerPrefab;
        foreach (var player in playersPrefabs)
        {
            Debug.Log(player);
        }
    }

    public bool IsPlayerAmountAtMaximum()
    {
        if (playerAmount == maximumPlayers)
        {
            //PlayerInputManager PIM;
            playerManager.GetComponent<PlayerInputManager>().DisableJoining();
            return true;
        }
        return false;
    }
}
