using System;
using System.Collections;
using UnityEngine;

public class BlockSpawnernivel1 : MonoBehaviour
{
    public BlockData[] blocks;
    public Transform spawnPoint;
    private GameObject currentBlock;
    public UILevel1 uiLvl1;

    [Header("Rangos de generación aleatoria")]
    [SerializeField] private int minBlocksToSpawn = 10;
    [SerializeField] private int maxBlocksToSpawnRange = 30;

    [SerializeField] private int minBlocksRequiredRangeMin = 10;
    [SerializeField] private int minBlocksRequiredRangeMax = 20;

    public static int MinBlocksRequired { get; private set; }

    private int maxBlocksToSpawn;
    private int spawnedCount = 0;
    private bool spawningEnabled = true;

    private string blockTag = "Cubo";

    public static event Action<Color32> OnBlockSpawned;

    void Start()
    {
        MinBlocksRequired = UnityEngine.Random.Range(minBlocksRequiredRangeMin, minBlocksRequiredRangeMax + 1);
        maxBlocksToSpawn = UnityEngine.Random.Range(MinBlocksRequired + 1, maxBlocksToSpawnRange + 1);

        Debug.Log($"🧱 Máx bloques para esta partida: {maxBlocksToSpawn}");
        Debug.Log($"🔒 Mínimo requerido en escena para ganar: {MinBlocksRequired}");

        uiLvl1.Inicializar(maxBlocksToSpawn, MinBlocksRequired);
        spawningEnabled = false;
        StartCoroutine(DelaySpawner());
    }

    void Update()
    {
        if (!spawningEnabled) return;

        if (currentBlock == null)
        {
            TrySpawn();
        }
    }

    void TrySpawn()
    {
        if (spawnedCount >= maxBlocksToSpawn)
        {
            Debug.Log("Límite de bloques alcanzado.");
            return;
        }

        int currentInScene = GameObject.FindGameObjectsWithTag(blockTag).Length;

        SpawnRandomBlock();
    }

    void SpawnRandomBlock()
    {
        int index = UnityEngine.Random.Range(0, blocks.Length);
        BlockData data = blocks[index];

        currentBlock = Instantiate(data.blockPrefab, spawnPoint.position, Quaternion.identity);
        currentBlock.tag = blockTag;
        currentBlock.layer = LayerMask.NameToLayer("Cubo");

        Rigidbody rb = currentBlock.AddComponent<Rigidbody>();
        rb.mass = data.mass;
        rb.useGravity = false;

        var controller = currentBlock.AddComponent<BlockFallController>();
        controller.Initialize(data, () => currentBlock = null);

        spawnedCount++;
        OnBlockSpawned?.Invoke(data.color);

        uiLvl1.ActualizarBloquesRestantes(maxBlocksToSpawn - spawnedCount);

        Debug.Log($"Bloque {spawnedCount}/{maxBlocksToSpawn} instanciado.");
    }
    public void DetenerSpawning()
    {
        spawningEnabled = false;
        Debug.Log("🛑 Se ha detenido el spawner.");
    }
    IEnumerator DelaySpawner()
    {
        yield return new WaitForSeconds(5f);
        spawningEnabled = true;
        SpawnRandomBlock();
    }
}
