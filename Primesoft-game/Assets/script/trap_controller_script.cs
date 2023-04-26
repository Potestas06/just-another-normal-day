using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class trap_controller_script : MonoBehaviour
{
    private Animator animator;
    private bool isActive;
    void Start()
    {
        isActive = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActive = !isActive;
            UpdateTraps();
            return;
        }
    }

    private void UpdateTraps()
    {
        Debug.Log("UpdateTraps: " + isActive);
        animator.SetTrigger(isActive ? "on" : "off");
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out trap_script trap))
            {
                trap.Aktiv = isActive;
            }
        }
    }




}
