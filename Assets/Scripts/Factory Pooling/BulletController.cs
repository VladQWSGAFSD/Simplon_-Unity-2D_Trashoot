using UnityEngine;

public class BulletController : MonoBehaviour, IDestructible
{
    public static float speed = 10f;
    [SerializeField]
    private float lifeTime = 5f;
    [SerializeField]
    private string targetTag = "Debris";
    [SerializeField] int scoreInt = 1;
    private Rigidbody2D rb2d; 

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 direction)
    {
        rb2d.AddForce(direction.normalized * speed, ForceMode2D.Impulse);
        Invoke(nameof(DeactivateObject), lifeTime); 

    }
    private void ResetBullet()
    {
        rb2d.velocity = Vector2.zero; 
        rb2d.angularVelocity = 0f; 
        transform.rotation = Quaternion.identity; 
    }
    public void DeactivateObject()
    {
        CancelInvoke(nameof(DeactivateObject)); 
        ResetBullet();
        BulletPool.Instance.Release(gameObject); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            DeactivateObject();
            //invoke deactivate in AsteroidController that will have IDestroy
            IDestructible destructible = other.GetComponent<IDestructible>();
            if (destructible != null)
            {
                destructible.DeactivateObject();
            }
            //bad to have it here, cant have it in destructible.DeactivateObject() cause asteroid gets deactivated by going outside of the view as well
            //lets not even talk about taht scoremanager is an instance but implements IScore(maybe Iscore has to be on asteroid cause it will depend on what
            // asteroid you destroy how many points you get?) 
            // so asteroid has to have IDestructible(with Deactivate() and Destroy()) and IScore that will augment if Destroy() 
            ScoreManager.Instance.IncrementScore(scoreInt);
        }
    }
    
}
