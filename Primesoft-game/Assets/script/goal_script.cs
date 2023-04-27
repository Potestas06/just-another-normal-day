using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goal_script : MonoBehaviour
{
    public bool key = false;
    private player_script player_script;

    private void Start()
    {
        player_script = GameObject.Find("player").GetComponent<player_script>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && key)
        {
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + player_script.coins);
            Debug.Log("goal: " + SceneManager.sceneCountInBuildSettings.GetType() + " " + SceneManager.sceneCountInBuildSettings);
            Debug.Log("now: " + SceneManager.GetActiveScene().buildIndex.GetType() + " " + SceneManager.GetActiveScene().buildIndex);
            if(SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
