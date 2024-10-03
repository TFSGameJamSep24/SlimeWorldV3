using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class WinScreenManager : MonoBehaviour
{
    [Header("Astronaut Animator")]
    [SerializeField] private Animator anim;
    [SerializeField] private float[] yRotation = new float[4];

    private int starCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (TransitionManager.instance != null) TransitionManager.instance.FadeOutWhite();

        if (LevelManager.instance != null) CheckScore();
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

        RotatePlayer(starCount);

        switch (starCount) 
        {
            case 0:
                if (anim != null) anim.Play("Pout");
                break;
            case 1:
                starCounter.ShowStars(starCount);
                if (anim != null) anim.Play("Victory");
                break;
            case 2:
                starCounter.ShowStars(starCount);
                if (anim != null) anim.Play("Shrug");
                break;
            case 3:
                starCounter.ShowStars(starCount);
                if (anim != null) anim.Play("Dance");
                break;
        }
    }

    private void RotatePlayer(int index)
    {
        Quaternion helper = anim.transform.rotation;

        helper.y = yRotation[index] * Mathf.Deg2Rad;

        anim.transform.rotation = helper;
    }
}
