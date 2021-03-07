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
        empty.transform.position = new Vector3(0, 0, 0);

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
                float xScaleFactor = GameManager.canvas.pixelRect.width / level.map.width + level.prefabScale.x;
                float yScaleFactor = GameManager.canvas.pixelRect.height / level.map.height + level.prefabScale.y;

                float xCoordinate = level.prefabScale.x / 2 + GameManager.canvas.pixelRect.width / -2 + x * xScaleFactor;
                float yCoordinate = GameManager.canvas.pixelRect.height / - 2 + y * xScaleFactor;

                Vector3 position = new Vector3(xCoordinate, yCoordinate, 0);

                var spawnedPrefab = Instantiate(colorMapping.prefab, position, Quaternion.identity, empty.transform);
                spawnedPrefab.GetComponent<RectTransform>().rect.Set(xScaleFactor, yScaleFactor, xScaleFactor, yScaleFactor);

                if (colorMapping.prefab == level.weihnachtsmannPrefab)
                {
                    GameManager.SetWeihnachtsmann(spawnedPrefab);
                }

                if(colorMapping.prefab == level.sackPrefab)
                {
                    GameManager.SetSack(spawnedPrefab.GetComponent<Sack>());
                }
            }
        }
    }
}
    