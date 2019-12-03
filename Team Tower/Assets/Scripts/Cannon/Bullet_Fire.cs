using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Fire : MonoBehaviour
{
    public float bulletForce = 37500.0f;
    public int fromWichCannon = 0;
    public int bulletType = 0; // 1 - CannonBall; 2 - FireBall; 3 - GosmaBall

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "FirePoint" && fromWichCannon == 0)
        {
            fromWichCannon = 1;
            GetComponent<Rigidbody2D>().AddForce(transform.right * bulletForce * Time.deltaTime); 
        }
        if (target.gameObject.tag == "FirePoint2" && fromWichCannon == 0)
        {
            fromWichCannon = 2;
            GetComponent<Rigidbody2D>().AddForce(-transform.right * bulletForce * Time.deltaTime);
        }
    }

}
