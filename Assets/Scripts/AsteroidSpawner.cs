using UnityEngine;
using System.Collections.Generic;

public class AsteroidSpawner : MonoBehaviour
{
	[SerializeField] private int minAsteroidsOnScreen = 5;
	[SerializeField] private int maxAsteroidsOnScreen = 15;
	[SerializeField] private float timeToAddAsteroid = 5f;
	[SerializeField] private GameObject spaceship;

	private List<GameObject> activeAsteroids = new List<GameObject>();
	private float timeSinceLastIncrease;

	private void Start()
	{
		timeSinceLastIncrease = 0f;
		SpawnInitialAsteroids();
	}

	private void Update()
	{
		timeSinceLastIncrease += Time.deltaTime;
		if (timeSinceLastIncrease >= timeToAddAsteroid)
		{
			timeSinceLastIncrease = 0f;
			if (minAsteroidsOnScreen < maxAsteroidsOnScreen)
			{
				minAsteroidsOnScreen++;
			}
		}

		MaintainAsteroidCount();
	}

	private void SpawnInitialAsteroids()
	{
		for (int i = 0; i < minAsteroidsOnScreen; i++)
		{
			SpawnAsteroid();
		}
	}

	private void MaintainAsteroidCount()
	{
		activeAsteroids.RemoveAll(item => item == null || !item.activeInHierarchy);

		while (activeAsteroids.Count < minAsteroidsOnScreen)
		{
			SpawnAsteroid();
		}
	}

	private void SpawnAsteroid()
	{
		Vector3 spawnPosition = CalculateSpawnPosition();
		GameObject asteroid = AsteroidPool.Instance.Get(spawnPosition);
		activeAsteroids.Add(asteroid);

		AsteroidController asteroidController = asteroid.GetComponent<AsteroidController>();
		if (spaceship != null && asteroidController != null)
		{
			asteroidController.SetInitialDirection(spaceship.transform.position);
		}
	}
	private Vector3 CalculateSpawnPosition()
	{
		Camera mainCamera = Camera.main;
		Vector2 viewportPoint = Random.value < 0.5f ? new Vector2(Random.value, Random.Range(0, 2) > 0.5f ? 1 : -0.1f)
													: new Vector2(Random.Range(0, 2) > 0.5f ? 1 : -0.1f, Random.value);

		Vector3 worldPoint = mainCamera.ViewportToWorldPoint(viewportPoint);
		worldPoint.z = 0;
		return worldPoint;
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(10, 150, 200, 30), $"Current count: { activeAsteroids.Count }");
		GUI.Label(new Rect(10, 180, 200, 30), $"Asteroids To Spawn: { minAsteroidsOnScreen - activeAsteroids.Count}");
	}

}

