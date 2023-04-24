using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_script : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer sprite;
    private Transform fist;
    private GameObject childObject;

    public float runSpeed = 0.8f;
    private float moveLimiter = 0.7f;

    private float horizontal;
    private float vertical;
    private bool isMoving;
    private bool isfliped;

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
    }

    private void Move()
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
            newPosition.x = -0.1f;
            fist.localPosition = newPosition;
        }

        childObject.SetActive(true);
        animator.SetTrigger("attack");

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        childObject.SetActive(false);
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Attack");
            StartCoroutine(AttackCoroutine());
        }
    }






}
