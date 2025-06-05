using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubeMenu : MonoBehaviour
{
    public GameObject cubePrefab;
    public Transform spawnPoint;
    public float spawnInterval = 1f;
    public int numberOfCubesToSpawn = 27;
    public GameObject spawnCube;

    private List<GameObject> spawnedCubes = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnCubesRoutine());
    }

    IEnumerator SpawnCubesRoutine()
    {
        yield return new WaitForSeconds(6f);
        Vector3 currentSpawnOffset = Vector3.zero;

        for (int i = 0; i < numberOfCubesToSpawn; i++)
        {
            Vector3 spawnPosition = spawnPoint.position + currentSpawnOffset;
            GameObject newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);

            newCube.transform.SetParent(spawnPoint.transform,true);

            float cubeSize = newCube.transform.localScale.x;

            int x = i % 3;
            int y = (i / 9);
            int z = (i / 3) % 3;

            spawnPosition = spawnPoint.position + new Vector3(
                (x - 1) * cubeSize,
                y * cubeSize,
                (z - 1) * cubeSize
            );
            newCube.transform.position = spawnPosition;

            spawnedCubes.Add(newCube);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
