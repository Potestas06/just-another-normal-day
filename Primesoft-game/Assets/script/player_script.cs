using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_script : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer sprite;

    public float runSpeed = 0.8f;
    private float moveLimiter = 0.7f;

    private float horizontal;
    private float vertical;
    private bool isMoving;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        isMoving = horizontal != 0 || vertical != 0;
        animator.SetBool("run", isMoving);
        if (horizontal < 0)
        {
            sprite.flipX = true;
        }
        else if (horizontal > 0)
        {
            sprite.flipX = false;
        }
    }



    private void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }



}
