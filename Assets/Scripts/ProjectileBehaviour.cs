using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    // Declaring and initializing the variables and constants.
    private const float Speed = 20.0f;
    private Vector3 _direction;

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
        if (transform.position.x < -SpawnManager.GlobalBound || transform.position.x > SpawnManager.GlobalBound ||
            transform.position.z < -SpawnManager.GlobalBound || transform.position.z > SpawnManager.GlobalBound)
        {
            Destroy(gameObject);
        }
    }

    // Destroys both projectile and enemy of they collide.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}