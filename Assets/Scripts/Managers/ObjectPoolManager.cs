using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public List<GameObject> prefabs; 
        public int size;
    }

    public List<Pool> pools;
    private Dictionary<string, Queue<IPoolable>> poolDictionary;

    private void Awake()
    {
        Instance = this;
        InitializePools();
    }

      private void InitializePools()
    {
        poolDictionary = new Dictionary<string, Queue<IPoolable>>();

        foreach (var pool in pools)
        {
            Queue<IPoolable> objectPool = new Queue<IPoolable>();

            for (int i = 0; i < pool.size; i++)
            {
                // choose a prefab randomly from the list cause easy(in what cases would you(and how) use something specific other than random?)
                GameObject prefab = pool.prefabs[Random.Range(0, pool.prefabs.Count)];
                GameObject obj = Instantiate(prefab);
                IPoolable poolable = obj.GetComponent<IPoolable>();
                poolable.Init();
                objectPool.Enqueue(poolable);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public IPoolable GetObject(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        if (poolDictionary[tag].Count > 0)
        {
            return poolDictionary[tag].Dequeue();
        }
        else
        {
            // extend the pool 
            //return ExtendPool(tag);
            return null;    
        }
    }

    private IPoolable ExtendPool(string tag)
    {
        Pool pool = pools.Find(p => p.tag == tag);
        if (pool == null)
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        // randomly from the list of in what case(and how to implement) would i ne d this not to be random??
        GameObject prefab = pool.prefabs[Random.Range(0, pool.prefabs.Count)];
        GameObject obj = Instantiate(prefab);
        IPoolable poolable = obj.GetComponent<IPoolable>();
        poolable.Init();

        // limit the maximum size of the pool
        // if (poolDictionary[tag].Count < maxPoolSize) {  }

        // ad the created object to the pool
        poolDictionary[tag].Enqueue(poolable);

        return poolable;
    }



    public void ReturnObject(string tag, IPoolable objectToReturn)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return;
        }

        objectToReturn.Deactivate();
        poolDictionary[tag].Enqueue(objectToReturn);
    }

    //outside of ObjectPoolManager?
    public void SetBulletSpeed(float newSpeed)
    {
        if (!poolDictionary.ContainsKey("Bullet"))
        {
            Debug.LogWarning("Bullet pool doesn't exist.");
            return;
        }

        foreach (var poolable in poolDictionary["Bullet"])
        {
            if (poolable is BulletController bulletController)
            {
                bulletController.SetBulletSpeed(newSpeed);
            }
        }
    }
}
