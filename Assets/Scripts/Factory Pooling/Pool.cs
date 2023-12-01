using UnityEngine;

public abstract class Pool : MonoBehaviour
{
    public abstract GameObject Get(Vector3 position, Vector2 direction);
    public abstract void Release(GameObject gameObject);
}
