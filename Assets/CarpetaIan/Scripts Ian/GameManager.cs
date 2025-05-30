using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject winPanel;

    public void LevelComplete(string message)
    {
        Debug.Log(message);

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

       
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
