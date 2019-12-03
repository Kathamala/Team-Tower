using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Destroy : MonoBehaviour
{
    public float damage = 100;

    public Transform bullet;
    public float colisionRadius = 0.4f;
    public bool collided = false;
    public LayerMask whatToCollideWith;
    public LayerMask castle1Top;
    public LayerMask castle1Mid;
    public LayerMask castle1Bot;
    public LayerMask castle2Top;
    public LayerMask castle2Mid;
    public LayerMask castle2Bot;
    public LayerMask GrassL;
    public LayerMask GrassLM;
    public LayerMask GrassM;
    public LayerMask GrassRM;
    public LayerMask GrassR;

    private void FixedUpdate()
    {
        //Grass Collsions
            //GrassL
        if (Physics2D.OverlapCircle(bullet.position, colisionRadius, GrassL))
        {
            Debug.Log("GrassL");
            Destroy(gameObject);
            Areas.areaAtacada = 1;
            Areas.fromWichCannon = gameObject.GetComponent<Bullet_Fire>().fromWichCannon;
            Areas.bulletType = gameObject.GetComponent<Bullet_Fire>().bulletType;
        }
            //GrassLM
        else if (Physics2D.OverlapCircle(bullet.position, colisionRadius, GrassLM))
        {
            Debug.Log("GrassLM");
            Destroy(gameObject);
            Areas.areaAtacada = 2;
            Areas.fromWichCannon = gameObject.GetComponent<Bullet_Fire>().fromWichCannon;
            Areas.bulletType = gameObject.GetComponent<Bullet_Fire>().bulletType;
        }
            //GrassM
        else if (Physics2D.OverlapCircle(bullet.position, colisionRadius, GrassM))
        {
            Debug.Log("GrassM");
            Destroy(gameObject);
            Areas.areaAtacada = 3;
            Areas.fromWichCannon = gameObject.GetComponent<Bullet_Fire>().fromWichCannon;
            Areas.bulletType = gameObject.GetComponent<Bullet_Fire>().bulletType;
        }
            //GrassRM
        else if (Physics2D.OverlapCircle(bullet.position, colisionRadius, GrassRM))
        {
            Debug.Log("GrassRM");
            Destroy(gameObject);
            Areas.areaAtacada = 4;
            Areas.fromWichCannon = gameObject.GetComponent<Bullet_Fire>().fromWichCannon;
            Areas.bulletType = gameObject.GetComponent<Bullet_Fire>().bulletType;
        }
            //GrassR
        else if (Physics2D.OverlapCircle(bullet.position, colisionRadius, GrassR))
        {
            Debug.Log("GrassR");
            Destroy(gameObject);
            Areas.areaAtacada = 5;
            Areas.fromWichCannon = gameObject.GetComponent<Bullet_Fire>().fromWichCannon;
            Areas.bulletType = gameObject.GetComponent<Bullet_Fire>().bulletType;
        }

        //Castle 1 collisions
            //Top
        else if (Physics2D.OverlapCircle(bullet.position, colisionRadius, castle1Top))
        {
            if (gameObject.GetComponent<Bullet_Fire>().fromWichCannon == 2)
            {
                Debug.Log("Castle1Top");
                Destroy(gameObject);
                Tower1.health -= damage;
                Areas.healthCastle1Top -= damage;
            }
        }
            //Mid
        else if (Physics2D.OverlapCircle(bullet.position, colisionRadius, castle1Mid))
        {
            if (gameObject.GetComponent<Bullet_Fire>().fromWichCannon == 2)
            {
                Debug.Log("Castle1Mid");
                Destroy(gameObject);
                Tower1.health -= damage;
                Areas.healthCastle1Mid -= damage;
            }
        }
            //Bot
        else if (Physics2D.OverlapCircle(bullet.position, colisionRadius, castle1Bot))
        {
            if (gameObject.GetComponent<Bullet_Fire>().fromWichCannon == 2)
            {
                Debug.Log("Castle1Bot");
                Destroy(gameObject);
                Tower1.health -= damage;
                Areas.healthCastle1Bot -= damage;
            }
        }

        //Castle 2 collisions
            //Top
        else if (Physics2D.OverlapCircle(bullet.position, colisionRadius, castle2Top))
        {
            if (gameObject.GetComponent<Bullet_Fire>().fromWichCannon == 1)
            {
                Debug.Log("Castle2Top");
                Destroy(gameObject);
                Tower2.health -= damage;
                Areas.healthCastle2Top -= damage;
            }
        }
            //Mid
        else if (Physics2D.OverlapCircle(bullet.position, colisionRadius, castle2Mid))
        {
            if (gameObject.GetComponent<Bullet_Fire>().fromWichCannon == 1)
            {
                Debug.Log("Castle2Mid");
                Destroy(gameObject);
                Tower2.health -= damage;
                Areas.healthCastle2Mid -= damage;
            }
        }
            //Bot
        else if (Physics2D.OverlapCircle(bullet.position, colisionRadius, castle2Bot))
        {
            if (gameObject.GetComponent<Bullet_Fire>().fromWichCannon == 1)
            {
                Debug.Log("Castle2Bot");
                Destroy(gameObject);
                Tower2.health -= damage;
                Areas.healthCastle2Bot -= damage;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Troops")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        if (collision.gameObject.tag == "StopArchers1")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        if (collision.gameObject.tag == "StopArchers2")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
