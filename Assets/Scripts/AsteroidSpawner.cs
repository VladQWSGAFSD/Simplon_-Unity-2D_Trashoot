using UnityEngine;

//public class AsteroidSpawner : MonoBehaviour
//{
//    public float spawnInterval = 0.5f;
//    public int maxAsteroids = 5;

//    private float timeSinceLastSpawn;
//    private int currentAsteroidCount;

//    private void Update()
//    {
//        UpdateMaxAsteroids();

//        if (timeSinceLastSpawn > spawnInterval && currentAsteroidCount < maxAsteroids)
//        {
//            SpawnAsteroid();
//            timeSinceLastSpawn = 0f;
//        }

//        timeSinceLastSpawn += Time.deltaTime;
//    }

//    private void SpawnAsteroid()
//    {
//        Vector3 spawnPosition = GetSpawnPositionOutsideCamera();
//        IPoolable asteroid = ObjectPoolManager.Instance.GetObject("Asteroid");
//        if (asteroid != null)
//        {
//            asteroid.Activate(spawnPosition, Quaternion.identity);
//            currentAsteroidCount++;
//        }
//    }

//    private Vector3 GetSpawnPositionOutsideCamera()
//    {
//        Camera mainCamera = Camera.main;

//        //  random point on the edge of camera's ViewportToWorldPoint
//        Vector2 viewportPoint = Random.value < 0.5f ? new Vector2(Random.value, Random.Range(0, 2) > 0.5f ? 1 : -0.1f)
//                                                    : new Vector2(Random.Range(0, 2) > 0.5f ? 1 : -0.1f, Random.value);

//        Vector3 worldPoint = mainCamera.ViewportToWorldPoint(viewportPoint);
//        worldPoint.z = 0; // 2D 

//        return worldPoint;
//    }


//    private void UpdateMaxAsteroids()
//    {
//        float timeInterval = 5f;
//        int increaseAmount = 10;
//        int maxLimit = 105;

//        if (Time.timeSinceLevelLoad / timeInterval > maxAsteroids - 5 && maxAsteroids < maxLimit)
//        {
//            maxAsteroids += increaseAmount;
//        }
//    }


//    public void OnAsteroidDeactivated()
//    {
//        currentAsteroidCount--;
//    }
//}



using TMPro;
using System.Collections.Generic;

public class AsteroidSpawner : MonoBehaviour
{
    public int minAsteroidsOnScreen = 5;
    public int maxAsteroidsOnScreen = 15;
    private List<IPoolable> activeAsteroids;


    public TMP_Text currentCountText;
    public TMP_Text asteroidsToSpawnText;




    private void Awake()
    {
        activeAsteroids = new List<IPoolable>();
    }
    private void Start()
    {
       // activeAsteroids = new List<IPoolable>();
        MaintainAsteroidCount();
    }

    private void Update()
    {
        MaintainAsteroidCount();
        UpdateUIText();
    }

    private void MaintainAsteroidCount()
    {
        int currentCount = activeAsteroids.Count;
        int asteroidsToSpawn = minAsteroidsOnScreen - currentCount;
        Debug.Log(asteroidsToSpawn);
        for (int i = 0; i < asteroidsToSpawn; i++)
        {
            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid()
    {
        if (activeAsteroids.Count < maxAsteroidsOnScreen)
        {
            IPoolable asteroid = ObjectPoolManager.Instance.GetObject("Asteroid");
            if (asteroid != null)
            {
                Vector3 spawnPosition = CalculateSpawnPosition();
                asteroid.Activate(spawnPosition, Quaternion.identity);
                activeAsteroids.Add(asteroid);
            }
        }
    }

    private Vector3 CalculateSpawnPosition()
    {
        Camera mainCamera = Camera.main;

        //  random point on the edge of camera's ViewportToWorldPoint
        Vector2 viewportPoint = Random.value < 0.5f ? new Vector2(Random.value, Random.Range(0, 2) > 0.5f ? 1 : -0.1f)
                                                    : new Vector2(Random.Range(0, 2) > 0.5f ? 1 : -0.1f, Random.value);

        Vector3 worldPoint = mainCamera.ViewportToWorldPoint(viewportPoint);
        worldPoint.z = 0; // 2D 

        return worldPoint;
    }

    public void OnAsteroidDeactivated(IPoolable asteroid)
    {
        if (activeAsteroids != null)
        {
            activeAsteroids.Remove(asteroid);
            Debug.Log("Asteroid removed. Active count: " + activeAsteroids.Count);
        }
        else
        {
            Debug.LogError("Active asteroids list is null in AsteroidSpawner");
        }
    }




    private void UpdateUIText()
    {
        int currentCount = activeAsteroids.Count;
        int asteroidsToSpawn = minAsteroidsOnScreen - currentCount;

        if (currentCountText != null)
            currentCountText.text = "Current Count: " + currentCount;

        if (asteroidsToSpawnText != null)
            asteroidsToSpawnText.text = "Asteroids to Spawn: " + asteroidsToSpawn;
    }
}
