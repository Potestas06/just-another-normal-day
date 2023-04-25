using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider_script : MonoBehaviour
{
    [SerializeField] private float innerRadius = 0.1f;
    [SerializeField] private float outerRadius = 0.2f;
    [SerializeField] private LayerMask playerLayer;
    public float moveSpeed = 0.5f;
    public float attackSpeed = 0.8f;
    private Animator animator;

    private Ray innerRay;
    private Ray outerRay;


    void Start()
    {
        animator = GetComponent<Animator>();
        innerRay = new Ray(transform.position, transform.forward);
        outerRay = new Ray(transform.position, transform.forward);
    }


    void Update()
    {
        innerRay.origin = transform.position;
        outerRay.origin = transform.position;
        innerRay.direction = transform.forward;
        outerRay.direction = transform.forward;
    }

    void FixedUpdate()
    {
        RaycastHit innerHit;
        RaycastHit outerHit;
        bool innerRaycast = Physics.Raycast(innerRay, out innerHit, innerRadius, playerLayer);
        bool outerRaycast = Physics.Raycast(outerRay, out outerHit, outerRadius, playerLayer);
        if (innerRaycast)
        {
            Debug.Log("inner");
            Vector3 direction = (innerHit.point - transform.position).normalized;
            transform.position += direction * attackSpeed * Time.deltaTime;
        }
        else if (outerRaycast)
        {
            Debug.Log("outer");
            Vector3 direction = (outerHit.point - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, innerRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, outerRadius);
    }


    private void die()
    {
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("fist"))
        {
            die();
        }
    }
}
