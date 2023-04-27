using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_script : MonoBehaviour
{
    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
}
