using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Levels")]
public class Level : ScriptableObject
{
    public bool bird;

    [Header("Level Generation")]
    public Texture2D map;
    public ColorToPrefab[] colorMappings;

    [System.Serializable]
    public struct ColorToPrefab
    {
        public string name;
        public Color color;
        public GameObject prefab;        
    }
}