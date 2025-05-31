using UnityEngine;
using System;

public class BlockFallController : MonoBehaviour
{
    private BlockData data;
    private Rigidbody rb;
    private float startTime;
    private Action onLanded;
    private Transform cam;


    public float moveSpeed = 5f;
    public float rotationDuration = 1f; 

    private bool isFalling = true;
    private bool isRotating = false;
    private Quaternion targetRotation;
    private float rotationTime;

    public void Initialize(BlockData blockData, Action onLand)
    {
        cam = Camera.main.transform;
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

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
       

        Vector3 right = cam.right;
        Vector3 forward = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;

        Vector3 direction = (right * moveX + forward * moveZ).normalized;
        Vector3 move = direction * moveSpeed * Time.deltaTime;

        transform.Translate(move, Space.World);

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
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vacio"))
        {
            onLanded?.Invoke();
            Destroy(gameObject); 
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isFalling) return;

        isFalling = false;
        rb.useGravity = true;
        onLanded?.Invoke();
        Destroy(this); 
    }
}
