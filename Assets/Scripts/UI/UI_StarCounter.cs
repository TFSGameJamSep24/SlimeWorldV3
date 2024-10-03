using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_StarCounter : MonoBehaviour
{
    [Header("Display Delay Properties")]
    [SerializeField] private float showStarDelay = 1f;
    [SerializeField] private TextMeshProUGUI score;

    [Header("Star Display Properties")]
    [SerializeField] private Image[] images = new Image[3];

    [Header("SFX Properties")]
    [SerializeField] private AudioClip starSound;

    [Header("Music Properties")]
    [SerializeField] private AudioClip winTheme;

    public void ShowStars(int number)
    {
        StartCoroutine(ShowStarsHelper(number));
        ShowScore();
    }

    private void ShowScore()
    {
        if (LevelManager.instance) score.text = LevelManager.instance.GetPoints().ToString();
    }

    IEnumerator ShowStarsHelper(int number)
    {
        if (number <= 0)
        {
            if (winTheme != null) AudioManager.instance.PlayMusic(winTheme);
            
            yield return null;
        }    
        
        else
        {
            yield return new WaitForSeconds(showStarDelay);

            foreach (Image star in images)
            {
                if (!star.gameObject.activeSelf)
                {
                    if (starSound != null) AudioManager.instance.PlaySFX(starSound);
                    star.gameObject.SetActive(true);
                    break;
                }
            }

            ShowStars(number - 1);
        }
    }
}
