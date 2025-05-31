using System.Collections.Generic;
using UnityEngine;

public class SpawnFarolas : MonoBehaviour
{
    public GameObject[] farolaPrefabs;
    public float spawnInterval = 2f;
    public float minX = -15f;
    public float maxX = 17f;
    public float yPos = 0f;
    public float zPos = 0f;
    public float fallSpeed = 2f;

    private float timer;
    private int lastPrefabIndex = -1;
    private float lastX = float.MaxValue;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnFarola();
            timer = spawnInterval;
        }
    }

    void SpawnFarola()
    {
        // Elegir un prefab distinto al anterior
        int prefabIndex;
        do
        {
            prefabIndex = Random.Range(0, farolaPrefabs.Length);
        } while (prefabIndex == lastPrefabIndex);
        lastPrefabIndex = prefabIndex;

        // Elegir una posición X distinta a la anterior (mínimo diferencia de 2)
        float randomX;
        do
        {
            randomX = Random.Range(minX, maxX);
        } while (Mathf.Abs(randomX - lastX) < 2f);
        lastX = randomX;

        // Instanciar farola
        Vector3 spawnPosition = new Vector3(randomX, yPos, zPos);
        GameObject farola = Instantiate(farolaPrefabs[prefabIndex], spawnPosition, Quaternion.identity);

        // Añadir movimiento
        FarolaMovimiento movimiento = farola.AddComponent<FarolaMovimiento>();
        movimiento.fallSpeed = fallSpeed;
    }
}