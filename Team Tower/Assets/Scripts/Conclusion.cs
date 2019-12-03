using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Conclusion : MonoBehaviour
{
    [SerializeField] private GameObject text;
    void Start()
    {
        
        if (Tower1.perdeu == true && !Tower2.perdeu)
        {
            text.GetComponent<TextMeshProUGUI>().text = "Team 2 win";
            Debug.Log("Torre 2 venceu");
        }
        else if (Tower2.perdeu == true && !Tower1.perdeu)
        {
            text.GetComponent<TextMeshProUGUI>().text = "Team 1 win";
            Debug.Log("Torre 1 venceu");
        }
        else
        {
            text.GetComponent<TextMeshProUGUI>().text = "Draw";
            
        }
        //Resetar valores
        TimeLeft.timeLeft = 177f;
        Areas.areaAtacada = 0;
        Areas.fromWichCannon = 0;
        Areas.bulletType = 0;
        Bullet_Spawn.bulletType = 0;
        Bullet_Spawn.cannonAim1 = false;
        Bullet_Spawn2.bulletType = 0;
        Bullet_Spawn2.cannonAim2 = false;
        Cannon1Aim_Anim.shotEnabled1 = false;
        Cannon2Aim_Anim.shotEnabled2 = false;
        Tower1.perdeu = false;
        Tower1.health = 1000f;
        Tower2.perdeu = false;
        Tower2.health = 1000f;
    }
    public void PlayAgainPressed()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitPressed()
    {
        Application.Quit();
    }
}
