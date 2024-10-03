using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButtonController : MonoBehaviour
{
    public Button audioButton;
    public Slider volumeSlider;
    private bool isSliderVisible = false;


    private void Start()
    {
        volumeSlider.gameObject.SetActive(false);
        volumeSlider.interactable = false;
    }

    public void ToggleSliderVisibility()
    {
        isSliderVisible = !isSliderVisible;

        volumeSlider.gameObject.SetActive(isSliderVisible);
        volumeSlider.interactable = isSliderVisible;
    }

    public void SetVolume(float volume)
    {
        Debug.Log("Volume set to: " + volume);
        AudioListener.volume = volume;
    }
}
