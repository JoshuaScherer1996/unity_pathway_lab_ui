using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Declaring and initializing variables.
    private GameObject _player;
    public float speed = 3.0f;
    private bool _isFollowing = false;

    // Start is called before the first frame update.
    private void Start()
    {
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame.
    private void Update()
    {
        // Calculates the distance between the enemy and the player.
        var distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        // Checks if the player is within 10 units range.
        if (distanceToPlayer <= 10f)
        {
            _isFollowing = true;
        }
        
        // Checks if the enemy is following the player.
        if (_isFollowing)
        {
            MoveTowardsPlayer();
        }
    }
    
    private void MoveTowardsPlayer()
    {
        // Calculate the direction towards the player
        var direction = (_player.transform.position - transform.position).normalized;

        // Move the enemy towards the player
        transform.position += direction * (speed * Time.deltaTime);
    }
}
