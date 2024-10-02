using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;
    public string nextLevel;
    private Animator anim;

    private void Awake()
    {
        if (instance == null) instance = this;
        anim = GetComponent<Animator>();
    }

    public void FadeInWhite(string nextLevelName)
    {
        nextLevel = nextLevelName;
        anim.Play("FadeInWhite");
    }

    public void FadeOutWhite()
    {
        anim.Play("FadeOutWhite");
    }

    private void FadeInBlack()
    {
        anim.Play("FadeInBlack");
    }

    private void FadeOutBlack()
    {
        anim.Play("FadeOutBlack");
    }

    public void GoToLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
