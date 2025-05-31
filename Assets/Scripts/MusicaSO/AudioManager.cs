using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider VolumenMaestro;
    [SerializeField] private Slider VolumenMusica;
    [SerializeField] private Slider VolumenMusicaSFX;
    [SerializeField] private Volumen audioSettings;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        LoadVolumeSettings();
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
}