using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Declaring and initializing the variables and constants.
    public const float GlobalBound = 25f;
    public GameObject enemyPrefab;
    
    // Start is called before the first frame update.
    private void Start()
    {
        
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        var spawnRangeX = Random.Range(-GlobalBound, GlobalBound);
        var spawnRangeZ = Random.Range(-GlobalBound, GlobalBound);

        var randomPos = new Vector3(spawnRangeX, 0.54f, spawnRangeZ);
        
        Instantiate(enemyPrefab, randomPos, enemyPrefab.transform.rotation);
    }
}
