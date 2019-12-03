using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    //animation
    private Animator anim;
    private int playerNumber;
    [SerializeField] private Sprite[] playerIdles;
    private SpriteRenderer rend;
    //[SerializeField] private AnimatorController[] animatorControllers;
    //move
    public float velocity, jumpForce;
    private float move;
    public float moveVertical;
    public float distance;
    public LayerMask whatIsLadder;
    public bool isClimbing;
    private bool jump;

    //jump
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    //Cannon
    public bool isColidingCannon1 = false;
    public bool shot1 = false;
    public bool isColidingCannon2 = false;
    public bool shot2 = false;
    public float cont = 0.0f;
    public bool fire1 = false;
    public bool fire2 = false;
    public int bulletCount = 0;

    //Furnace
    Collision2D furnace;
    public bool isColidingFurnace1 = false;
    public bool furn1 = false;
    public bool makeBullet1 = false;
    public bool canMakeBullet1 = true;
    public bool isColidingFurnace2 = false;
    public bool furn2 = false;
    public bool makeBullet2 = false;
    public bool canMakeBullet2 = true;

    public Vector2 radialValues;

    //CannonBall
    public int bulletType = 0; // 1 - CannonBall; 2 - FireBall; 3 - GosmaBall
    private bool isColidingBullet = false;
    public bool grabbed = false;
    Collision2D bullet;
    public Transform holdpoint;

    //Berrante
    Collision2D berrante;
    private bool isColidingBerrante1 = false;
    public bool berr1 = false;
    public bool invokeTroops1 = false;
    private bool isColidingBerrante2 = false;
    public bool berr2 = false;
    public bool invokeTroops2 = false;
    public bool canMakeTroops1 = true;
    public bool canMakeTroops2 = true;

    public bool check = false;

    //Stop
    public bool stopCannon = false;
    public bool stopFurnace = false;
    public bool stopBerrante = false;

    //FixFloor
    [SerializeField] private int fixingFloor = 0; //1, 2, 3 - Top, Mid, Bot do Castle 1. 4, 5, 6 - Top, Mid Bot do Castle 2
    [SerializeField] private float secondsToFix = 6f;
    private Coroutine co;

    //FixInteract
    [SerializeField] private bool passou = false;

    //Wait For Players
    public GameObject pm;

    Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        jump = false;
        TeleportToLocation();
        PlayerManager.AddNewPlayerPrefabToList(gameObject);

        canMakeBullet1 = true;
        canMakeBullet2 = true;
        canMakeTroops1 = true;
        canMakeTroops2 = true;

        pm = GameObject.Find("PlayerManager");
    }
    
    void FixedUpdate()
    {
        //    move = Input.GetAxisRaw("Horizontal");
        //Debug.Log(move);
        rb.velocity = new Vector2(velocity * move * Time.deltaTime, rb.velocity.y);
        if (!shot1 && !shot2 && !furn1 && !furn2 && !berr1 && !berr2)
        {
            check = false;
        }
        //Stairs
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);
        if (hitInfo.collider != null)
        {
            if (moveVertical != 0)
            {
                isClimbing = true;
            }
        }
        else
        {
            isClimbing = false;
        }

        if (isClimbing == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, moveVertical * velocity * Time.deltaTime);
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 5;
        }

    }

    void TeleportToLocation()
    {
        playerNumber = PlayerManager.playerAmount;
        if (PlayerManager.playerAmount == 1)
        {
            //transform.position = new Vector3(-5, 0, 0);
            transform.position = new Vector3(-10, 5, 0);
            rend.sprite = playerIdles[0];
            //anim.runtimeAnimatorController = animatorControllers[0];
        }
        else if (PlayerManager.playerAmount == 2)
        {
            transform.position = new Vector3(10, 5, 0);
            rend.sprite = playerIdles[1];
            //anim.SetBool("IsBones", true);
            //Debug.Log("IsBones");
            //anim.runtimeAnimatorController = animatorControllers[1];
        }
        else if (PlayerManager.playerAmount == 3)
        {
            transform.position = new Vector3(-10, 1, 0);
            rend.sprite = playerIdles[2];
            //anim.SetBool("IsRobot", true);
            //anim.runtimeAnimatorController = animatorControllers[2];
        }
        else if (PlayerManager.playerAmount == 4)
        {
            transform.position = new Vector3(10, 1, 0);
            rend.sprite = playerIdles[3];
            //anim.SetBool("IsFlap", true);
            //anim.runtimeAnimatorController = animatorControllers[3];
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        if (!shot1 && !shot2 && !furn1 && !furn2 && !berr1 && !berr2 && fixingFloor == 0)
        {
            move = value.x;
            moveVertical = value.y;
        }
        else if(furn1 || furn2 || berr1 || berr2)
        {
            radialValues = value;
        }
        else if (shot1 || shot2)
        {
            cont = value.y;
        }
        //rb.velocity = new Vector2(velocity * value.x * Time.deltaTime, rb.velocity.y);
        //Debug.Log(value);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        /*if (context.interaction is HoldInteraction)
        {
            jump = true;
        }*/
        if (!shot1 && !shot2 && !furn1 && !furn2 && !berr1 && !berr2 && fixingFloor == 0)
        {
            if (context.ReadValue<float>() > 0.5)
            {
                jump = true;
            }
            else
            {
                jump = false;
            }
        }
        //rb.velocity = new Vector2(velocity * value.x * Time.deltaTime, rb.velocity.y);
        //Debug.Log(value);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        //Checks if the player is moving (animation)
        if (move != 0)
        {
            switch (playerNumber)
            {
                case 1:
                    anim.SetBool("RunningAlien", true);
                    break;
                case 2:
                    anim.SetBool("RunningBones", true);
                    break;
                case 3:
                    anim.SetBool("RunningRobot", true);
                    break;
                case 4:
                    anim.SetBool("RunningFlap", true);
                    break;
                default:
                    Debug.Log("There is an error on changing the player to run");
                    break;
            }
        }
        else
        {
            switch (playerNumber)
            {
                case 1:
                    anim.SetBool("RunningAlien", false);
                    
                    break;
                case 2:
                    anim.SetBool("RunningBones", false);
                    anim.SetBool("IsBones", true);
                    break;
                case 3:
                    anim.SetBool("RunningRobot", false);
                    anim.SetBool("IsRobot", true);
                    break;
                case 4:
                    anim.SetBool("RunningFlap", false);
                    anim.SetBool("IsFlap", true);
                    break;
                default:
                    Debug.Log("There is an error on changing the player to not run");
                    break;
            }
        }
        if (move > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (move < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (isGrounded && jump)
        {
            //jump = false;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = jumpForce * Time.deltaTime * Vector2.up;
        }
        if (jump && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = jumpForce * Time.deltaTime * Vector2.up;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (jump)
        {
            isJumping = false;
        }

        if (grabbed)
        {
            Physics2D.IgnoreCollision(bullet.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            bullet.collider.gameObject.transform.position = holdpoint.position;
        }

        //Fill health bar
        if (fixingFloor == 1)
        {
            Areas.healthCastle1Top += (secondsToFix/(Areas.maxHealthAreas*Time.deltaTime))/(3.2f);
        }
        if (fixingFloor == 2)
        {
            Areas.healthCastle1Mid += (secondsToFix / (Areas.maxHealthAreas * Time.deltaTime)) / (3.2f);
        }
        if (fixingFloor == 3)
        {
            Areas.healthCastle1Bot += (secondsToFix / (Areas.maxHealthAreas * Time.deltaTime)) / (3.2f);
        }
        if (fixingFloor == 4)
        {
            Areas.healthCastle2Top += (secondsToFix / (Areas.maxHealthAreas * Time.deltaTime)) / (3.2f);
        }
        if (fixingFloor == 5)
        {
            Areas.healthCastle2Mid += (secondsToFix / (Areas.maxHealthAreas * Time.deltaTime)) / (3.2f);
        }
        if (fixingFloor == 6)
        {
            Areas.healthCastle2Bot += (secondsToFix / (Areas.maxHealthAreas * Time.deltaTime)) / (3.2f);
        }

    }

    void OnInteract(InputAction.CallbackContext context)
    {
        //WaitForPlayers (Se quiser testar só com 1 ou 3 jogadores, comentar esse return)
        if (!pm.GetComponent<PlayerManager>().IsPlayerAmountAtMaximum())
        {
            return;
        }

        StartCoroutine(fixInteract());
        if (passou)
        {
            return;
        }
        passou = true;
        if (fixingFloor != 0)
        {
            StopCoroutine(co);
            if (fixingFloor == 1)
            {
                Areas.healthCastle1Top = 0;
            }
            else if (fixingFloor == 2)
            {
                Areas.healthCastle1Mid = 0;
            }
            else if (fixingFloor == 3)
            {
                Areas.healthCastle1Bot = 0;
            }
            else if (fixingFloor == 4)
            {
                Areas.healthCastle2Top = 0;
            }
            else if (fixingFloor == 5)
            {
                Areas.healthCastle2Mid = 0;
            }
            else if (fixingFloor == 6)
            {
                Areas.healthCastle2Bot = 0;
            }
            fixingFloor = 0;
            return;
        }

        //Interact with Berrante 1
        if (isColidingBerrante1 && !berr1 && !invokeTroops1 && !berrante.gameObject.GetComponent<Berrante1>().gerado && canMakeTroops1 && Areas.healthCastle1Bot > 0)
        {
            berr1 = true;
            canMakeTroops1 = false;
            StartCoroutine(berranteWait());
        }
        if (berr1 && check)
        {
            invokeTroops1 = true;
            check = false;
            StopCoroutine(berranteWait());
            return;
        }

        //Fix Berrante 1
        if (isColidingBerrante1 && Areas.healthCastle1Bot == 0)
        {
            fixingFloor = 3;
            co = StartCoroutine(fixFloor());
        }

        //Interact with Berrante 2
        if (isColidingBerrante2 && !berr2 && !invokeTroops2 && !berrante.gameObject.GetComponent<Berrante2>().gerado && canMakeTroops2 && Areas.healthCastle2Bot > 0)
        {
            berr2 = true;
            canMakeTroops2 = false;
            StartCoroutine(berranteWait());
        }
        if (berr2 && check)
        {
            invokeTroops2 = true;
            StopCoroutine(berranteWait());
            return;
        }

        //Fix Berrante 2
        if (isColidingBerrante2 && Areas.healthCastle2Bot == 0)
        {
            fixingFloor = 6;
            co = StartCoroutine(fixFloor());
        }

        //Interact with CannonBalls
        if (isColidingBullet)
        {
            grabbed = true;
            bulletType = bullet.gameObject.GetComponent<Bullet_Fire>().bulletType;
            bulletCount = 1;
        }

        //Interact with Furnace 1
        if (isColidingFurnace1 && !makeBullet1 && !furn1 && canMakeBullet1 && !furnace.gameObject.GetComponent<Furnace1>().gerado && Areas.healthCastle1Mid > 0)
        {
            furn1 = true;
            canMakeBullet1 = false;
            StartCoroutine(furnaceWait());
        }
        if (furn1 && check)
        {
            makeBullet1 = true;
            StopCoroutine(furnaceWait());
            return;
        }

        //Fix Furnace 1
        if (isColidingFurnace1 && Areas.healthCastle1Mid == 0)
        {
            fixingFloor = 2;
            co = StartCoroutine(fixFloor());
        }

        //Interact with Furnace 2
        if (isColidingFurnace2 && !makeBullet2 && !furn2 && canMakeBullet2 && !furnace.gameObject.GetComponent<Furnace2>().gerado && Areas.healthCastle2Mid > 0)
        {
            furn2 = true;
            canMakeBullet2 = false;
            StartCoroutine(furnaceWait());
        }
        if (furn2 && check)
        {
            makeBullet2 = true;
            StopCoroutine(furnaceWait());
            return;
        }

        //Fix Furnace 2
        if (isColidingFurnace2 && Areas.healthCastle2Mid == 0)
        {
            fixingFloor = 5;
            co = StartCoroutine(fixFloor());
        }

        //Interact with cannon 1
        if (isColidingCannon1 && bulletCount > 0 && Areas.healthCastle1Top > 0)
        {
            shot1 = true;
            bullet.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Bullet_Spawn.bulletType = bulletType;
            bulletCount = 0;
            grabbed = false;
            StartCoroutine(bulletWait());
            return;
        }

        if (shot1 && check)
        {
            fire1 = true;
            Destroy(bullet.gameObject);
            bulletType = 0;
            StopCoroutine(bulletWait());
        }

        //Fix Cannon 1
        if (isColidingCannon1 && Areas.healthCastle1Top == 0)
        {
            fixingFloor = 1;
            co = StartCoroutine(fixFloor());
        }

        //Interact with cannon 2
        if (isColidingCannon2 && bulletCount > 0 && Areas.healthCastle2Top > 0)
        {
            shot2 = true;
            bullet.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Bullet_Spawn2.bulletType = bulletType;
            bulletCount = 0;
            grabbed = false;
            StartCoroutine(bulletWait());
            return;
        }

        if (shot2 && check)
        {
            fire2 = true;
            Destroy(bullet.gameObject);
            bulletType = 0;
            StopCoroutine(bulletWait());
        }

        //Fix Cannon 2
        if (isColidingCannon2 && Areas.healthCastle2Top == 0)
        {
            fixingFloor = 4;
            co = StartCoroutine(fixFloor());
        }
    }
    public void OnCancel(InputAction.CallbackContext context)
    {
        //Soltar bala
        if (grabbed)
        {
            grabbed = false;
            bulletCount = 0;
            bulletType = 0;
            Physics2D.IgnoreCollision(bullet.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
        }
        //Cancelar bala sendo feita na fornalha
        if (!canMakeBullet1 || !canMakeBullet2)
        {
            if (isColidingFurnace1 || isColidingFurnace2)
            {
                stopFurnace = true;
            }
        }
        //Cancelar tropas
        if (!canMakeTroops1 || !canMakeTroops2)
        {
            if (isColidingBerrante1 || isColidingBerrante2)
            {
                stopBerrante = true;
            }
        }
        //Cancelar tiro de canhão
        if (shot1 || shot2)
        {
            stopCannon = true;
            bullet.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            grabbed = true;
            shot1 = false;
            shot2 = false;
            bulletCount = 1;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cannon")
        {
            isColidingCannon1 = true;
        }
        if (collision.gameObject.tag == "Cannon2")
        {
            isColidingCannon2 = true;
        }
        if (collision.gameObject.tag == "Furnace1")
        {
            furnace = collision;
            isColidingFurnace1 = true;
        }
        if (collision.gameObject.tag == "Furnace2")
        {
            furnace = collision;
            isColidingFurnace2 = true;
        }
        if (collision.gameObject.tag == "Grabbable" && bulletCount == 0)
        {
            isColidingBullet = true;
            bullet = collision;
        }
        if (collision.gameObject.tag == "Berrante")
        {
            berrante = collision;
            isColidingBerrante1 = true;
        }
        if (collision.gameObject.tag == "Berrante2")
        {
            berrante = collision;
            isColidingBerrante2 = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cannon")
        {
            isColidingCannon1 = false;
        }
        if (collision.gameObject.tag == "Cannon2")
        {
            isColidingCannon2 = false;
        }
        if (collision.gameObject.tag == "Furnace1")
        {
            isColidingFurnace1 = false;
        }
        if (collision.gameObject.tag == "Furnace2")
        {
            isColidingFurnace2 = false;
        }
        if (collision.gameObject.tag == "Grabbable")
        {
            isColidingBullet = false;
        }
        if (collision.gameObject.tag == "Berrante")
        {
            isColidingBerrante1 = false;
        }
        if (collision.gameObject.tag == "Berrante2")
        {
            isColidingBerrante2 = false;
        }
    }

    private IEnumerator bulletWait()
    {
        yield return new WaitForSeconds(0.5f);
        if (shot1 || shot2)
        {
            check = true;
        }
    }
    private IEnumerator furnaceWait()
    {
        yield return new WaitForSeconds(0.5f);
        if (furn1 || furn2)
        {
            check = true;
        }
    }
    private IEnumerator berranteWait()
    {
        yield return new WaitForSeconds(0.5f);
        if (berr1 || berr2)
        {
            check = true;
        }
    }
    private IEnumerator fixFloor()
    {
        yield return new WaitForSeconds(secondsToFix);
        if (fixingFloor == 1)
        {
            Areas.healthCastle1Top = Areas.maxHealthAreas;
            fixingFloor = 0;
        }
        else if (fixingFloor == 2)
        {
            Areas.healthCastle1Mid = Areas.maxHealthAreas;
            fixingFloor = 0;
        }
        else if (fixingFloor == 3)
        {
            Areas.healthCastle1Bot = Areas.maxHealthAreas;
            fixingFloor = 0;
        }
        else if (fixingFloor == 4)
        {
            Areas.healthCastle2Top = Areas.maxHealthAreas;
            fixingFloor = 0;
        }
        else if (fixingFloor == 5)
        {
            Areas.healthCastle2Mid = Areas.maxHealthAreas;
            fixingFloor = 0;
        }
        else if (fixingFloor == 6)
        {
            Areas.healthCastle2Bot = Areas.maxHealthAreas;
            fixingFloor = 0;
        }
        passou = true;
        StopCoroutine(fixFloor());
    }
    private IEnumerator fixInteract()
    {
        yield return new WaitForSeconds(0.2f);
        passou = false;
    }
}