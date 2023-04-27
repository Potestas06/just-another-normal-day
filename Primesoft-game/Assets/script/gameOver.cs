using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            Restart();
        }else if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            GoToMenu();
        }
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("lastLevel"));
    }
}
