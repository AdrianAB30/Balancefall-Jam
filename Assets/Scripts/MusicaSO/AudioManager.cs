using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Mixer y Sliders")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider VolumenMaestro;
    [SerializeField] private Slider VolumenMusica;
    [SerializeField] private Slider VolumenMusicaSFX;
    [SerializeField] private Volumen audioSettings;

    [Header("Música por escena")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip nivel1Music;
    [SerializeField] private AudioClip nivel2Music;
    [SerializeField] private AudioClip nivel3Music;
    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            musicSource.volume = 0.2f;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
       
    }

    private void Start()
    {
        LoadVolumeSettings();
        PlayMusicForScene();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void LoadVolumeSettings()
    {
        VolumenMaestro.value = audioSettings.Master;
        VolumenMusica.value = audioSettings.Musica;
        VolumenMusicaSFX.value = audioSettings.SFX;
        SetMasterVolumen();
        SetMusicVolumen();
        SetSFXVolumen();
    }

    public void SetMasterVolumen()
    {
        float volumen = VolumenMaestro.value;
        audioMixer.SetFloat("Master", Mathf.Log10(volumen) * 20);
        audioSettings.Master = volumen;
    }

    public void SetMusicVolumen()
    {
        float volumen = VolumenMusica.value;
        audioMixer.SetFloat("Musica", Mathf.Log10(volumen) * 20);
        audioSettings.Musica = volumen;
    }

    public void SetSFXVolumen()
    {
        float volumen = VolumenMusicaSFX.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volumen) * 20);
        audioSettings.SFX = volumen;
    }

    private void PlayMusicForScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        AudioClip newClip = null;

        switch (currentScene)
        {
            case "Menu":
                newClip = menuMusic;
                break;
            case "Nivel 1":
                newClip = nivel1Music;
                break;
            case "Nivel 2":
                newClip = nivel2Music;
                break;
            case "Nivel 3":
                newClip = nivel3Music;
                break;
        }

        if (newClip != null)
        {
            StartCoroutine(FadeMusic(newClip));
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene();
    }

    private IEnumerator FadeMusic(AudioClip newClip, float duration = 1f)
    {
        if (musicSource.clip == newClip) yield break;

        float startVolume = musicSource.volume;

        // Fade out
        while (musicSource.volume > 0.1f)
        {
            musicSource.volume -= Time.deltaTime / duration;
            yield return null;
        }

        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.Play();

        // Fade in hasta el valor del slider
        float targetVolume = VolumenMusica.value;
        while (musicSource.volume < targetVolume)
        {
            musicSource.volume += Time.deltaTime / (duration * 10f); // un poco más lento
            yield return null;
        }

        musicSource.volume = targetVolume;
    }
}
