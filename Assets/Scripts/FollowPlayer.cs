using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Declaring and initializing variables.
    public GameObject player;
    private readonly Vector3 _offset = new (0, 1, -2);
    private const float ViewAngle = 1.0f;
    
    // LateUpdate is called after the Update method. Makes sure that the camera doesn't stutter.
    private void LateUpdate()
    {
        // Calculates and sets the new position for the camera.
        transform.position = player.transform.position + player.transform.TransformDirection(_offset);
        
        // Makes the camera look at the player.
        transform.LookAt(player.transform.position + Vector3.up * ViewAngle);
    }
}
