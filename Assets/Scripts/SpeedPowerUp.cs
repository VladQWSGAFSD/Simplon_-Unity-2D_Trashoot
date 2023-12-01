using UnityEngine;
using UnityEngine.Events;

public class SpeedPowerUp : MonoBehaviour, IViagra
{
    public UnityEvent animationEvent;
    public float newBulletSpeed = 50.0f;
    //[SerializeField] Animation anim;

    public void ActivatePower()
    {
        //instead of ObjectPoolManager better use bulletspawner.cs
        //ObjectPoolManager.Instance.SetBulletSpeed(newBulletSpeed);
        animationEvent.Invoke();
        //dont like using public for speed, maybe better: BulletController.SetBulletSpeed(newBulletSpeed), why?
        BulletController.speed = newBulletSpeed;

    }

}
