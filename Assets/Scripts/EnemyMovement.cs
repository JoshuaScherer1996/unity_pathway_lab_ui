using System.Collections;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    // Declaring and initializing the variables and constants.
    private GameObject _player;
    public float speed = 3.0f;
    private bool _isFollowing = false;
    private bool _isPatrolling = true;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;

    private const float MinX = -25f;
    private const float MaxX = 25f;
    private const float MinZ = -25f;
    private const float MaxZ = 25f;

    // Start is called before the first frame update.
    private void Start()
    {
        // Finds all relevant components needed at the start of the game.
        _player = GameObject.Find("Player");
        _startPosition = transform.position;
        // Starts the coroutine for the patrolling logic.
        StartCoroutine(Patrol());
    }

    private void Update()
    {
        // Calculates the distance between the enemies current position and the players position.
        var distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        // Starts following the player and stops patrolling if it is within the range of 10f to the player.
        if (distanceToPlayer <= 10f)
        {
            _isFollowing = true;
            _isPatrolling = false;
        }

        // Uses the following logic implemented in the MoveTowardsPlayer method.
        if (_isFollowing)
        {
            MoveTowardsPlayer();
        }
    }

    private void ChooseNewPosition()
    {
        // Generates a random target position within the constrained field.
        var newPosX = Mathf.Clamp(_startPosition.x + Random.Range(-5f, 5f), MinX, MaxX);
        var newPosZ = Mathf.Clamp(_startPosition.z + Random.Range(-5f, 5f), MinZ, MaxZ);

        // Sets the new target position with the new x and z values.
        _targetPosition = new Vector3(newPosX, transform.position.y, newPosZ);
    }

    // Coroutine implements the patrolling logic.
    private IEnumerator Patrol()
    {
        // As long as the enemy actually patrols the logic is executed.
        while (_isPatrolling)
        {
            // Chooses a new position.
            ChooseNewPosition();

            // Moves towards the target position.
            while (Vector3.Distance(transform.position, _targetPosition) > 0.1f)
            {
                // Ensures that for a frame edge case the enemy immediately stops 
                if (_isFollowing) yield break;

                var direction = (_targetPosition - transform.position).normalized;
                transform.position += direction * (speed * Time.deltaTime);
                // Pauses the coroutine until the next frame for smooth movement.
                yield return null;
            }

            // Pauses for 3 seconds before choosing the next patrol point.
            yield return new WaitForSeconds(2f);
        }
    }

    // Implements the logic to move towards the players position.
    private void MoveTowardsPlayer()
    {
        var direction = (_player.transform.position - transform.position).normalized;
        transform.position += direction * (speed * Time.deltaTime);
    }
    
    // Destroys both the player and the enemy if they collide.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}