using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_script : MonoBehaviour
{
    public player_script player_script;
    private int player_health;
    private int player_coins;
    public TextMeshProUGUI textMeshProUI;
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

        if(player_script.coins != player_coins)
        {
            player_coins = player_script.coins;
            addcoin();
            Debug.Log("added coins: " + player_coins);
        }
    }

    private void addcoin()
    {
        GameObject childObject = gameObject.transform.Find("coins").gameObject;
        childObject.GetComponent<TextMeshProUGUI>().text = "Coins: " + player_coins.ToString();
    }
    private void removeHart()
    {
        player_health++;
        GameObject childObject = gameObject.transform.Find("Health" + player_health).gameObject;

        childObject.SetActive(false);
    }

    private void addHart()
    {
        player_health--;
        GameObject childObject = gameObject.transform.Find("Health" + player_health).gameObject;
        childObject.SetActive(true);
    }
}
