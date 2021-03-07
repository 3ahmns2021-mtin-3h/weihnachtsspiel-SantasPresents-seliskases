using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Levels")]
public class Level : ScriptableObject
{
    public bool bird;
    public Vector2 prefabScale = new Vector2(100, 100);
    [Space]
    public GameObject icePrefab;
    public GameObject weihnachtsmannPrefab;
    public GameObject sackPrefab;

    [Header("Level Generation")]
    public Texture2D map;
    public ColorToPrefab[] colorMappings;

    [System.Serializable]
    public struct ColorToPrefab
    {
        public Color color;
        [HideInInspector]
        public GameObject prefab;

        public enum Type
        {
            Ice,
            Weihnachtsmann,
            Sack
        }
        [SerializeReference]
        public Type type;
    }

    [HideInInspector]
    public int highScore;
}