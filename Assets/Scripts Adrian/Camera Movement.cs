using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> pointsCamera;
    [SerializeField] private Transform lookTarget;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    private int currentIndex = 0;
    private bool moving = false;

    void Update()
    {
        SwitchCamera();
    }
    private void SwitchCamera()
    {
        if (!moving)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                currentIndex = (currentIndex + 1) % pointsCamera.Count;
                StartCoroutine(MoveToPoint(pointsCamera[currentIndex]));
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                currentIndex = (currentIndex - 1 + pointsCamera.Count) % pointsCamera.Count;
                StartCoroutine(MoveToPoint(pointsCamera[currentIndex]));
            }
        }

        Vector3 direction = lookTarget.position - transform.position;
        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator MoveToPoint(Transform targetPoint)
    {
        moving = true;
        while (Vector3.Distance(transform.position, targetPoint.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPoint.position;
        moving = false;
    }
}