using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    public GameObject bulletPrefab;
    private Queue<IPoolable> bulletPool = new Queue<IPoolable>();
    public int poolSize = 20;

    private void Awake()
    {
        Instance = this;
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            IPoolable poolable = obj.GetComponent<IPoolable>();
            poolable.Deactivate();
            bulletPool.Enqueue(poolable);
        }
    }

    public IPoolable GetBullet()
    {
        if (bulletPool.Count > 0)
        {
            return bulletPool.Dequeue();
        }
        else
        {
            // optionally create a new bullet if the pool is empty?
            GameObject obj = Instantiate(bulletPrefab);
            return obj.GetComponent<IPoolable>();
            //Debug.Log("new one , pool empty");
        }
    }

    public void ReturnBullet(IPoolable bullet)
    {
       // bullet.Deactivate();
        bulletPool.Enqueue(bullet);
       // Debug.Log("return to pool");

    }

    //outside of ObjectPoolManager?
    public void SetBulletSpeed(float newSpeed)
    {
        foreach (var bullet in bulletPool)
        {
            if (bullet is BulletController bulletController)
            {
                bulletController.SetBulletSpeed(newSpeed);
            }
        }
    }
}
