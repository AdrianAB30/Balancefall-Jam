using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public BlockData[] blocks;
    public Transform spawnPoint;

    private GameObject currentBlock;

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
        int index = Random.Range(0, blocks.Length);
        BlockData data = blocks[index];

        currentBlock = Instantiate(data.blockPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody rb = currentBlock.AddComponent<Rigidbody>();
        rb.mass = data.mass;
        rb.useGravity = false; 

        var controller = currentBlock.AddComponent<BlockFallController>();
        controller.Initialize(data, () => currentBlock = null);
    }
}
