﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    public GameObject present;
    public GameObject rock;
    public float xResolution;
    public float spawnMinTime;
    public float spawnMaxTime;
    public float spawnHeight;

    private float nextSpawnTime;
    private GameObject empty;
    private List<Vector2> spawnPoints = new List<Vector2>();

    private void Awake()
    {
        Vector2 leftTopCorner = new Vector2(GameManager.canvas.pixelRect.width / -2, GameManager.canvas.pixelRect.height/2);
        Vector2 rightTopCorner = new Vector2(GameManager.canvas.pixelRect.width / 2, GameManager.canvas.pixelRect.height/2);

        float xSummand = (rightTopCorner.x - leftTopCorner.x) / (xResolution + 1);
        List<float> xCoordinates = new List<float>();

        for (int i = 1; i <= xResolution; i++)
        {
            spawnPoints.Add(new Vector2(leftTopCorner.x + (xSummand * i), leftTopCorner.y + spawnHeight));
        }

        empty = new GameObject("Collectables");
        empty.transform.parent = GameManager.canvas.transform;
        empty.transform.position = new Vector3(GameManager.canvas.pixelRect.width / 2, GameManager.canvas.pixelRect.height / 2, 0);

        SetSpawnTime();
    }

    private void Update()
    {        
        nextSpawnTime -= Time.deltaTime;

        if(nextSpawnTime <= 0)
        {
            Spawn();
            SetSpawnTime();
        }
    }

    private void SetSpawnTime()
    {
        nextSpawnTime = Random.Range(spawnMinTime, spawnMaxTime);
    }

    private void Spawn()
    {
        Vector2 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        int spawnIndex = Random.Range(1, 3);

        switch (spawnIndex)
        {
            case 1:
                GameObject spawnedPresent = Instantiate(present, spawnPoint, Quaternion.identity);
                spawnedPresent.transform.SetParent(empty.transform, false);
                break;
            case 2:
                GameObject spawnedRock = Instantiate(rock, spawnPoint, Quaternion.identity);
                spawnedRock.transform.SetParent(empty.transform, false);
                break;
        }
    }
}
    