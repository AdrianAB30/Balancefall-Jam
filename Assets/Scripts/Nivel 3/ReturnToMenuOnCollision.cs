using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ReturnToMenuOnCollision : MonoBehaviour
{
    public string cubeTag = "Cubo"; 
    public string menuSceneName = "Menu";
    public GameObject spawner;
    public Image fadeImage; 
    public float fadeDuration = 2f;

    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag(cubeTag))
        {
            triggered = true;
            spawner.SetActive(false);
            StartCoroutine(FadeAndReturnToMenu());
        }
    }

    IEnumerator FadeAndReturnToMenu()
    {
        float t = 0f;
        Color color = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene(menuSceneName);
    }
}
