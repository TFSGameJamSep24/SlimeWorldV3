using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button audioButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private Slider volumeSlider;

    private bool isMuted = false;

    [Header("AudioProperties")]
    [SerializeField] private AudioClip menuTheme;

    private void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        audioButton.onClick.AddListener(ToggleAudio);
        creditsButton.onClick.AddListener(ToggleCredits);

        //creditsPanel.SetActive(false);

        //Set initial volume to the saved value
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        AudioListener.volume = savedVolume;
        volumeSlider.value = savedVolume;

        volumeSlider.onValueChanged.AddListener(AdjustVolume);

        if (menuTheme) AudioManager.instance.PlayMusic(menuTheme); 
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    private void ToggleAudio()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0f : volumeSlider.value;
    }

    private void AdjustVolume(float volume)
    {
        if (!isMuted)
        {
            AudioListener.volume = volume;
        }
        PlayerPrefs.SetFloat("Volume", volume);
    }

    private void ToggleCredits()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }





}
