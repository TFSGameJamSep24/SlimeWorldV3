using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Text oxygenText;
    public Text docileSlimeText;
    public Text shySlimeText;
    public Text angrySlimeText;
    public GameObject gameOverPanel;
    public Text timerText;

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

    public void UpdateOxygen(float currentOxygen)
    {
        oxygenText.text = $"Oxygen: {currentOxygen:F1}";
    }

    public void UpdateSlimeCount(string slimeType, int count)
    {
        switch (slimeType)
        {
            case "docile":
                docileSlimeText.text = $"Docile Slimes: {count}";
                break;
            case "shy":
                shySlimeText.text = $"Shy Slimes: {count}";
                break;
            case "angry":
                angrySlimeText.text = $"Angry Slimes: {count}";
                break;
        }
    }

    public void GameOverScreen()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Game Over Panel is not assigned in the inspector");
        }
    }

    public void UpdateTimer(float currentTime)
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}

