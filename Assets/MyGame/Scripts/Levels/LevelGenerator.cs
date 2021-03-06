using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static void GenerateLevel(Level level)
    {
        for(int x = 0; x < level.map.width; x++)
        {
            for(int y = 0; y < level.map.height; y++)
            {
                GenerateTile(x, y, level);
            }
        }
    }

    private static void GenerateTile(int x, int y, Level level)
    {
        Color pixelColor = level.map.GetPixel(x, y);

        if(pixelColor.a == 0)
        {
            return;
        }

        foreach(Level.ColorToPrefab colorMapping in level.colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                Vector2 position = new Vector2(x, y);
                Instantiate(colorMapping.prefab, position, Quaternion.identity);
            }
        }
    }
}
