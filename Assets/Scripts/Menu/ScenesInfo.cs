using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesInfo
{
    public readonly string Game;
    public readonly string MainMenu;

    public ScenesInfo()
    {
        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        string[] scenes = new string[sceneCount];
        for (int i = 0; i < sceneCount; i++)
        {
            scenes[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
        }

        MainMenu = scenes[0];
        Game = scenes[1];
    }
}
