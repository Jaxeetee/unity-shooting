using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int _amountToSpawn = 5;
    [SerializeField] Enemy _enemy;

    //TODO: Spawn enemy at a given radius around the spawner
    //TODO: Use Unity's Object Pooling system for better performance
    private Vector3 GetRandomSpawnPosition()
    {
        float radius = 5f; // Example radius, adjust as needed
        Vector2 randomPoint = Random.insideUnitCircle * radius;
        return new Vector3(randomPoint.x, 0, randomPoint.y) + transform.position;
    }
    
    public void SpawnEnemies()
    {
        for (int i = 0; i < _amountToSpawn; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            Instantiate(_enemy, spawnPosition, Quaternion.identity);
        }
    }
}
