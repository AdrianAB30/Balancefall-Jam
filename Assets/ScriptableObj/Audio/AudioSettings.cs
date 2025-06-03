using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSettings", menuName = "Scriptable Objects/AudioSettings", order = 2)]
public class AudioSettings : ScriptableObject
{
    public float masterVolume = 1;
    public float musicVolume = 1;
    public float sfxVolume = 1;
}
