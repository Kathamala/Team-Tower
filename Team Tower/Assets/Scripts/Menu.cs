using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject howToPlay;
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject buttonHolder;
    [SerializeField] private GameObject backCredits;
    [SerializeField] private GameObject backHowToPlay;
    public void StartPressed()
    {
        SceneManager.LoadScene("Game");
        
    }

    public void QuitPressed()
    {
        Application.Quit();
    }
    public void HowToPlayPressed()
    {
        buttonHolder.active = false;
        howToPlay.active = true;
        EventSystem.current.SetSelectedGameObject(backHowToPlay);
    }
    public void CreditsPressed()
    {
        buttonHolder.active = false;
        credits.active = true;
        EventSystem.current.SetSelectedGameObject(backCredits);
    }
    public void HowToPlayBackPressed()
    {
        buttonHolder.active = true;
        howToPlay.active = false;
        EventSystem.current.SetSelectedGameObject(start);
    }
    public void CreditsBackPressed()
    {
        buttonHolder.active = true;
        credits.active = false;
        EventSystem.current.SetSelectedGameObject(start);
    }
}
