using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class summond_scritpt : MonoBehaviour
{
    private soundManager soundManager;
    public float moveSpeed = 0.5f;
    public GameObject blood_efekt;
    private Animator animator;
    private Transform target;
    private player_script player_script;
    private Rigidbody2D body;
    private bool isdead;


    void Start()
    {
        soundManager = GameObject.Find("soundManager").GetComponent<soundManager>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player_script = GameObject.Find("player").GetComponent<player_script>();
    }


    void Update()
    {
        if (target != null && !isdead)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }


    private void die()
    {
        isdead = true;
        body.velocity = Vector2.zero;
        StartCoroutine(dieAnimation());
    }

    private IEnumerator dieAnimation()
    {
        animator.SetTrigger("die");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Instantiate(blood_efekt, transform.position, Quaternion.identity);
        soundManager.Play("explosion");
        Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isdead)
        {
            target = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = null;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("summon collision");
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player" && player_script.isAttacking)
        {
            soundManager.Play("dieSpider");
            die();
        }
        else if (collision.gameObject.tag == "Player" && !isdead)
        {
            player_script.takeDamage();
        }
    }
}
