using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Pool;
using PlayerInputSystem;
using System.Collections;

enum FireMode
{
    Semi,
    Full,
    Burst
}

public class Gun : MonoBehaviour
{

    [Header("Gun Attributes")]
    [SerializeField] float _fireRate;
    [SerializeField] int _magazineSize = 30;
    [SerializeField] FireMode _fireMode = FireMode.Full;
    [SerializeField] int _burstAmount = 3;

    [Header("Bullet Attributes")]
    [SerializeField] float _bulletVelocity;
    [SerializeField] Bullet _bullet;
    [SerializeField] Transform _bulletPosition;

    IObjectPool<Bullet> _objectPool;

    float _nextTimeToShoot;
    bool _isShooting;
    int _currentAmount = 0; //Amount of bullets left in a magazine 

    bool _isReloading;

    void Awake()
    {
        _objectPool = new ObjectPool<Bullet>(CreateProjectile, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject);
    }

    Bullet CreateProjectile()
    {
        Bullet bulletInstance = Instantiate(_bullet);
        bulletInstance.objectPool = _objectPool;
        return bulletInstance;
    }

    void OnGetFromPool(Bullet pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    void OnReleaseToPool(Bullet pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    void OnDestroyPooledObject(Bullet pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    public void ShootProjectile()
    {
        Bullet bulletObject = _objectPool.Get();
        bulletObject.SetSpeed(_bulletVelocity);
        bulletObject.gameObject.transform.position = _bulletPosition.position;
        bulletObject.gameObject.transform.rotation = _bulletPosition.rotation;
    }

    public void Reload()
    {

    }

}
