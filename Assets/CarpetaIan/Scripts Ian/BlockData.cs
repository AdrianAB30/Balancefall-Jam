using UnityEngine;

[CreateAssetMenu(fileName = "NewBlockData", menuName = "Tetris3D/Block Data")]
public class BlockData : ScriptableObject
{
    public GameObject blockPrefab;
    public AnimationCurve fallSpeedCurve;
    public float mass;
    public Color32 color;
}
