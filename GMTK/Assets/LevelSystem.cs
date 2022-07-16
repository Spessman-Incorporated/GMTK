using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelSystem
{
    public static class Levels
    {
        public static string Menu = "Menu";
        public static string Level01 = "Level01";
    }

    public static void LoadLevel(string level)
    {
        Debug.Log($"Loaded level: {level}");
        SceneManager.LoadScene(level);
    }
}
