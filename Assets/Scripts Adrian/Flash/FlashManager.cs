using UnityEngine;
using System.Collections;

public class FlashManager : MonoBehaviour
{
    [SerializeField] private float flashDuration = 0.3f;
    [SerializeField] private float flashIntensity = 0.5f;

    private Color originalAmbientColor;
    private bool isFlashing = false;


    void OnEnable()
    {
        BlockSpawner.OnBlockSpawned += FlashWithColor;
    }

    void OnDisable()
    {
        BlockSpawner.OnBlockSpawned -= FlashWithColor;
    }

    void Start()
    {
        originalAmbientColor = RenderSettings.ambientLight;
    }

    public void FlashWithColor(Color32 color)
    {
        if (!isFlashing)
        {
            StartCoroutine(FlashAmbientLightSmooth((Color)color));
        }
    }

    private IEnumerator FlashAmbientLightSmooth(Color32 flashColor)
    {
        isFlashing = true;

        Color targetColor = ((Color)flashColor) * flashIntensity;
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
