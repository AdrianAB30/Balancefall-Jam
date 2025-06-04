using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject settingsPanel; 
    public GameObject levelsPanel;
    public GameObject creditsPanel;
    public GameObject mainMenuPanel;
    public float durationFade;
    public float timeDtwn;
    public float intervalTime;
    public FadeManager fadeManager;
    public List<Vector3> targetsButtons;
    public List<RectTransform> buttons;
    public Transform platform;
    public Ease myEase;

    private void Start()
    {
        ShowButtonsMenu();
    }
    public void PlayGame()
    {
        fadeManager.FadeToScene("Nivel1");
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
        Application.Quit();
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    private void ShowButtonsMenu()
    {
        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < buttons.Count; i++)
        {
            sequence.Append(buttons[i].DOAnchorPos(targetsButtons[i], timeDtwn).SetEase(myEase));
        }
    }
    public void MovePlatform()
    {
        DOTween.Sequence()
       .AppendInterval(8f)
       .Append(platform.DOMove(new Vector3(25,-11,13), timeDtwn).SetEase(myEase));

    }
}