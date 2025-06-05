using UnityEngine;

[CreateAssetMenu(fileName = "TextosHowToPlay", menuName = "Scriptable Objects/TextosHowToPlay", order = 4)]
public class TextosHowToPlay : ScriptableObject
{
    [TextArea(5, 10)]
    public string howToPlayText;

}
