using UnityEngine;

public class SpaceshipController : MonoBehaviour,IMovable,IDestructible
{
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float rotateSpeed = 180f;
    [SerializeField] Transform barrelEnd;
    [SerializeField] Pool bulletPool;
    private IGameOver gameStateManager;

    public void Initialize(IGameOver gameStateManager)
    {
        this.gameStateManager = gameStateManager;
    }

    private void Update()
    {
        Move();

        // Shoot
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    #region Ship's Reaction
    public void DeactivateObject()
    {
        // Animation, reset level, reset score? Trigger an event? 
        gameObject.SetActive(false);
        //for testing for now moveed out to collison directly
//        GameStateManager.Instance.TriggerGameOver();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);

    }
    public void HandleCollision(GameObject collisionObject)
    {
        if (collisionObject.gameObject.CompareTag("Debris"))
        {
            //before DeactivateObject() get IDestructuble and see how much damage can be done to the ship instead
            DeactivateObject();
            GameStateManager.Instance.TriggerGameOver();

            //Debug.Log("Destroyed.");
        }

        // retrieve the IViagra component from the gameObject of the collision, bad it might be, think what better then GetComponent?
        IViagra powerUp = collisionObject.gameObject.GetComponent<IViagra>();
        if (powerUp != null)
        {
            powerUp.ActivatePower();
            Destroy(collisionObject.gameObject);

        }
    }
    #endregion

    #region Ship's Action
    private void Shoot()

    {
        if (barrelEnd != null)
        {
            //IPoolable bullet = ObjectPoolManager.Instance.GetObject("Bullet");
            //bullet.Activate(barrelEnd.position, barrelEnd.rotation);
            //bullets.Add(bulletPool.Get(barrelEnd.position));
            Vector2 direction = barrelEnd.up; // Use the barrel's 'up' direction for 2D space
            BulletPool.Instance.Get(barrelEnd.position, direction);
        }
    }
    public void Move()
    {
        // Movement
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            MoveUp();
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            MoveDown();
        }

        // Rotation
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RotateRight();
        }
    }

    public void MoveUp()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }
    public void MoveDown()
    {
        
        //transform.Translate(Vector2.up * -moveSpeed * Time.deltaTime);
    }
    public void RotateLeft()
    {
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }
    public void RotateRight()
    {
        transform.Rotate(Vector3.forward, -rotateSpeed * Time.deltaTime);
    }
    #endregion
}
