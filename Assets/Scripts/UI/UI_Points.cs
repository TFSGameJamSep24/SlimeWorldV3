using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Points : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    private int pointsCount;

    private void Awake()
    {
        pointsText.GetComponentInChildren<TextMeshProUGUI>();
        pointsCount = 0;
        pointsText.text = pointsCount.ToString();
    }

    void Start()
    {
        CollectorManager.instance.OnCollect += AddPoints;
    }

    private void OnDestroy()
    {
        CollectorManager.instance.OnCollect -= AddPoints;
    }

    private void AddPoints(int points)
    {
        pointsCount += points;
        pointsText.text = pointsCount.ToString();
    }
}
