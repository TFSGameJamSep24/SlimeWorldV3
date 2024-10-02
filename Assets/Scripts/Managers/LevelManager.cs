using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private AudioClip levelTheme;

    // Start is called before the first frame update
    void Start()
    {
        if (levelTheme != null) AudioManager.instance.PlayMusic(levelTheme);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
