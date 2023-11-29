using UnityEngine;
using UnityEngine.Events;

public class SpeedPowerUp : MonoBehaviour, IViagra
{
    public UnityEvent animationEvent;
    public float newBulletSpeed = 50.0f;
    //[SerializeField] Animation anim;

    public void ActivatePower()
    {
        ObjectPoolManager.Instance.SetBulletSpeed(newBulletSpeed);
       // anim.Play("barrelAnimation");
        animationEvent.Invoke();
    }

}
