using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    TrailRenderer _bulletTrail;
    [SerializeField] float _speed = 50f;

    [Tooltip("number of seconds on how long the bullet will stay active before it disappears")]
    [SerializeField] float _lifeTime = 5f;
    bool _didHit = false;

    IObjectPool<Bullet> _objectPool;

    public IObjectPool<Bullet> objectPool
    {
        set => _objectPool = value;
    }

    int _damagePoints = 0;
    public int damagePoints
    {
        get => _damagePoints;
        set => _damagePoints = value;
    }

    void Awake()
    {
        _bulletTrail = GetComponent<TrailRenderer>();
    }

    void OnEnable()
    { 
        _didHit = false;
        
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(LifeTimeDecay(_lifeTime));
        }
    }

    void OnDisable()
    {
        // _bulletTrail.Clear();
        // StopAllCoroutines();
    }

    public void SetBulletStats(float speed, int damage = 0)
    {
        _speed = speed;
        damagePoints = damage;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * _speed * Time.deltaTime, Space.World);

        //TODO create a detection to disable the bullet
        Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //Debug.Log("Bullet hit: " + hit.collider.name);
                _didHit = true;

                IDamageable damageable = hit.collider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damagePoints);
                }

                // this.gameObject.transform.position = Vector3.zero;
                // this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

                _objectPool.Release(this);
            }
    }

    IEnumerator LifeTimeDecay(float duration)
    {
        float timeLeft = duration;
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        this.gameObject.transform.position = Vector3.zero;
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        _objectPool.Release(this);
    }

    IEnumerator CollisionDetection()
    {
        while (!_didHit)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //Debug.Log("Bullet hit: " + hit.collider.name);
                _didHit = true;

                IDamageable damageable = hit.collider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damagePoints);
                }

                // this.gameObject.transform.position = Vector3.zero;
                // this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

                _objectPool.Release(this);
            }

            yield return null;
        }
        
    }
    
    
}
