using UnityEngine;
using System.Collections;

public class EarthquakeController : MonoBehaviour
{
    [SerializeField] private float durationVisual;
    [SerializeField] private float intensityVisual;
    [SerializeField] private float durationShake;
    [SerializeField] private float intensityShake;
    [SerializeField] private Transform sceneRoot;
    [SerializeField] private AnimationCurve shakeCurve;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = sceneRoot.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TriggerEarthquake();
        }
    }

    public void TriggerEarthquake()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeVisual());
        StartCoroutine(Shake());
    }

    IEnumerator ShakeVisual()
    {
        float time = 0f;
        while (time < durationVisual)
        {
            sceneRoot.position = originalPosition + Random.insideUnitSphere * intensityVisual;
            time += Time.deltaTime;
            yield return null;
        }
        sceneRoot.position = originalPosition;
    }

    IEnumerator Shake()
    {
        float time = 0f;
        Rigidbody[] rigidbodies = sceneRoot.GetComponentsInChildren<Rigidbody>();

        while (time < durationShake)
        {
            float normalizedTime = time / durationShake;
            float forceMultiplier = shakeCurve.Evaluate(normalizedTime); 

            foreach (Rigidbody rb in rigidbodies)
            {
                Vector3 randomForce = Random.onUnitSphere * intensityShake * forceMultiplier * 100f;
                rb.AddForce(randomForce, ForceMode.Impulse);
            }

            time += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
