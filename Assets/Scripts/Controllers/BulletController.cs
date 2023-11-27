using UnityEngine;

public class BulletController : MonoBehaviour, IPoolable, IDestructible
{
    [SerializeField] float bulletSpeed = 10.0f;
    [SerializeField] float lifeTime = 5.0f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Activate(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        gameObject.SetActive(true);

        // apply force to the bullet
        rb.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);

        // Deactivate after a lifeTime  time
        Invoke("Deactivate", lifeTime);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        CancelInvoke();
        rb.velocity = Vector2.zero; 
    }

    public void DestroyObject()
    {
        Deactivate(); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Debris"))
        {
            DestroyObject();

            // health diminish instead? read gdd first
            Destroy(collision.gameObject);
        }
    }

    public void SetBulletSpeed(float newSpeed)
    {
        bulletSpeed = newSpeed;
    }
}
