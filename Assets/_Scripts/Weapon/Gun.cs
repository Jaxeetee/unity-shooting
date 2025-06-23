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
    int _burstRemaining = 0;

    [Header("Bullet Attributes")]
    [SerializeField] float _bulletVelocity;
    [SerializeField] Bullet _bullet;
    [SerializeField] Transform _bulletPosition;

    IObjectPool<Bullet> _objectPool;

    float _nextTimeToShoot;
    int _currentAmount = 0; //Amount of bullets left in a magazine 
    bool _isReloading = false;
    bool _isShooting;
    bool _isBursting;

    void Awake()
    {
        _objectPool = new ObjectPool<Bullet>(CreateProjectile, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject);
    }

    void Start()
    {
        _currentAmount = _magazineSize;

        if (_fireMode == FireMode.Burst)
            _burstRemaining = _burstAmount;
    }

    void OnEnable()
    {

    }

    void OnDisable()
    {

    }

    #region ==== OBJECT POOLING ====
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
    #endregion

    void Shoot()
    {
        if (_isReloading && _currentAmount == 0) return;

        Debug.Log("isFiring");
        if (_fireMode == FireMode.Burst)
        {
            if (!_isShooting) return;
            StartCoroutine(BurstShoot());
        }
        else if (_fireMode == FireMode.Full)
        {
            if (!_isShooting) return;
            StartCoroutine(FullAutoShoot());
        }
        if (Time.time > _nextTimeToShoot)
        {
            SpawnProjectiles();
            _nextTimeToShoot = Time.time + _fireRate / 1000;
            _currentAmount--;
        }

        
    }

    IEnumerator FullAutoShoot()
    {
        while (_isShooting)
        {
            if (Time.time > _nextTimeToShoot)
            {
                Debug.Log("~~~~~~~~~Auto shooting~~~~~~!");
                SpawnProjectiles();
                _nextTimeToShoot = Time.time + _fireRate / 1000;
                _currentAmount--;
            }
            yield return null;
        }
    }

    IEnumerator BurstShoot()
    {
        while (!_isBursting && _burstRemaining > 0)
        {
            if (Time.time > _nextTimeToShoot)
            {
                Debug.Log("--------Burst shooting-------!");
                SpawnProjectiles();
                _nextTimeToShoot = Time.time + _fireRate / 1000;
                _burstRemaining--;
                _currentAmount--;
            }
            yield return null;
        }
        _isBursting = true;
    }

    void SpawnProjectiles()
    {
        Bullet bulletObject = _objectPool.Get();
        bulletObject.SetSpeed(_bulletVelocity);
        bulletObject.gameObject.transform.position = _bulletPosition.position;
        bulletObject.gameObject.transform.rotation = _bulletPosition.rotation;
    }

    public void Reload()
    {

    }

    public void OnTriggerHold()
    {
        _isShooting = true;
        Shoot();
    }

    public void OnTriggerReleased()
    {
        _isShooting = false;
        _burstRemaining = _burstAmount;
    }

}
