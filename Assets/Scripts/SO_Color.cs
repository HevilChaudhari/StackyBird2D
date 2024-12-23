
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class SO_Color : ScriptableObject
{

    public Color[] ApplyColor =
    {
        Color.yellow,
        Color.green,
        Color.blue,
        new Color(1f, 0.5f, 0f),
        Color.red,
    };

}
