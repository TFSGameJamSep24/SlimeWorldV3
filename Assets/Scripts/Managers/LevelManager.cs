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

    private bool isTransitioning = false;

    public delegate void LevelEnd();
    public event LevelEnd OnLevelEnd;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (levelTheme != null) AudioManager.instance.PlayMusic(levelTheme);
    }

    public void EndLevel()
    {
        if (isTransitioning) return;

        points = CollectorManager.instance.GetTotalPoints();
        StartCoroutine(FadeToWinScreen());

        OnLevelEnd?.Invoke();
        isTransitioning = true;
    }

    IEnumerator FadeToWinScreen()
    {
        yield return new WaitForSeconds(winScreenDelay);
        TransitionManager.instance.FadeInWhite("WinScreen");
    }

    public int GetPoints()
    {
        return points;
    }

    public ref int[] GetStarValues()
    {
        return ref starValues;
    }
}