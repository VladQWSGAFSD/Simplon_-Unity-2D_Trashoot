using UnityEngine;

public class SpaceshipController : MonoBehaviour,IMovable,IDestructible
{
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float rotateSpeed = 180f;
    [SerializeField] Transform barrelEnd; 


    private void Update()
    {
        Move();


        // Shoot
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (barrelEnd != null)
        {
            IPoolable bullet = ObjectPoolManager.Instance.GetObject("Bullet");
            bullet.Activate(barrelEnd.position, barrelEnd.rotation);
        }
    }
    public void Move()
    {
        // Movement
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector2.up * -moveSpeed * Time.deltaTime);
        }

        // Rotation
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.forward, -rotateSpeed * Time.deltaTime);
        }
    }

    public void DestroyObject()
    {
        // Animation, reset level, reset score? Trigger and event? 
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Debris"))
        {
            DestroyObject();
            //Debug.Log("Destroyed.");
            GameStateManager.Instance.TriggerGameOver();

        }

        // retrieve the IViagra component from the gameObject of the collision, bad it might be, think what better then GetComponent?
        IViagra powerUp = collision.gameObject.GetComponent<IViagra>();
        if (powerUp != null)
        {
            powerUp.ActivatePower();
            Destroy(collision.gameObject);

        }
    }
}
