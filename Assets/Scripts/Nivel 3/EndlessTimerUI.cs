using UnityEngine;
using TMPro;

public class EndlessTimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timerText.text = $"Tiempo: {minutes:00}:{seconds:00}";
    }
}
