using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StarCounter : MonoBehaviour
{
    [Header("Display Delay Properties")]
    [SerializeField] private float showStarDelay = 1f;

    [Header("Star Display Properties")]
    [SerializeField] private Image[] images = new Image[3];

    [Header("SFX Properties")]
    [SerializeField] private AudioClip starSound;

    public void ShowStars(int number)
    {
        StartCoroutine(ShowStarsHelper(number));
    }

    IEnumerator ShowStarsHelper(int number)
    {
        if (number <= 0)
        {
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
