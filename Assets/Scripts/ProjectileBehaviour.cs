using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private GameObject _player;
    private const float Speed = 20.0f;
    
    // Start is called before the first frame update.
    private void Start()
    {
        _player = GameObject.Find("Player");
        transform.position = _player.transform.position + new Vector3(0f, 0f, 2f);
    }

    // Update is called once per frame.
    private void Update()
    {
        transform.Translate(Vector3.forward * (Speed * Time.deltaTime));
    }
}
