using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float xResolution;
    [Header("Bird")]
    public GameObject bird;
    public float birdSpawnTime;
    public bool useBird;
    [Header("Collectables")]
    public GameObject[] collectables;
    public float spawnMinTime;
    public float spawnMaxTime;
    public float spawnHeight;

    private float timeUntilSpawn;
    private float time;
    private bool birdSpawned = false;
    private GameObject empty;
    private Vector2[] spawnPoints;

    private void Awake()
    {
        spawnPoints = Spawn.SpawnPoints(xResolution, spawnHeight);

        empty = new GameObject("Collectables");
        empty.transform.parent = GameManager.canvas.transform;
        empty.transform.position = new Vector3(GameManager.canvas.pixelRect.width / 2, GameManager.canvas.pixelRect.height / 2, 0);
    }

    private void Start()
    {
        SpawnCollectable();
    }

    private void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn < 0)
        {
            SpawnCollectable();            
        }

        if (!useBird || birdSpawned)
        {
            return;
        }

        time += Time.deltaTime;

        if (time > birdSpawnTime && GameManager.numPresentsStored != 0)
        {
            birdSpawned = true;
            GameManager.currentBird = Spawn.SpawnObject(bird, spawnPoints, GameManager.canvas.transform);
        }
    }

    private void SpawnCollectable()
    {
        Spawn.SpawnRandomObject(collectables, spawnPoints, empty.transform);
        timeUntilSpawn = Random.Range(spawnMinTime, spawnMaxTime);
    }
}
    