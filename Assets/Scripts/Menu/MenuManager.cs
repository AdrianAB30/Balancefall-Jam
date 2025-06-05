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
    public GameObject spawnCube;
    public Ease myEase;

    private void Start()
    {
        MovePlatform();
        ShowButtonsMenu();
    }
    public void PlayGame()
    {
        fadeManager.FadeToScene("Nivel 1");
    }

    public void OpenLevels()
    {
        levelsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        spawnCube.SetActive(false);
        platform.gameObject.SetActive(false);
    }
    public void CloseLevels()
    {
        spawnCube.SetActive(true);
        platform.gameObject.SetActive(true);
        levelsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        spawnCube.SetActive(false);
        platform.gameObject.SetActive(false);
    }
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        spawnCube.SetActive(false);
        platform.gameObject.SetActive(false);
    }

    public void QuitGame()
    {     
        Application.Quit();
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        mainMenuPanel.SetActive(true);
        spawnCube.SetActive(true);
        platform.gameObject.SetActive(true);
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
       .AppendInterval(5f)
       .Append(platform.DOMove(new Vector3(25,-11,13), timeDtwn).SetEase(myEase));

    }
}