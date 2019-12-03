using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower2 : MonoBehaviour
{
    public static bool perdeu = false;
    public float maxHealth = 1000f;
    public static float health;

    [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        perdeu = false;
        health = maxHealth;
    }

    private void Update()
    {
        healthBar.SetSize(health / maxHealth);
        if (health <= 0)
        {
            health = 0;
            healthBar.SetSize(0);
            perdeu = true;
            SceneManager.LoadScene("Conclusion");
        }
    }
}
