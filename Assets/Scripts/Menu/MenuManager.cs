using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject settingsPanel; 
    public GameObject levelsPanel;
    public GameObject creditsPanel;
    public GameObject mainMenuPanel;

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
}