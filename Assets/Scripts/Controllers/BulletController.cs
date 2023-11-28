using UnityEngine;

public class BulletController : MonoBehaviour, IPoolable, IDestructible
{
    public float bulletSpeed = 10.0f;
    [SerializeField] float lifeTime = 1.0f;
    //bad idea, this should be on debris cause the value might depend on what debris it is not on what bullet it is?
    [SerializeField] int scoreInt = 1;
    private Rigidbody2D rb;
    private bool hasCollided = false;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Activate(Vector3 position, Quaternion rotation)
    {
        hasCollided = false;

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
        ObjectPoolManager.Instance.ReturnBullet(this);

    }

    public void DestroyObject()
    {
        Deactivate(); 

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCollided && collision.gameObject.CompareTag("Debris"))
        {
            //Debug.Log("collsion with debris");
            hasCollided = true;

            DestroyObject();
           // health diminish instead? read gdd first
            Destroy(collision.gameObject);

            ScoreManager.Instance.IncrementScore(scoreInt);

        }
    }

    public void SetBulletSpeed(float newSpeed)
    {
        bulletSpeed = newSpeed;
    }
}
