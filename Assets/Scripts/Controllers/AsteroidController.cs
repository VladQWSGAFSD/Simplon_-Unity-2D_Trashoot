using UnityEngine;

public class AsteroidController : MonoBehaviour, IPoolable
{
    public float moveSpeed = 1.0f;
    private Rigidbody2D rb;
    private AsteroidSpawner asteroidSpawner;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (asteroidSpawner == null)
        {
            asteroidSpawner = FindObjectOfType<AsteroidSpawner>();
        }
    }

    public void Activate(Vector3 position, Quaternion rotation)
    {
        gameObject.SetActive(true);
        transform.position = position;
        transform.rotation = rotation;

        SetInitialMovement();
    }

    private void SetInitialMovement()
    {
        //  towards player 
        var player = FindObjectOfType<SpaceshipController>();
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        rb.velocity = Vector2.zero;
        // ObjectPoolManager.Instance.ReturnObject("Asteroid", this);
        //bad,real bad? 
        if (asteroidSpawner != null)
        {
            asteroidSpawner.OnAsteroidDeactivated(this);
        }
        else
        {
            Debug.LogWarning("AsteroidSpawner reference is null in AsteroidController");
        }

    }

    void Update()
    {
        if (IsOutOfCameraBounds())
        {
            //Debug.Log("out of bounds");
           // Deactivate();
        }
    }
    //cant have this if i dont want to spawn ouside of the edge,say -1f, cause it will deactiavte immediatly. how do? implement coroutine(dont like)?
    private bool IsOutOfCameraBounds()
    {
        //coroutine?>
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        return viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1;
    }

    public void Init()
    {
        gameObject.SetActive(false);
        rb.velocity = Vector2.zero;
    }
}
