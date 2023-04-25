using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_script : MonoBehaviour
{
    public float runSpeed = 0.8f;
    public bool isAttacking;
    public int health = 3;
    public int coins;

    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer sprite;
    private Transform fist;
    private GameObject childObject;
    private SpriteRenderer playerSpriteRenderer;

    private float moveLimiter = 0.7f;
    private float horizontal;
    private float vertical;
    private bool isMoving;
    private bool isfliped;
    private bool isdead;



    private void Start()
    {
        childObject = transform.GetChild(0).gameObject;
        fist = transform.GetChild(0);
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Attack();
        checkHealth();
    }

    private void Move()
    {
        if (!isdead)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            isMoving = horizontal != 0 || vertical != 0;
            animator.SetBool("run", isMoving);
            if (horizontal < 0)
            {
                isfliped = false;
                sprite.flipX = true;
            }
            else if (horizontal > 0)
            {
                isfliped = true;
                sprite.flipX = false;
            }


            if (horizontal != 0 && vertical != 0)
            {
                horizontal *= moveLimiter;
                vertical *= moveLimiter;
            }

            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        }
    }

    private IEnumerator AttackCoroutine()
    {
        if (isfliped)
        {
            Vector3 newPosition = fist.localPosition;
            newPosition.x = 0.1f;
            fist.localPosition = newPosition;
        }
        else
        {
            Vector3 newPosition = fist.localPosition;
            newPosition.x = -0.19f;
            fist.localPosition = newPosition;
        }

        animator.SetTrigger("attack");
        childObject.SetActive(true);
        isAttacking = true;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isAttacking = false;
        childObject.SetActive(false);
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private void checkHealth()
    {
        if (health == 0)
        {
            isdead = true;
            body.velocity = Vector2.zero;
            StartCoroutine(dieCoroutine());
        }
    }
    private IEnumerator dieCoroutine()
    {
        animator.SetBool("run", false);
        animator.SetBool("dead", true);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(1);
    }


    void ResetColor()
    {
        playerSpriteRenderer.color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("spider") && !isAttacking)
        {
            health--;
            Color damageColor = new Color(0.75f, 0.20f, 0.22f);
            playerSpriteRenderer.color = damageColor;
            Invoke("ResetColor", 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("coin"))
        {
            coins++;
            Destroy(collision.gameObject);
        }
    }
}
