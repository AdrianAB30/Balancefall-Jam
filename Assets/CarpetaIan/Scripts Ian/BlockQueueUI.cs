using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockQueueUI : MonoBehaviour
{
    public GameObject buttonPrefab; 
    public Transform contentPanel;
    public List<BlockData> allBlocks;
    public Transform spawnPoint;
    public float interval = 10f;

    private bool hasActiveBlock = false;

    void Start()
    {
        StartCoroutine(AddBlockRoutine());
    }

    IEnumerator AddBlockRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            AddNewBlock();
        }
    }

    void AddNewBlock()
    {
        var random = allBlocks[Random.Range(0, allBlocks.Count)];
        GameObject newButton = Instantiate(buttonPrefab, contentPanel);
        newButton.GetComponent<BlockButtonUI>().Initialize(random, OnBlockSelected);
    }

    void OnBlockSelected(BlockData data, GameObject buttonGO)
    {
        if (hasActiveBlock) return;

        GameObject block = Instantiate(data.blockPrefab, spawnPoint.position, Quaternion.identity);
        var controller = block.GetComponent<BlockFallController>();
        controller.Initialize(data, () => hasActiveBlock = false);

        hasActiveBlock = true;
        Destroy(buttonGO);
    }
}
