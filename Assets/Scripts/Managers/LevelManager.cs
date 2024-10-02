using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private AudioClip levelTheme;

    [SerializeField] private int[] starValues = new int[3];
    [SerializeField] private int points;

    [Header("Transition Properties")]
    [SerializeField] private float winScreenDelay = 4f;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (levelTheme != null) AudioManager.instance.PlayMusic(levelTheme);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.M)) StartCoroutine(FadeToWinScreen());
    }

    public void EndLevel()
    {
        points = CollectorManager.instance.GetTotalPoints();
    }

    IEnumerator FadeToWinScreen()
    {
        yield return new WaitForSeconds(winScreenDelay);
        TransitionManager.instance.FadeInWhite("WinScreen");
    }
}