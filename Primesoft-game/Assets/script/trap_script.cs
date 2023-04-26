using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class trap_script : MonoBehaviour
{
    private soundManager soundManager;
    private Animator animator;
    private player_script player_script;
    void Start()
    {
        soundManager = GameObject.Find("soundManager").GetComponent<soundManager>();
        player_script = GameObject.Find("player").GetComponent<player_script>();
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Aktiv && collision.tag == "Player")
        {
            State = true;
            Aktiv = false;
            aktivateTrap();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Aktiv && collision.tag == "Player")
        {
            State = false;
        }
    }


    public bool State
    {
        get; set;
    }

    public bool Aktiv
    {
        get; set;
    }

    public async void aktivateTrap()
    {
        await Task.Delay(200);
        StartCoroutine(waitfortrapoutCoroutine());
        player_script.takeDamage();
        await Task.Delay(2000);
        animator.SetTrigger("deaktivate");
        Debug.Log("spikes in");
        Aktiv = true;
    }

    private IEnumerator waitfortrapoutCoroutine()
    {
        animator.SetTrigger("aktivate");
        soundManager.Play("trap");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Debug.Log("spikes out");
    }


}
