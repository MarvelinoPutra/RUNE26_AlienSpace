using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    [Header("UI Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        SetupSliders();
    }

    private void SetupSliders()
    {
        if (AudioManager.Instance == null)
        {
            Debug.LogWarning("AudioManager tidak ditemukan di scene ini.");
            return;
        }

        if (musicSlider != null)
        {
            musicSlider.minValue = 0f;
            musicSlider.maxValue = 100f;
            // Ambil data volume terakhir yang tersimpan
            musicSlider.value = AudioManager.Instance.GetMusicVolume();

            musicSlider.onValueChanged.RemoveAllListeners();
            musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.minValue = 0f;
            sfxSlider.maxValue = 100f;
            // Ambil data volume terakhir yang tersimpan
            sfxSlider.value = AudioManager.Instance.GetSFXVolume();

            sfxSlider.onValueChanged.RemoveAllListeners();
            sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
        }
    }
}