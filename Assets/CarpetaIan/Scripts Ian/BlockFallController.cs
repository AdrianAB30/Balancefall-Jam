using UnityEngine;
using System;

public class BlockFallController : MonoBehaviour
{
    private BlockData data;
    private Rigidbody rb;
    private float startTime;
    private Action onLanded;

    public float moveSpeed = 5f;

    private bool isFalling = true;

    public void Initialize(BlockData blockData, Action onLand)
    {
        data = blockData;
        startTime = Time.time;
        onLanded = onLand;
        rb = GetComponent<Rigidbody>();
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

        Vector3 move = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isFalling) return;
        rb.useGravity = true;
        isFalling = false;
        onLanded?.Invoke();
        Destroy(this); 
    }
}
