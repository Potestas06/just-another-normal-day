using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stone_attack_particle_script : MonoBehaviour
{
    private player_script player_script;
    private bool canTakeDamage = true;

    void Start()
    {
        player_script = GameObject.Find("player").GetComponent<player_script>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player" && canTakeDamage) 
        {
            player_script.takeDamage();
            canTakeDamage = false; 
            StartCoroutine(DamageCooldown());
        }
    }

    IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        canTakeDamage = true;
    }


}
