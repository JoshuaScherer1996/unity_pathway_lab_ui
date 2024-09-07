using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Declaring and initializing the variables and constants.
    private const float Speed = 5.0f;
    private const float JumpForce = 10.0f;
    private const float GravityModifier = 2.0f;
    private const float MouseSensitivity = 600.0f;

    private float _horizontalInput;
    private float _verticalInput;
    private float _rotationY;
    private bool _isOnGround;
    private Rigidbody _playerRb;

    public GameObject projectile;
    private const float SpawnDistance = 2.0f;
    
    public GameObject teleportProjectile;
    private Vector3 _targetTeleportPosition;
    private bool _shouldTeleport;
    private const float TeleportSpeed = 200.0f;

    // Start is called before the first frame update.
    private void Start()
    {
        // Gets the players Rigidbody component.
        _playerRb = GetComponent<Rigidbody>();

        // Applies the GravityModifier to the games general physics.
        Physics.gravity *= GravityModifier;
    }

    // Update is called once per frame.
    private void Update()
    {
        // Getting the Player Input from the horizontal and vertical Axis.
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        // Captures mouse movement for camera rotation.
        var mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        _rotationY += mouseX;

        // Rotates the player around the Y axis based on the mouse movement.
        transform.localRotation = Quaternion.Euler(0, _rotationY, 0);

        // Executes the jump method when space is pressed and the player is on the ground.
        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround)
        {
            Jump();
        }

        // Instantiates a projectile when the left mouse button is triggered.
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootProjectile(false);
        }

        // Instantiates a teleport projectile when the left mouse button is triggered.
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ShootProjectile(true);
            // ToDo: update the player position based on the collisions position.
        }
        
        // Handles the teleport movement
        if (_shouldTeleport)
        {
            MoveTowardsTeleportPosition();
        }
    }

    // Called at a fixed interval and is preferred for physics-based updates.
    private void FixedUpdate()
    {
        // Calculates movement direction relative to the player's orientation.
        var movement = transform.forward * _verticalInput + transform.right * _horizontalInput;

        // Calculates new position.
        var newPosition = transform.position + movement * (Speed * Time.fixedDeltaTime);

        // Keeps the newPosition values inside the set min and max bounds.
        newPosition.x = Mathf.Clamp(newPosition.x, -SpawnManager.GlobalBound, SpawnManager.GlobalBound);
        newPosition.z = Mathf.Clamp(newPosition.z, -SpawnManager.GlobalBound, SpawnManager.GlobalBound);

        // Moves the player around smoothly within the boundaries.
        _playerRb.MovePosition(newPosition);
    }

    // Method that executes the jump logic.
    private void Jump()
    {
        _playerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        _isOnGround = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        // Checks if the player is currently on the ground.
        if (other.gameObject.CompareTag("Ground"))
        {
            _isOnGround = true;
        }
    }

    // Implements the shooting logic.
    private void ShootProjectile(bool isTeleportProjectile)
    {
        // Gets the direction of the cinemachine camera.
        var cameraTransform = Camera.main.transform;
        var spawnPosition = transform.position + cameraTransform.forward * SpawnDistance;

        // Instantiates the projectile at the spawn position. Differentiates between the two possible projectiles.
        var spawnedProjectile =
            isTeleportProjectile ? 
            Instantiate(teleportProjectile, spawnPosition, cameraTransform.rotation) : 
            Instantiate(projectile, spawnPosition, cameraTransform.rotation);


        // Getting the ProjectileBehaviour script and using its SetDirection method.
        var projectileScript = spawnedProjectile.GetComponent<ProjectileBehaviour>();
        projectileScript.SetDirection(cameraTransform.forward);

        projectileScript.isTeleport = isTeleportProjectile;
    }
    
    // Initiates teleportation sequence.
    public void InitiateTeleport(Vector3 teleportPosition)
    {
        // Uses raycasting to ensure the teleport point is above the ground.
        if (Physics.Raycast(teleportPosition + Vector3.up * 50f, Vector3.down, out var hit, Mathf.Infinity))
        {
            _targetTeleportPosition = hit.point; // Adjust to ground level.
            _shouldTeleport = true; // Start teleportation movement.
        }
    }

    // Moves the player towards the teleport point.
    private void MoveTowardsTeleportPosition()
    {
        var step = TeleportSpeed * Time.deltaTime;
        var targetPosition = new Vector3(_targetTeleportPosition.x, transform.position.y, _targetTeleportPosition.z);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        // Stops teleporting when the player reaches the destination.
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            _shouldTeleport = false;
        }
    }
}