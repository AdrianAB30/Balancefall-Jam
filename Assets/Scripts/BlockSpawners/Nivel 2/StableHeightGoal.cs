using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class StableHeightGoal : MonoBehaviour
{
    [Header("Condiciones de victoria")]
    public float requiredHeight = 10f;
    public string blockTag = "Bloque";

    [Header("Condición de derrota")]
    public float maxTimeToTry = 60f;
    public TextMeshProUGUI MaxHeight;

    [Header("Referencias")]
    public GameManager gameManager;
    public Image fadeImage;

    private float globalTimer = 0f;
    private bool levelEnded = false;

    void Update()
    {
        if (levelEnded) return;

        globalTimer += Time.deltaTime;

        float maxHeight = GetMaxPlacedBlockHeight();

        if (maxHeight >= requiredHeight)
        {
            levelEnded = true;
            SceneManager.LoadScene("Nivel 3");
            gameManager.LevelComplete("✅ ¡Torre alcanzó la altura!");
            

        }

        if (globalTimer >= maxTimeToTry)
        {
            levelEnded = true;
            StartCoroutine(FadeAndRestart());
        }
    }

    float GetMaxPlacedBlockHeight()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag(blockTag);
        float maxY = 0f;

        foreach (var block in blocks)
        {
            // Verifica si ya no tiene BlockFallController (ya se soltó)
            if (block.GetComponent<BlockFallController>() == null)
            {
                float y = block.transform.position.y;

                // Opcional: ignorar si el bloque está muy arriba flotando
                if (y > maxY && y < 100f)
                {
                    maxY = y;
                    MaxHeight.text = "Max Height " + maxY.ToString("f1");
                }
            }
        }

        return maxY;
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
