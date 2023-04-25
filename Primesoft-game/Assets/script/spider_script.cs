using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider_script : MonoBehaviour
{

    public float moveSpeed = 0.5f;
    private Animator animator;
    private Transform target;
    public player_script player_script;
    private Rigidbody2D body;
    private bool isdead;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player_script = GameObject.Find("player").GetComponent<player_script>();
    }


    void Update()
    {
        if(target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }




    private void die()
    {
        isdead = true;
        body.velocity = Vector2.zero;
        animator.SetBool("run", false);
        StartCoroutine(dieAnimation());
    }

    private IEnumerator dieAnimation()
    {
        animator.SetTrigger("die");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        StartCoroutine(Waitdestroy());
    }

    private IEnumerator Waitdestroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !isdead)
        {
            target = collision.gameObject.transform;
            animator.SetBool("run", true);
        }   
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = null;
            animator.SetBool("run", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Spider collision");
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player" && player_script.isAttacking)
        {
            player_script.coins++;
            die();
            
        }
    }
}
