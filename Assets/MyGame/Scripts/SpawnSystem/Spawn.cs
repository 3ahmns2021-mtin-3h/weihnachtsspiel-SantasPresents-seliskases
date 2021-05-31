using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public static Vector2[] SpawnPoints(float xResolution, float spawnHeight)
    {
        List<Vector2> spawnPoints = new List<Vector2>();

        Vector2 leftTopCorner = new Vector2(GameManager.canvas.pixelRect.width / -2, GameManager.canvas.pixelRect.height / 2);
        Vector2 rightTopCorner = new Vector2(GameManager.canvas.pixelRect.width / 2, GameManager.canvas.pixelRect.height / 2);

        float xStep = (rightTopCorner.x - leftTopCorner.x) / (xResolution + 1);
        List<float> xCoordinates = new List<float>();
        
        for (int i = 1; i < xResolution + 1; i++)
        {
            spawnPoints.Add(new Vector2(leftTopCorner.x + (xStep * i), leftTopCorner.y + spawnHeight));
        }

        return spawnPoints.ToArray();
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector2[] spawnPoints, Transform parent)
    {
        Vector2 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject spawnedOject = Instantiate(objectToSpawn, spawnPoint, Quaternion.identity);
        spawnedOject.transform.SetParent(parent, false);

        return spawnedOject;
    }

    public static GameObject SpawnRandomObject(GameObject[] objectPool, Vector2[] spawnPoints, Transform parent)
    {
        int spawnIndex = Random.Range(0, objectPool.Length);
        Vector2 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject spawnedOject = Instantiate(objectPool[spawnIndex], spawnPoint, Quaternion.identity);
        spawnedOject.transform.SetParent(parent, false);

        return spawnedOject;
    }
}
