using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RectTransform[] panelLevels;

    [Header("Dotween")]
    [SerializeField] Ease easeAnimation;
    [SerializeField] private float duration;
    [SerializeField] private Vector3[] targetPositions;
    private Vector3[] originalPositions;


    public void LevelComplete(string message)
    {
        Debug.Log(message);
        Time.timeScale = 0f;   
    }

    public void ChangeScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
