using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Declaring and initializing variables.
    public GameObject player;
    private readonly Vector3 _offset = new (0, 1, -2);
    
    // LateUpdate is called after the Update method. Makes sure that the camera doesn't stutter.
    private void LateUpdate()
    {
        // Offsetting the camera away from the players position by adding a new vector3 instance.
        transform.position = player.transform.position + _offset;
    }
}
