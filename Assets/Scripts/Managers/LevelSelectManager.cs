using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    [Header("Camera Properties")]
    [SerializeField] private CinemachineVirtualCamera cam;

    [Header("Audio Properties")]
    [SerializeField] private AudioClip levelSelectTheme;
    [SerializeField] private AudioClip buttonPressSFX;
    [SerializeField] private AudioClip goToLevelSFX;

    [Header("Tutorial Properties")]
    [SerializeField] private bool isTutorialShown = false;

    [Header("Credit Properties")]
    [SerializeField] private bool showCredits = false;

    [Header("LevelSelect Properties")]
    [SerializeField] private string[] levels = new string[3];
    [SerializeField] private Transform[] levelPlanets = new Transform[3];
    private int levelIndex = 0;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        if (LevelManager.instance) Destroy(LevelManager.instance);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (TransitionManager.instance) TransitionManager.instance.FadeOutWhite();
        if (AudioManager.instance) AudioManager.instance.PlayMusic(levelSelectTheme);
    }

    public void ShowTutorial()
    {
        Debug.Log("Pressing");
        isTutorialShown = !isTutorialShown;

        if (isTutorialShown) anim.Play("ShowTutorial");
        else anim.Play("HideTutorial");
    }

    public void ShowCredits()
    {
        Debug.Log("Credits");
        showCredits = !showCredits;

        if (AudioManager.instance) AudioManager.instance.PlaySFX(buttonPressSFX);

        if (showCredits) anim.Play("ShowCredits");
        else anim.Play("HideCredits");
    }

    public void ChooseLevel(int index)
    {
        if (AudioManager.instance) AudioManager.instance.PlaySFX(buttonPressSFX);

        ResetState();
        levelIndex = index;
        cam.Follow = levelPlanets[levelIndex];
        cam.LookAt = levelPlanets[levelIndex];
    }

    public void SelectLevel()
    {
        anim.Play("SelectLevel");
    }

    public void GoToLevel()
    {
        if (TransitionManager.instance)
        {
            if (AudioManager.instance) AudioManager.instance.PlayMusic(goToLevelSFX);
            TransitionManager.instance.FadeInWhite(levels[levelIndex]);
        }
    }

    public void GoToLevelSelect()
    {
        if (AudioManager.instance) AudioManager.instance.PlaySFX(buttonPressSFX);
        TransitionManager.instance.FadeInWhite("LevelSelect");
    }

    private void ResetState()
    {
        if (isTutorialShown) anim.Play("HideTutorial");
    }
}