using UnityEngine;
using System;
using System.Collections;

public class BlockSpawner : MonoBehaviour
{
    public BlockData[] blocks;
    public Transform spawnPoint;
    public Transform root;
    private GameObject currentBlock;
    private bool spawningEnabled = true;

    public static event Action<Color32> OnBlockSpawned;


    void Start()
    {
        SpawnRandomBlock();
    }

    void Update()
    {
        if (currentBlock == null)
        {
            SpawnRandomBlock();
        }
    }

    void SpawnRandomBlock()
    {
        int index = UnityEngine.Random.Range(0, blocks.Length);
        BlockData data = blocks[index];

        currentBlock = Instantiate(data.blockPrefab, spawnPoint.position, Quaternion.identity);

        currentBlock.tag = "Cubo";

        currentBlock.layer = LayerMask.NameToLayer("Cubo");

        currentBlock.transform.parent = root.transform;

        Rigidbody rb = currentBlock.AddComponent<Rigidbody>();
        rb.mass = data.mass;
        rb.useGravity = false; 

        var controller = currentBlock.AddComponent<BlockFallController>();
        controller.Initialize(data, () => currentBlock = null);

        OnBlockSpawned?.Invoke(data.color);
    }
    IEnumerator DelaySpawner()
    {
        yield return new WaitForSeconds(5f);
        spawningEnabled = true;
        SpawnRandomBlock();
    }
}
