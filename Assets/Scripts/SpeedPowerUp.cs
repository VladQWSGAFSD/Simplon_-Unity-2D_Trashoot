using UnityEngine;

public class SpeedPowerUp : MonoBehaviour, IViagra
{
    public float newBulletSpeed = 50.0f;

    public void ActivatePower()
    {
        ObjectPoolManager.Instance.SetBulletSpeed(newBulletSpeed);
    }

}
