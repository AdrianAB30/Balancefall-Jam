using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject settingsPanel; 
    public GameObject levelsPanel;
    public GameObject creditsPanel;
    public GameObject mainMenuPanel;
    public Image fadeMenu;
    public float durationFade;

    public void Start()
    {
        fadeMenu.gameObject.SetActive(true);
        StartCoroutine(FadeIn());
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Nivel 1");
    }

    public void OpenLevels()
    {
      
        levelsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
    public void CloseLevels()
    {

        levelsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);

    }
    public void OpenCredits()
    {
       
        creditsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
    public void OpenSettings()
    {
        
        settingsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void QuitGame()
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    IEnumerator FadeIn()
    {
        float elapsed = 0f;
        Color color = fadeMenu.color;
        color.a = 1f; 
        fadeMenu.color = color;

        while (elapsed < durationFade)
        {
            elapsed += Time.deltaTime;
            color.a = 1f - (elapsed / durationFade);
            fadeMenu.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeMenu.color = color;
        fadeMenu.gameObject.SetActive(false);
    }
}