using UnityEngine;
using UnityEngine.UI;

public class BlockButtonUI : MonoBehaviour
{
    public Image icon;
    private BlockData data;
    private System.Action<BlockData, GameObject> onClickCallback;

    public void Initialize(BlockData data, System.Action<BlockData, GameObject> callback)
    {
        this.data = data;
        onClickCallback = callback;
        icon.sprite = data.blockIcon; // Icono del bloque
        GetComponent<Button>().onClick.AddListener(() => onClickCallback?.Invoke(data, gameObject));
    }
}
