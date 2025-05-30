using UnityEngine;
using System.Collections;

public class FlashManager : MonoBehaviour
{
    [SerializeField] private float flashDuration = 0.3f;
    [SerializeField] private Color flashColor = new Color32(255, 169, 0, 255);
    [SerializeField] private float flashIntensity = 0.5f;

    private Color originalAmbientColor;
    private bool isFlashing = false;

    void Start()
    {
        originalAmbientColor = RenderSettings.ambientLight;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isFlashing)
        {
            StartCoroutine(FlashAmbientLightSmooth());
        }
    }

    private IEnumerator FlashAmbientLightSmooth()
    {
        isFlashing = true;

        Color targetColor = flashColor * flashIntensity;
        float timer = 0f;

        while (timer < flashDuration)
        {
            RenderSettings.ambientLight = Color.Lerp(originalAmbientColor, targetColor, timer / flashDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        RenderSettings.ambientLight = targetColor;

        yield return new WaitForSeconds(0.05f);

        timer = 0f;
        while (timer < flashDuration)
        {
            RenderSettings.ambientLight = Color.Lerp(targetColor, originalAmbientColor, timer / flashDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        RenderSettings.ambientLight = originalAmbientColor;

        isFlashing = false;
    }
}
