using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Pool;
using PlayerInputSystem;

public class Gun : MonoBehaviour
{
    [Header("Gun Attributes")]
    [SerializeField] float _rateOfFire;
    [SerializeField] int _defaultSize;
    [SerializeField] int _magazineSize;

    [Header("Bullet Attributes")]
    [SerializeField] float _bulletVelocity;
    [SerializeField] Bullet _bullet;
    [SerializeField] Transform _bulletPosition;

    IObjectPool<Bullet> _objectPool;

    float _nextTimeToShoot;

    void Awake()
    {
        _objectPool = new ObjectPool<Bullet>(CreateProjectile, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject);
    }

    void OnEnable()
    {
        MyInputManager.onShoot += ShootInput;
    }

    void OnDisable()
    {
        MyInputManager.onShoot -= ShootInput;
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

    void ShootInput(float val)
    {
        Debug.Log("Is Shooting");
        if (val > 0)
        {

            Bullet bulletObject = _objectPool.Get();
            bulletObject.SetSpeed(_bulletVelocity);
            bulletObject.gameObject.transform.position = _bulletPosition.position; ;
            bulletObject.gameObject.transform.rotation = _bulletPosition.rotation; ;
        }
    }

}
