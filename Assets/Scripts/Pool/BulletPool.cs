using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : Pool
{
    [SerializeField] private BulletFactory bulletFactory;
    [SerializeField] private int initialPoolSize = 10;

    public static BulletPool Instance { get; private set; }

    private IObjectPool<GameObject> pool;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        pool = new ObjectPool<GameObject>(
            createFunc: () => bulletFactory.Generate(Vector3.zero),
            actionOnGet: bullet => bullet.SetActive(true),
            actionOnRelease: bullet => bullet.SetActive(false),
            actionOnDestroy: bullet => Destroy(bullet),
            collectionCheck: false,
            defaultCapacity: initialPoolSize,//assign place in memeory for initialPoolSize instaod of dynamic,save space
            maxSize: int.MaxValue//limit in case of errors,too many
        );
    }

    public override GameObject Get(Vector3 position, Vector2 direction)
    {
        GameObject bullet = pool.Get();
        bullet.transform.position = position;
        bullet.transform.rotation = Quaternion.identity; 
        bullet.GetComponent<BulletController>().Initialize(direction);
        return bullet;
    }

    public override void Release(GameObject bullet)
    {
        bullet.SetActive(false);
        pool.Release(bullet);
    }
}
