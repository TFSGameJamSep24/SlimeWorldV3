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

    [Header("Tutorial Properties")]
    [SerializeField] private bool isTutorialShown = false;

    [Header("LevelSelect Properties")]
    [SerializeField] private string[] levels = new string[3];
    [SerializeField] private Transform[] levelPlanets = new Transform[3];
    private int levelIndex = -1;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
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

    public void ChooseLevel(int index)
    {
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
        if (TransitionManager.instance) TransitionManager.instance.FadeInWhite(levels[levelIndex]);
    }

    private void ResetState()
    {
        if (isTutorialShown) anim.Play("HideTutorial");
    }
}