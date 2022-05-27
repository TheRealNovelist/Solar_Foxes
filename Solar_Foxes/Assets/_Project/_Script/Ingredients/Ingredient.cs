using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public enum Direction
{
    North,
    South,
    West,
    East
}

[System.Serializable]
public class Movement
{
    public Direction direction = Direction.North;
    public int moves = 1;
}

[CreateAssetMenu(menuName = "Scriptable Object/Ingredient", fileName = "New Ingredient")]
public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public Sprite sprite;

    public List<Movement> movementQueue;
    
#if UNITY_EDITOR
    void OnValidate() {
        string assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
        ingredientName = Path.GetFileNameWithoutExtension(assetPath);
    }
#endif
}
