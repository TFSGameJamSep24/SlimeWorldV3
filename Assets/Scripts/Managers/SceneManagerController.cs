using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{
    public static SceneManagerController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadMainHub()
    {
        LoadScene("MainHub");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadLevel (int levelIndex)
    {
        LoadScene($"Level_{levelIndex}");
    }

    public void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        LoadScene(currentScene.name);
    }

    public void LoadNextLevel()
    {
        //Add a way to track level progression here
        int currentLevel = GetCurrentLevel();
        int nextLevel = currentLevel + 1;

        if (nextLevel <= 3) //Assuming we make 3 levels!
        {
            LoadLevel(nextLevel);
        }
        else
        {
            LoadMainHub(); //go back to main menu
        }
    }

    private int GetCurrentLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName.Contains("Level_"))
        {
            int levelIndex;
            if (int.TryParse(currentSceneName.Split('_')[1], out levelIndex))
            {
                return levelIndex;
            }
        }

        return 0;
    }

}
