using UnityEngine;

public class BulletFactory : Factory
{
    [SerializeField]
    private GameObject bulletPrefab; 

    public override GameObject Generate(Vector3 position)
    {
        GameObject newBullet = Instantiate(bulletPrefab, position, Quaternion.identity);
        newBullet.SetActive(false); 
        return newBullet;
    }
}
