using UnityEngine;

public class CameraBoundsController : MonoBehaviour
{
    [SerializeField] GameObject spaceship;

    private void Update()
    {
        if (spaceship != null)
        {
            ConstrainWithinCameraBounds();
        }
    }

    private void ConstrainWithinCameraBounds()
    {
        var camera = Camera.main;
        var viewportPosition = camera.WorldToViewportPoint(spaceship.transform.position);
        viewportPosition.x = Mathf.Clamp01(viewportPosition.x);
        viewportPosition.y = Mathf.Clamp01(viewportPosition.y);
        spaceship.transform.position = camera.ViewportToWorldPoint(viewportPosition);
    }
}
