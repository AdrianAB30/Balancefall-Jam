using UnityEngine;
using System;

public class BlockFallController : MonoBehaviour
{
    private BlockData data;
    private Rigidbody rb;
    private float startTime;
    private Action onLanded;

    public float moveSpeed = 5f;
    public float rotationDuration = 1f; // Duración de la rotación en segundos

    private bool isFalling = true;
    private bool isRotating = false;
    private Quaternion targetRotation;
    private float rotationTime;

    public void Initialize(BlockData blockData, Action onLand)
    {
        data = blockData;
        startTime = Time.time;
        onLanded = onLand;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        if (!isFalling) return;

        float elapsed = Time.time - startTime;
        float fallSpeed = data.fallSpeedCurve.Evaluate(elapsed);
        Vector3 newVelocity = new Vector3(rb.linearVelocity.x, -fallSpeed, rb.linearVelocity.z);
        rb.linearVelocity = newVelocity;
    }

    void Update()
    {
        if (!isFalling) return;

        // Movimiento WASD
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);

        // Rotación por pasos
        if (!isRotating)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartRotation(Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, 90)));
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                StartRotation(Quaternion.Euler(transform.eulerAngles + new Vector3(90, 0, 0)));
            }
        }
        else
        {
            // Lerp de rotación
            rotationTime += Time.deltaTime;
            float t = Mathf.Clamp01(rotationTime / rotationDuration);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, t);

            if (t >= 1f)
                isRotating = false;
        }
    }

    void StartRotation(Quaternion newRotation)
    {
        targetRotation = newRotation;
        rotationTime = 0f;
        isRotating = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isFalling) return;

        isFalling = false;
        rb.useGravity = true;
        onLanded?.Invoke();
        Destroy(this); // Desactiva el control
    }
}
