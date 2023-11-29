using UnityEngine;

public interface IPoolable
{
    void Activate(Vector3 position, Quaternion rotation);
    void Deactivate();
    void Init();
}
