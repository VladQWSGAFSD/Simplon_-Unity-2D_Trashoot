using UnityEngine;

public abstract class Factory : MonoBehaviour
{
    public abstract GameObject Generate(Vector3 position);
}
