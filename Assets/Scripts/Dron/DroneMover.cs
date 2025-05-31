using UnityEngine;
using DG.Tweening;

public class DroneMover : MonoBehaviour
{
    public Vector3[] pathPositions;
    public float moveDuration = 5f;
    public float zRotation = 30f;
    public float rotationSpeed = 0.3f;
    public Ease myEase;

    void Start()
    {
        transform.DOPath(pathPositions, moveDuration, PathType.CatmullRom)
            .SetEase(myEase)
            .SetLookAt(0.05f)
            .OnComplete(() => Destroy(gameObject));

        transform.DORotate(new Vector3(0, 0, zRotation), rotationSpeed, RotateMode.LocalAxisAdd)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
