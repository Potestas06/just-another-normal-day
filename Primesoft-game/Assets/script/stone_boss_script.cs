using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stone_boss_script : MonoBehaviour
{
    private soundManager soundManager;
    public float moveSpeed = 0.2f;
    public GameObject blood_efekt;
    private Animator animator;
    private Transform target;
    private player_script player_script;
    private Rigidbody2D body;
    private bool isdead;
    public int health = 6;
    private SpriteRenderer sprite;
    public GameObject stone_attack;
    private bool canwalk = true;


    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        soundManager = GameObject.Find("soundManager").GetComponent<soundManager>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player_script = GameObject.Find("player").GetComponent<player_script>();
    }


    void Update()
    {
        if (target != null && !isdead && canwalk)
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
            StartCoroutine(SpreadAttack());
        }
    }

    private IEnumerator SpreadAttack()
    {
        yield return StartCoroutine(GlowAndAnimate());
        Instantiate(stone_attack, transform.position, Quaternion.identity);
        canwalk = false;
        yield return new WaitForSeconds(5f);
        canwalk = true;
    }

    private IEnumerator GlowAndAnimate()
    {
        animator.SetTrigger("glow");
        soundManager.Play("stone_particle");
        yield return new WaitForSeconds(1f);
    }




    private void resetColor()
    {
        sprite.color = Color.white;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("summon collision");
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player" && player_script.isAttacking)
        {
            soundManager.Play("dieSpider");
            if (health > 1)
            {
                health--;
                sprite.color = Color.red;
                Invoke("resetColor", 0.2f);
            }
            else
                die();
        }
        else if (collision.gameObject.tag == "Player" && !isdead)
        {
            animator.SetTrigger("attack");
            player_script.takeDamage();
        }
    }
}
