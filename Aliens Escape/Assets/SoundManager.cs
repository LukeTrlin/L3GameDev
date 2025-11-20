using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicController : MonoBehaviour
{
    [Header("Music")]
    public AudioSource musicSource;      // Background music
    public Slider musicSlider;           // Slider for music volume
    public TextMeshProUGUI musicVolumeText; // Text for music %

    [Header("Sound Effects")]
    public AudioClip shootClip; // assign shooting sound
    public AudioSource sfxSource;        // AudioSource for SFX
    public Slider sfxSlider;             // Slider for SFX volume
    public TextMeshProUGUI sfxVolumeText; // Text for SFX %
    public AudioClip hitEnemyClip;       // Hit enemy sound

    public AudioClip interactSound;

    public AudioClip reloadClip;      // Player reload sound
    public AudioClip monsterDeathClip; // Zombie death sound
    private static MusicController instance;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    void Awake()
    {
        
    }

    void Start()
    {
        // Load saved music volume
        float savedMusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
        musicSource.volume = savedMusicVolume;

        if (musicSlider != null)
        {
            musicSlider.value = savedMusicVolume;
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }
        UpdateMusicText(savedMusicVolume);

        // Load saved SFX volume
        float savedSFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 1f);
        if (sfxSource != null)
            sfxSource.volume = savedSFXVolume;

        if (sfxSlider != null)
        {
            sfxSlider.value = savedSFXVolume;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }
        UpdateSFXText(savedSFXVolume);

        // Play music
        if (!musicSource.isPlaying)
            musicSource.Play();
    }

    // Music volume
    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
        PlayerPrefs.SetFloat(MusicVolumeKey, value);
        PlayerPrefs.Save();
        UpdateMusicText(value);
    }

    private void UpdateMusicText(float value)
    {
        if (musicVolumeText != null)
            musicVolumeText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    // SFX volume
    public void SetSFXVolume(float value)
    {
        if (sfxSource != null)
            sfxSource.volume = value;

        PlayerPrefs.SetFloat(SFXVolumeKey, value);
        PlayerPrefs.Save();
        UpdateSFXText(value);
    }

    private void UpdateSFXText(float value)
    {
        if (sfxVolumeText != null)
            sfxVolumeText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    // Play hit enemy sound
    public void PlayHitEnemySound()
    {
        if (hitEnemyClip != null)
        {
            if (sfxSource != null)
                sfxSource.PlayOneShot(hitEnemyClip, sfxSource.volume);
        }
    }

    public void PlayShootSound()
{
    if (shootClip != null && sfxSource != null)
    {
        sfxSource.PlayOneShot(shootClip, sfxSource.volume);
    }
}
    public void PlayReloadSound()
{
    if (reloadClip != null && sfxSource != null)
        sfxSource.PlayOneShot(reloadClip, sfxSource.volume);
}

public void PlayMonsterDeathSound()
{
    if (monsterDeathClip != null && sfxSource != null)
        sfxSource.PlayOneShot(monsterDeathClip, sfxSource.volume);
}

public void PlayInteractSound()
{
    if (sfxSource != null && interactSound != null)
        sfxSource.PlayOneShot(interactSound);
}



    // Singleton accessor
    public static MusicController Instance => instance;
}