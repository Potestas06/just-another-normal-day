using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_over : MonoBehaviour
{
    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }
}
