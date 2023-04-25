using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_script : MonoBehaviour
{
    public player_script player_script;
    private int player_health;
    // Start is called before the first frame update
    void Start()
    {
        player_script = GameObject.Find("player").GetComponent<player_script>();
        player_health = player_script.health;
    }

    // Update is called once per frame
    void Update()
    {
        if (player_script.health != player_health)
        {
            if (player_script.health < player_health)
            {
                player_health = player_script.health;
                Debug.Log("Remove " + player_health);
                removeHart();
            }
            else
            {
                player_health = player_script.health;
                Debug.Log("add " + player_health);
                addHart();
            }

        }
    }

    private void removeHart()
    {
        GameObject childObject = transform.GetChild(player_health).gameObject;
        childObject.SetActive(false);
    }

    private void addHart()
    {
        GameObject childObject = transform.GetChild(player_health).gameObject;
        childObject.SetActive(true);
    }
}
