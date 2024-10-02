using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance {  get; private set; }

    public float oxygen = 100f; //Starting oxygen level
    public float maxOxygen = 100f;
    public float oxygenDepletionRate = 1f;

    private bool isGameActive = true;


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

    void Update()
    {
        if (isGameActive)
        {
            UpdateOxygen();
        }
    }

    private void UpdateOxygen()
    {
        oxygen -= oxygenDepletionRate * Time.deltaTime;
        oxygen = Mathf.Clamp(oxygen, 0, maxOxygen);

        if (oxygen <= 0)
        {
            EndGame();
        }

        UIManager.Instance.UpdateOxygen(oxygen);
    }

    public void EndGame()
    {
        isGameActive = false;
        //Handle game over logic 
        UIManager.Instance.GameOverScreen();
    }

    public void StartGame()
    {
        isGameActive = true;
        oxygen = maxOxygen;
    }



}
