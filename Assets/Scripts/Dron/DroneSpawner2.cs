using UnityEngine;

public class DroneSpawner2 : MonoBehaviour
{
    public GameObject dronePrefab;
    public Transform[] pathPoints;
    public float spawnInterval = 10f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnDrone), 0f, spawnInterval);
    }

    void SpawnDrone()
    {
        GameObject drone = Instantiate(dronePrefab, pathPoints[0].position, Quaternion.identity);

        Vector3[] positions = new Vector3[pathPoints.Length];
        for (int i = 0; i < pathPoints.Length; i++)
            positions[i] = pathPoints[i].position;

        DroneMover2 mover = drone.GetComponent<DroneMover2>();
        mover.pathPositions = positions;
    }
}
