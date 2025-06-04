using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubeMenu : MonoBehaviour
{
    public GameObject cubePrefab;      
    public Transform spawnPoint;       
    public float spawnInterval = 1f;   
    public int numberOfCubesToSpawn = 27; 
    private int cubesSpawned = 0;

    private Rigidbody myRBD;

    void Start()
    {
        StartCoroutine(SpawnCubesRoutine());
    }

    IEnumerator SpawnCubesRoutine()
    {
        Vector3 currentSpawnOffset = Vector3.zero; 

        for (int i = 0; i < numberOfCubesToSpawn; i++)
        {

            Vector3 spawnPosition = spawnPoint.position + currentSpawnOffset;
            GameObject newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);

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


            cubesSpawned++;
            Debug.Log($"Spawned cube {cubesSpawned}");

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
