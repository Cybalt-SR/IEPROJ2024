using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlescreenManager : MonoBehaviour
{
    public void OnStartPress()
    {
        SceneManager.LoadScene("main");
    }
}
