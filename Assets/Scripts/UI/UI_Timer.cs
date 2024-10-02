using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI minText;
    [SerializeField] private TextMeshProUGUI secText;

    // Start is called before the first frame update
    void Start()
    {
        TimerManager.Instance.OnTimerUpdate += DisplayTime;
    }

    private void OnDestroy()
    {
        TimerManager.Instance.OnTimerUpdate -= DisplayTime;
    }

    private void DisplayTime(float time)
    {
        int timeLeft = (int)time;

        minText.text = (timeLeft / 60).ToString();
        secText.text = (timeLeft % 60).ToString("00");
    }
}
