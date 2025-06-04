using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [Header("Fade Settings")]
    public Image fadeImage;           
    public float fadeDuration = 1f;   
    public bool fadeOnStart = true;   

    void Start()
    {
        if (fadeOnStart && fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            Color c = fadeImage.color;
            c.a = 1f;
            fadeImage.color = c;
            StartCoroutine(Fade(1f, 0f)); 
        }
    }

    /// <summary>
    /// Llama a esta función para hacer un fade out y cambiar de escena después.
    /// </summary>
    public void FadeToScene(string sceneName)
    {
        if (fadeImage != null)
        {
            StartCoroutine(FadeAndLoadScene(sceneName));
        }
    }

    /// <summary>
    /// Solo hace fade in o fade out según los valores pasados.
    /// </summary>
    public void FadeManual(float fromAlpha, float toAlpha)
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            Color c = fadeImage.color;
            c.a = fromAlpha;
            fadeImage.color = c;
            StartCoroutine(Fade(fromAlpha, toAlpha));
        }
    }

    IEnumerator FadeAndLoadScene(string sceneName)
    {
        fadeImage.gameObject.SetActive(true);
        yield return Fade(0f, 1f);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator Fade(float from, float to)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }

        color.a = to;
        fadeImage.color = color;

        if (to == 0f)
            fadeImage.gameObject.SetActive(false);
    }
}
