using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    void Start()
    {
        // Load saved volume or default to 0.75 (in linear scale)
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.75f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);
        
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        // Convert linear (0.0001–1) to decibels (-80 to 0)
        float volumeInDb = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("MasterVolume", volumeInDb);
        PlayerPrefs.SetFloat("Volume", value);
    }
}