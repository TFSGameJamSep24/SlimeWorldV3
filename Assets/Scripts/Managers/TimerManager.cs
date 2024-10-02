using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{

    public delegate void Timer(float currentTime);
    public event Timer OnTimerUpdate;


    public static TimerManager Instance { get; private set; }

    public float levelTime = 300f; // 5 mins
    private float currentTime;

    private bool isTimerRunning = false;

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

    private void Start()
    {
        currentTime = levelTime;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            UpdateTimer();
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    private void UpdateTimer()
    {
        currentTime -= Time.deltaTime;
        currentTime = Mathf.Clamp(currentTime, 0, levelTime);

        UIManager.Instance.UpdateTimer(currentTime);

        if (currentTime <= 0)
        {
            GameManager.Instance.EndGame();
        }

        OnTimerUpdate?.Invoke(currentTime);
    }

    public void ResetTimer()
    {
        currentTime = levelTime;
    }

}
