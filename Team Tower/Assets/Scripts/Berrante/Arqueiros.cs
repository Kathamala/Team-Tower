using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arqueiros : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    public Animator anim;
    public float health = 100;
    public float speed = 100f;
    private float holdSpeed;
    public float delayTimeToAttack = 1f;
    public float damage = 5;

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
        rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);

        if (currentArea == Areas.areaAtacada && Areas.fromWichCannon == 2)
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
        if (collision.gameObject.tag != "GrassL" && collision.gameObject.tag != "GrassLM" && collision.gameObject.tag != "GrassM" && collision.gameObject.tag != "GrassRM" && collision.gameObject.tag != "GrassR" && collision.gameObject.tag != "StopArchers1" && collision.gameObject.tag != "Ground")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        if (collision.gameObject.tag == "StopArchers1")
        {
            StartCoroutine(attack());
            anim.SetBool("isWalking", false);
        }
        if (collision.gameObject.tag == "StopArchers2")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        if (collision.gameObject.tag == "GrassL")
        {
            currentArea = 1;
        }
        if (collision.gameObject.tag == "GrassLM")
        {
            currentArea = 2;
        }
        if (collision.gameObject.tag == "GrassM")
        {
            currentArea = 3;
        }
        if (collision.gameObject.tag == "GrassRM")
        {
            currentArea = 4;
        }
    }

    private IEnumerator attack()
    {
        yield return new WaitForSeconds(delayTimeToAttack);
        Tower2.health -= damage;
        Areas.healthCastle2Bot -= 1;
        StartCoroutine(attack());
    }

    private IEnumerator slowDown()
    {
        speed = speed*0.25f;
        yield return new WaitForSeconds(3f);
        speed = holdSpeed;
    }
}
