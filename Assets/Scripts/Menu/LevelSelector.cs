using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [Header("References")]
    public RectTransform selector;
    public RectTransform[] levels;
    public Material Material0;
    public Material Material1;
    public Material Material2;
    public GameObject Fog;
    public float animationDuration = 0.3f;
    public float scaleFactor = 1.2f;
    public Camera mainCamera;

    private int currentIndex = 0;

    void Start()
    {
        UpdateSelection();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) MoveRight();
        if (Input.GetKeyDown(KeyCode.Q)) MoveLeft();
        if (Input.GetKeyDown(KeyCode.Space)) selectLevel();
    }

    public void selectLevel()
    {
        if (currentIndex == 0)
        {
            SceneManager.LoadScene("Nivel 1");
        }
        else if (currentIndex==1)
        {
            SceneManager.LoadScene("Nivel 2");
        }
        else if (currentIndex == 2)
        {
            SceneManager.LoadScene("Nivel 3");
        }
    }
    public void MoveLeft()
    {
        if (currentIndex > 0)
        {
            
            currentIndex--;
            UpdateSelection();
            
        }
        else if (currentIndex <= 0)
        {
            currentIndex = 2;
           UpdateSelection();
        }

       
    }

    public void MoveRight()
    {
        if (currentIndex < levels.Length - 1)
        {
            currentIndex++;
            UpdateSelection();
        }
        else if (currentIndex >= 2)
        {
            currentIndex = 0;
            UpdateSelection();
        }
        
    }

    private void UpdateSelection()
    {
        if (currentIndex == 0)
        {
            Fog.GetComponent<Renderer>().material = Material0;
             Camera.main.backgroundColor = new Color(10f / 255f, 15f / 255f, 44f / 255f, 0f);
            
        }
        else if (currentIndex == 1)
        {
            Fog.GetComponent<Renderer>().material = Material1;
             mainCamera.backgroundColor = new Color(30f / 255f, 27f / 255f, 23f / 255f, 0f);
            
        }
        else if (currentIndex == 2)
        {
            Fog.GetComponent<Renderer>().material = Material2;
             mainCamera.backgroundColor = new Color(169f / 255f, 205f / 255f, 235f / 255f, 0f);
            
        }
            selector.DOMove(levels[currentIndex].position, animationDuration);

        
        for (int i = 0; i < levels.Length; i++)
        {
            if (i == currentIndex)
            {
                levels[i].DOScale(scaleFactor+0.09f, animationDuration);
            }
            else
            {
                levels[i].DOScale(scaleFactor + 0.06f, animationDuration);
            }
        }
    }
}