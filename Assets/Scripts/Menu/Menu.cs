using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private static Menu _instance = null;
    private ScenesInfo _sceneInfo;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        _sceneInfo = new ScenesInfo();
        DontDestroyOnLoad(gameObject);
    }

    public void GoHome()
    {
        SceneManager.LoadScene(_sceneInfo.MainMenu);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(_sceneInfo.Game);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
