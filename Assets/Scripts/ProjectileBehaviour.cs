using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private const float Speed = 20.0f;
    private Vector3 _direction;

    private const float MinX = -25f;
    private const float MaxX = 25f;
    private const float MinZ = -25f;
    private const float MaxZ = 25f;

    // Sets the direction of the projectile.
    public void SetDirection(Vector3 direction)
    {
        // Normalizes the direction.
        _direction = direction.normalized; 
    }

    // Update is called once per frame.
    private void Update()
    {
        // Moves the projectile in the set direction.
        transform.position += _direction * (Speed * Time.deltaTime);

        // Destroys the projectile if it goes out of bounds.
        if (transform.position.x < MinX || transform.position.x > MaxX || 
            transform.position.z < MinZ || transform.position.z > MaxZ)
        {
            Destroy(gameObject);
        }
    }
}
