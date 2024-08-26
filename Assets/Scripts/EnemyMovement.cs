using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject _player;
    public float speed = 3.0f;

    private void Start()
    {
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        // Calculate the distance between the enemy and the player
        var distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        // Check if the player is within 10 units range
        if (distanceToPlayer <= 10f)
        {
            // Move towards the player's position
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
