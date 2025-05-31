using UnityEngine;

public class FarolaMovimiento : MonoBehaviour
{
    public float fallSpeed = 2f;           // Velocidad hacia arriba
    public float rotationAmplitude = 5f;   // Grados de oscilación (de -5 a 5)
    public float rotationSpeed = 2f;       // Velocidad de oscilación
    private float destroyY = 12f;

    private float timeOffset;              // Para que cada farola tenga una oscilación distinta

    void Start()
    {
        timeOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        // Movimiento hacia arriba
        transform.position += Vector3.up * fallSpeed * Time.deltaTime;

        // Rotación oscilante en Z
        float zRotation = Mathf.Sin(Time.time * rotationSpeed + timeOffset) * rotationAmplitude;
        transform.rotation = Quaternion.Euler(0f, 0f, zRotation);

        // Destruir al salir de la pantalla
        if (transform.position.y >= destroyY)
        {
            Destroy(gameObject);
        }
    }
}