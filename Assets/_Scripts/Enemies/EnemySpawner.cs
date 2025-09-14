using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int _amountToSpawn = 5;
    [SerializeField] Enemy _enemy;

    int _currentlySpawned = 0;
    IObjectPool<Enemy> _objectPool;

    //TODO: Spawn enemy at a given radius around the spawner
    //TODO: Use Unity's Object Pooling system for better performance

    void Awake() => _objectPool = new ObjectPool<Enemy>(CreateEnemy, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject);

    void Start()
    {
        SpawnEnemies();
    } 

    #region OBJECT POOLING METHODS
    private void OnDestroyPooledObject(Enemy pooledEnemy)
    {
        Destroy(pooledEnemy.gameObject);
    }

    private void OnReleaseToPool(Enemy pooledEnemy)
    {
        pooledEnemy.gameObject.SetActive(false);
    }

    private void OnGetFromPool(Enemy pooledEnemy)
    {
        pooledEnemy.gameObject.SetActive(true);
    }

    private Enemy CreateEnemy()
    {
        Enemy newEnemy = Instantiate(_enemy);
        newEnemy.objectPool = _objectPool;
        return newEnemy;
    }

#endregion
    private Vector3 GetRandomSpawnPosition()
    {
        float radius = 5f; // Example radius, adjust as needed
        Vector2 randomPoint = Random.insideUnitCircle * radius;
        return new Vector3(randomPoint.x, 3f, randomPoint.y) + transform.position;
    }
    
    public void SpawnEnemies()
    {
        // if (_currentlySpawned == 0)
        //     for (int i = 0; i < _amountToSpawn; i++)
        //     {
        //         CreateEnemy();
        //         _currentlySpawned++;
        //     }
        for (int i = 0; i < _amountToSpawn; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            Enemy enemy = _objectPool.Get();
            enemy.transform.position = spawnPosition;
            enemy.transform.rotation = Quaternion.identity;
        }
    }
}
