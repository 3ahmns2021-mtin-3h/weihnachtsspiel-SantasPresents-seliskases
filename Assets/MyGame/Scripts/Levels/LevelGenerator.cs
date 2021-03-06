using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{

    public static void GenerateLevel(Level level)
    {
        GameObject empty = new GameObject("Level");
        empty.transform.parent = GameManager.canvas.transform;
        empty.transform.position = new Vector3(GameManager.canvas.pixelRect.width / 2, GameManager.canvas.pixelRect.height / 2, 0);

        for (int x = 0; x < level.map.width; x++)
        {
            for(int y = 0; y < level.map.height; y++)
            {
                GenerateTile(x, y, level, empty);
            }
        }
    }

private static void GenerateTile(int x, int y, Level level, GameObject empty)
    {
        Color pixelColor = level.map.GetPixel(x, y);

        if(pixelColor.a == 0)
        {
            return;
        }

        foreach (Level.ColorToPrefab colorMapping in level.colorMappings)    
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                float xScaleFactor = (GameManager.canvas.pixelRect.width / 1) / (level.map.width / 1);
                float yScalaFactor = (GameManager.canvas.pixelRect.height / 1) / (level.map.height / 1);                

                Vector2 position = new Vector2(x * xScaleFactor, y * yScalaFactor);
                var spawnedPrefab = Instantiate(colorMapping.prefab, position, Quaternion.identity, empty.transform);
                spawnedPrefab.GetComponent<RectTransform>().rect.Set(x * xScaleFactor, y * xScaleFactor, xScaleFactor, yScalaFactor);
            }
        }
    }
}
