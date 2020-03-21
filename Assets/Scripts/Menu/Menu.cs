using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadLevel(int ind)
    {
        SceneManager.LoadScene(ind);
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
