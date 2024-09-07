using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    // Declaring and initializing the variables and constants.
    public const float GlobalBound = 25f;
    public GameObject enemyPrefab;
    private bool _gameOver = false;

    private void Start()
    {
        // Starts the coroutine to spawn enemies in defined intervals. 
        StartCoroutine(SpawnInterval());
    }

    private void SpawnEnemy()
    {
        // Generates two random Positions within the global bounds.
        var spawnRangeX = Random.Range(-GlobalBound, GlobalBound);
        var spawnRangeZ = Random.Range(-GlobalBound, GlobalBound);

        // Creates a new position based on the global bounds.
        var randomPos = new Vector3(spawnRangeX, 0.54f, spawnRangeZ);
        
        // Creates a new enemy object.
        Instantiate(enemyPrefab, randomPos, enemyPrefab.transform.rotation);
    }

    // Coroutine to repeatedly spawn enemies.
    private IEnumerator SpawnInterval()
    {
        while(!_gameOver)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(4f);
        }
    }
}
