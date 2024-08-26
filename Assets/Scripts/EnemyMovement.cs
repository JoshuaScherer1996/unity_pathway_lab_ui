using System.Collections;
using UnityEngine;

/*
 * I need to start the coroutine inside start and not update. As thought this starts a new coroutine every frame.
 * Clamping the position doesn't work properly and probably isn't at the right position.
 * Need to restructure the logic for my coroutine.
 * Here an example Code from GPT which in itself is faulty regarding following but gives some good ideas:
 * using System.Collections;
   using UnityEngine;
   
   public class EnemyMovement : MonoBehaviour
   {
       private GameObject _player;
       public float speed = 3.0f;
       private bool _isFollowing = false;
       private bool _isPatrolling = true;
       private Vector3 _startPosition;
       private Vector3 _targetPosition;
   
       private const float MinX = -5f;
       private const float MaxX = 5f;
       private const float MinZ = -5f;
       private const float MaxZ = 5f;
   
       private void Start()
       {
           _player = GameObject.Find("Player");
           _startPosition = transform.position;
           StartCoroutine(Patrol());
       }
   
       private void Update()
       {
           var distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
   
           // Start following the player if within 10 units range
           if (distanceToPlayer <= 10f)
           {
               _isFollowing = true;
               _isPatrolling = false; // Stop patrolling
           }
   
           if (_isFollowing)
           {
               MoveTowardsPlayer();
           }
       }
   
       private void ChooseNewPosition()
       {
           // Generate a random target position within the range around the initial position
           float newPosX = _startPosition.x + Random.Range(MinX, MaxX);
           float newPosZ = _startPosition.z + Random.Range(MinZ, MaxZ);
   
           // Set the new target position
           _targetPosition = new Vector3(newPosX, transform.position.y, newPosZ);
       }
   
       private IEnumerator Patrol()
       {
           while (_isPatrolling)
           {
               ChooseNewPosition();
   
               // Move towards the target position
               while (Vector3.Distance(transform.position, _targetPosition) > 0.1f)
               {
                   Vector3 direction = (_targetPosition - transform.position).normalized;
                   transform.position += direction * (speed * Time.deltaTime);
                   yield return null;
               }
   
               // Pause for 3 seconds before choosing the next patrol point
               yield return new WaitForSeconds(3f);
           }
       }
   
       private void MoveTowardsPlayer()
       {
           Vector3 direction = (_player.transform.position - transform.position).normalized;
           transform.position += direction * (speed * Time.deltaTime);
       }
   }
 */


public class EnemyMovement : MonoBehaviour
{
    // Declaring and initializing variables.
    private GameObject _player;
    public float speed = 3.0f;
    private bool _isFollowing = false;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    
    private const float MinX = -5f;
    private const float MaxX = 5f;
    private const float MinZ = -5f;
    private const float MaxZ = 5f;

    // Start is called before the first frame update.
    private void Start()
    {
        _player = GameObject.Find("Player");
        _startPosition = transform.position;

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
        else
        {
            // Starts the movement in a coroutine.
            StartCoroutine(GoToNewPosition());
        }
    }
    
    // Implements the logic to choose the position patrol within a given range.
    private void ChooseNewPosition()
    {
        // Choose random new float within range.
        var newPosX = Random.Range(MinX, MaxX);
        var newPosY = Random.Range(MinZ, MaxZ);
        
        // Set target position.
        _targetPosition = new Vector3(newPosX, 0f, newPosY);

        // Clamp the new position within the said range.
        _targetPosition.x = Mathf.Clamp(_startPosition.x, MinX, MaxX);
        _targetPosition.z = Mathf.Clamp(_startPosition.z, MinZ, MaxZ);

    }

    private IEnumerator GoToNewPosition()
    {
        // Generates the target position
        ChooseNewPosition();
        
        // Moves the enemy towards the target position.
        const float patrolSpeed = 1.0f;
        var direction = _targetPosition - transform.position;
        transform.position += direction * (patrolSpeed * Time.deltaTime);

        // pause for 3 seconds
        yield return new WaitForSeconds(3);
    }
    
    // Implements the logic to follow the player
    private void MoveTowardsPlayer()
    {
        // Calculate the direction towards the player
        var direction = (_player.transform.position - transform.position).normalized;

        // Move the enemy towards the player
        transform.position += direction * (speed * Time.deltaTime);
    }
}
