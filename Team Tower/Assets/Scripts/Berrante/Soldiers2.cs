using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldiers2 : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    public Animator anim;
    public float health = 100;
    public float speed = 100f;
    private float holdSpeed;
    public float delayTimeToAttack = 1f;
    public float damage = 15;

    public int currentArea = 0;

    private bool passado = false;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        holdSpeed = speed;
        anim.SetBool("isWalking", true);
    }
    
    void Update()
    {
        healthBar.SetSize(health / 100);
        rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);

        if (currentArea == Areas.areaAtacada && Areas.fromWichCannon == 1)
        {
            if (Areas.bulletType == 1)
            {
                health -= 50;
            }
            else if (Areas.bulletType == 2)
            {
                health -= 100;
            }
            else if (Areas.bulletType == 3)
            {
                StartCoroutine(slowDown());
            }
            Areas.areaAtacada = 0;
            Areas.bulletType = 0;
            Areas.fromWichCannon = 0;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "GrassL" && collision.gameObject.tag != "GrassLM" && collision.gameObject.tag != "GrassM" && collision.gameObject.tag != "GrassRM" && collision.gameObject.tag != "GrassR" && collision.gameObject.tag != "Castle1Bot" && collision.gameObject.tag != "Ground")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        if (collision.gameObject.tag == "Castle1Bot")
        {
            StartCoroutine(attack());
            anim.SetBool("isWalking", false);
        }
        if (collision.gameObject.tag == "GrassL")
        {
            Debug.Log("Area1");
            currentArea = 1;
        }
        else if (collision.gameObject.tag == "GrassLM")
        {
            Debug.Log("Area2");
            currentArea = 2;
        }
        else if (collision.gameObject.tag == "GrassM")
        {
            Debug.Log("Area3");
            currentArea = 3;
        }
        else if (collision.gameObject.tag == "GrassRM")
        {
            Debug.Log("Area4");
            currentArea = 4;
        }
        else if (collision.gameObject.tag == "GrassR")
        {
            Debug.Log("Area5");
            currentArea = 5;
        }
    }

    private IEnumerator attack()
    {
        yield return new WaitForSeconds(delayTimeToAttack);
        Tower1.health -= damage;
        StartCoroutine(attack());
    }

    private IEnumerator slowDown()
    {
        speed = speed*0.25f;
        yield return new WaitForSeconds(3f);
        speed = holdSpeed;
    }
}
