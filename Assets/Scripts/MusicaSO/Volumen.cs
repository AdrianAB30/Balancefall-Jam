using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Volumendeaudio/Volumen", fileName = "Volumen")]

public class Volumen : ScriptableObject
{
    [SerializeField] public float Master;
    [SerializeField] public float Musica;
    [SerializeField] public float SFX;

}