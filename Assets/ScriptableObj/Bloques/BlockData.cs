using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBlockData", menuName = "Scriptable Objects/Block Data", order = 1)]
public class BlockData : ScriptableObject
{
    public GameObject blockPrefab;
    public AnimationCurve fallSpeedCurve;
    public float mass;
    public Color32 color;
}
