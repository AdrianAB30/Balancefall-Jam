using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TimeGoalCheckerNivel1 : MonoBehaviour
{
    [Header("Condiciones de victoria")]
    public float timeToWin = 30f;
    public string blockTag = "Cubo";

    [Header("Condición de derrota")]
    public float maxTimeToTry = 60f;

    [Header("Referencias")]
    public GameManager gameManager;
    public Image fadeImage;
    public GameObject platform;
    public BlockCounterPlatform contadorPlataforma;
    public BlockSpawnernivel1 spawner;

    private float survivalTimer = 0f;
    private float globalTimer = 0f;
    private bool levelEnded = false;

    private int minimumBlocksRequired;

    void Start()
    {
        // Tomar el valor generado por el spawner
        minimumBlocksRequired = BlockSpawnernivel1.MinBlocksRequired;
        Debug.Log($"📋 TimeGoalChecker: usando mínimo requerido = {minimumBlocksRequired}");
    }

    void Update()
    {
        if (levelEnded) return;

        globalTimer += Time.deltaTime;

        int currentBlockCount = contadorPlataforma.BloquesEnPlataforma;

        if (currentBlockCount >= minimumBlocksRequired)
        {
            survivalTimer += Time.deltaTime;

            if (survivalTimer >= timeToWin)
            {
                levelEnded = true;
                Rigidbody rb = platform.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
                spawner.DetenerSpawning();
                gameManager.LevelComplete("✅ ¡Superaste el desafío de tiempo!");
            }
        }
        else
        {
            survivalTimer = 0f;
        }

        if (globalTimer >= maxTimeToTry)
        {
            levelEnded = true;
            spawner.DetenerSpawning();
            StartCoroutine(FadeAndRestart());
        }
    }

    IEnumerator FadeAndRestart()
    {
        Debug.Log("❌ Tiempo agotado. Reiniciando nivel...");
        Time.timeScale = 1f;

        float duration = 2f;
        float t = 0f;
        Color color = fadeImage.color;

        while (t < duration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, t / duration);
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
