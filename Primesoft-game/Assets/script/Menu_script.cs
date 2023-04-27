using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_script : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            PlayGame();
        }
        
    }
    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
}
