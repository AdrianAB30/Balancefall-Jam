using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class SceneMusic
{
    public string sceneName;
    public AudioSource musicSource;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Settings")]
    [SerializeField] private AudioSettings audioSettings;
    [SerializeField] private AudioMixer myAudioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Dotween Audio")]
    [SerializeField] private SceneMusic[] sceneMusics;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float fadeDurationMenu = 3f; 
    [SerializeField] private float maxVolume = 1f;

    private AudioSource currentMusicSource;
    private string currentScene;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        LoadVolume();
        currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Menu")
        {
            StartCoroutine(FadeInMenuMusicCoroutine());
        }
        else
        {
            PlaySceneMusic(currentScene);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlaySceneMusic(scene.name);
    }

    public void LoadVolume()
    {
        masterSlider.value = audioSettings.masterVolume;
        musicSlider.value = audioSettings.musicVolume;
        sfxSlider.value = audioSettings.sfxVolume;
    }

    public void SetMasterVolume()
    {
        audioSettings.masterVolume = masterSlider.value;
        myAudioMixer.SetFloat("Master", Mathf.Log10(audioSettings.masterVolume) * 20);
    }

    public void SetMusicVolume()
    {
        audioSettings.musicVolume = musicSlider.value;
        myAudioMixer.SetFloat("Musica", Mathf.Log10(audioSettings.musicVolume) * 20);
    }

    public void SetSfxVolume()
    {
        audioSettings.sfxVolume = sfxSlider.value;
        myAudioMixer.SetFloat("SFX", Mathf.Log10(audioSettings.sfxVolume) * 20);
    }

    private void PlaySceneMusic(string sceneName)
    {
        foreach (SceneMusic sm in sceneMusics)
        {
            if (sm.sceneName == sceneName)
            {
                if (currentMusicSource == sm.musicSource && sm.musicSource.isPlaying)
                    return;

                float fade = (sceneName == "MenuPrincipal") ? fadeDurationMenu : fadeDuration;

                if (currentMusicSource != null)
                {
                    currentMusicSource.DOFade(0, fade).OnComplete(() =>
                    {
                        currentMusicSource.Stop();
                        StartNewMusic(sm, fade);
                    });
                }
                else
                {
                    StartNewMusic(sm, fade);
                }

                return;
            }
        }
    }
    private IEnumerator FadeInMenuMusicCoroutine()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        for (int i = 0; i < sceneMusics.Length; i++)
        {
            if (sceneMusics[i].sceneName == currentScene)
            {
                currentMusicSource = sceneMusics[i].musicSource;
                currentMusicSource.volume = 0;
                currentMusicSource.Play();

                yield return null;

                currentMusicSource.DOFade(maxVolume, fadeDurationMenu);
                break;
            }
        }
    }

    private void StartNewMusic(SceneMusic sm, float fade)
    {
        currentMusicSource = sm.musicSource;
        currentMusicSource.volume = 0;
        currentMusicSource.Play();
        currentMusicSource.DOFade(maxVolume, fade);
    }
}
