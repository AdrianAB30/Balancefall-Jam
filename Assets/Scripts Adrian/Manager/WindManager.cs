using UnityEngine;
using System.Collections;

public class WindManager : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float windStrength = 20f;
    [SerializeField] private Vector3 boxSize = new Vector3(10f, 5f, 10f);
    [SerializeField] private LayerMask affectedLayers;
    [SerializeField] private float leftLimit = -10f;
    [SerializeField] private float rightLimit = 10f;
    [SerializeField] private float waitTime = 60f;

    public static Vector3 CurrentWind { get; private set; }

    private int direction = 1;
    private bool isWaiting = false;

    void Update()
    {
        if (isWaiting) return;

        transform.position += Vector3.right * direction * speed * Time.deltaTime;
        CurrentWind = Vector3.right * direction * windStrength;

        if (transform.position.x >= rightLimit && direction == 1)
        {
            StartCoroutine(WaitAndSwitchDirection(-1));
        }
        else if (transform.position.x <= leftLimit && direction == -1)
        {
            StartCoroutine(WaitAndSwitchDirection(1));
        }
    }

    void FixedUpdate()
    {
        if (!isWaiting)
        {
            ApplyWindToNearbyObjects();
        }
    }

    IEnumerator WaitAndSwitchDirection(int newDirection)
    {
        isWaiting = true;
        direction = 0;
        yield return new WaitForSeconds(waitTime);
        direction = newDirection;
        isWaiting = false;
    }

    private void ApplyWindToNearbyObjects()
    {
        Collider[] hits = Physics.OverlapBox(transform.position, boxSize / 2f, Quaternion.identity, affectedLayers);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Cubo"))
            {
                Rigidbody rb = hit.attachedRigidbody;
                if (rb != null)
                {
                    rb.AddForce(CurrentWind, ForceMode.Acceleration);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
