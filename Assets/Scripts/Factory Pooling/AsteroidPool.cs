using UnityEngine;
using UnityEngine.Pool;

public class AsteroidPool : MonoBehaviour
{
    [SerializeField] private AsteroidFactory asteroidFactory;
    [SerializeField] private int initialPoolSize = 10;

    public static AsteroidPool Instance { get; private set; }

    private IObjectPool<GameObject> pool;
   
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        pool = new ObjectPool<GameObject>(
            createFunc: () => asteroidFactory.Generate(Vector3.zero),
            actionOnGet: asteroid => asteroid.SetActive(true),
            actionOnRelease: asteroid => asteroid.SetActive(false),
            actionOnDestroy: asteroid => Destroy(asteroid),
            collectionCheck: false,
            defaultCapacity: initialPoolSize,
            maxSize: int.MaxValue
        );
    }

    public GameObject Get(Vector3 position)
    {
        //restarting the game cause a problem with get, problem with order of execution or re-initialization?
        //how to solve?
        GameObject asteroid = pool.Get();
        asteroid.transform.position = position;
        return asteroid;
    }

    public void Release(GameObject asteroid)
    {
        asteroid.SetActive(false);
        pool.Release(asteroid);
    }
}
