using UnityEngine;
using System; // Dibutuhkan untuk Array.Find

// Membuat class baru untuk menampung nama dan file audio agar muncul di Inspector
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("SFX Library")]
    public Sound[] sfxLibrary; // Tempat memasukkan banyak SFX di Inspector

    private const string MUSIC_KEY = "MusicVolume";
    private const string SFX_KEY = "SFXVolume";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadVolume();
    }

    public void SetMusicVolume(float volume)
    {
        float normalizedVolume = volume / 100f;
        if (musicSource != null)
            musicSource.volume = normalizedVolume;

        PlayerPrefs.SetFloat(MUSIC_KEY, volume);
    }

    public void SetSFXVolume(float volume)
    {
        float normalizedVolume = volume / 100f;
        if (sfxSource != null)
            sfxSource.volume = normalizedVolume;

        PlayerPrefs.SetFloat(SFX_KEY, volume);
    }

    // CARA 1: Memutar SFX berdasarkan NAMA yang ada di Library (Sangat disarankan)
    public void PlaySFX(string soundName)
    {
        Sound s = Array.Find(sfxLibrary, sound => sound.name == soundName);
        if (s != null && s.clip != null)
        {
            sfxSource.PlayOneShot(s.clip);
        }
        else
        {
            Debug.LogWarning("SFX dengan nama: " + soundName + " tidak ditemukan!");
        }
    }

    // CARA 2: Memutar SFX langsung menggunakan AudioClip (Opsi cadangan)
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public float GetMusicVolume() => PlayerPrefs.GetFloat(MUSIC_KEY, 100f);
    public float GetSFXVolume() => PlayerPrefs.GetFloat(SFX_KEY, 100f);

    private void LoadVolume()
    {
        SetMusicVolume(GetMusicVolume());
        SetSFXVolume(GetSFXVolume());
    }
}