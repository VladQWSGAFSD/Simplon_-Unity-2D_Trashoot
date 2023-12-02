using UnityEngine;

public class AsteroidController : MonoBehaviour, IDestructible
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 50f;

    private void Start()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.angularVelocity = Random.Range(-rotationSpeed, rotationSpeed);
    }

    public void SetInitialDirection(Vector3 targetPosition)
    {
        Vector2 direction = (targetPosition - transform.position).normalized;
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (!IsInView())
        {
            DeactivateObject();
        }
    }

    private bool IsInView()
    {
        //youre already doing this with CalculateSpawnPosition() in AsteroidSpawner so why not do it there(spawer can spawn, but controller is in charge of DeactivateObject())
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.z > 0 && screenPoint.x > -0.1 && screenPoint.x < 1.1 && screenPoint.y > -0.1 && screenPoint.y < 1.1;
    }

    public void DeactivateObject()
    {
        ResetAsteroid();
        gameObject.SetActive(false);
        AsteroidPool.Instance.Release(gameObject);
    }

    private void ResetAsteroid()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.zero;
        rb2d.angularVelocity = 0;
        transform.rotation = Quaternion.identity;
    }

}
