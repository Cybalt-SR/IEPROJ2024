using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppFunctions : MonoBehaviour
{
    public void Die()
    {
        SceneManager.LoadScene("lose");
    }
    public void EnterGame()
    {
        SceneManager.LoadScene("main");
    }
    public void ExitGame()
    {
        SceneManager.LoadScene("title");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
