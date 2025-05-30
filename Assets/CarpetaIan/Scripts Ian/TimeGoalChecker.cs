using UnityEngine;

public class TimeGoalChecker : MonoBehaviour
{
    public float timeToWin = 30f;
    public int minimumBlocksRequired = 5;
    public string blockTag = "Bloque"; 

    private float timer = 0f;
    private bool levelEnded = false;

    public GameManager gameManager;

    void Update()
    {
        if (levelEnded) return;

        int currentBlockCount = GameObject.FindGameObjectsWithTag(blockTag).Length;

        if (currentBlockCount >= minimumBlocksRequired)
        {
            timer += Time.deltaTime;

            if (timer >= timeToWin)
            {
                levelEnded = true;
                gameManager.LevelComplete("¡Sobreviviste con estabilidad el tiempo requerido!");
            }
        }
        else
        {
            timer = 0f; 
        }
    }
}
