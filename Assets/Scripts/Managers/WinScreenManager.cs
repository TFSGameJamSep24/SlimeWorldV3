using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class WinScreenManager : MonoBehaviour
{
    [Header("Ending Theme")]
    [SerializeField] private AudioClip winTheme;

    private int starCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (TransitionManager.instance != null) TransitionManager.instance.FadeOutWhite();

        if (LevelManager.instance != null) CheckScore();

        if (AudioManager.instance != null) AudioManager.instance.PlayMusic(winTheme);
    }

    private void CheckScore()
    {
        // Get number of points
        int points = LevelManager.instance.GetPoints();

        foreach (int i in LevelManager.instance.GetStarValues())
        {
            if (points >= i) starCount++;
        }


        // Compare to show number of stars and play animation
        UI_StarCounter starCounter = FindAnyObjectByType<UI_StarCounter>();

        if (!starCounter) return;

        switch (starCount) 
        {
            case 0:
                Debug.Log("Too bad");
                break;
            case 1:
                starCounter.ShowStars(starCount);
                break;
            case 2:
                starCounter.ShowStars(starCount);
                break;
            case 3:
                starCounter.ShowStars(starCount);
                break;
        }
    }
}
