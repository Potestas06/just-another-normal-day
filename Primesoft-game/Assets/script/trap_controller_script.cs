using System.Collections;
using UnityEngine;

public class trap_controller_script : MonoBehaviour
{
    private Animator animator;
    private bool isActive = true;
    private soundManager soundManager;
    public bool unlocked = true;
    void Start()
    {
        soundManager = GameObject.Find("soundManager").GetComponent<soundManager>();
        animator = GetComponent<Animator>();
        foreach (Transform child in transform)
        {
            trap_script trap = child.GetComponent<trap_script>();
            if (trap != null)
            {
                trap.Aktiv = true;
            }
        }
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && unlocked)
        {
            soundManager.Play("lever");
            isActive = !isActive;
            yield return StartCoroutine(UpdateTrapsCoroutine());
        }
    }

    private IEnumerator UpdateTrapsCoroutine()
    {
        animator.SetBool("lever", isActive);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out trap_script trap))
            {
                trap.Aktiv = isActive;
            }
        }
    }
}





