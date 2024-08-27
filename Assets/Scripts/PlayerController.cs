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
    
    private const float MinX = -25f;
    private const float MaxX = 25f;
    private const float MinZ = -25f;
    private const float MaxZ = 25f;
    
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
        
        //ToDo: On left mouse click instantiate a projectile
    }

    // Called at a fixed interval and is preferred for physics-based updates.
    private void FixedUpdate()
    {
        // Calculates movement direction relative to the player's orientation.
        var movement = transform.forward * _verticalInput + transform.right * _horizontalInput;

        // Calculates new position.
        var newPosition = transform.position + movement * (Speed * Time.fixedDeltaTime);

        // Keeps the newPosition values inside the set min and max bounds.
        newPosition.x = Mathf.Clamp(newPosition.x, MinX, MaxX);
        newPosition.z = Mathf.Clamp(newPosition.z, MinZ, MaxZ);

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
}
