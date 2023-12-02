using UnityEngine;
using System.Collections.Generic;

public class AsteroidFactory : Factory
{
    [SerializeField]
    private List<GameObject> asteroidPrefabs;

    public override GameObject Generate(Vector3 position)
    {
        int randomIndex = Random.Range(0, asteroidPrefabs.Count);
        GameObject newAsteroid = Instantiate(asteroidPrefabs[randomIndex], position, Quaternion.identity);
        newAsteroid.SetActive(false); 
        return newAsteroid;
    }
}
