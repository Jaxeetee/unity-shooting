using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    TrailRenderer _bulletTrail;
    [SerializeField] float _speed = 50f;

    [Tooltip("number of seconds on how long the bullet will stay active before it disappears")]
    [SerializeField] float _lifeTime = 5f;

    IObjectPool<Bullet> _objectPool;

    public IObjectPool<Bullet> objectPool
    {
        set => _objectPool = value;
    }

    void Awake()
    {
        _bulletTrail = GetComponent<TrailRenderer>();
    }
    
    void OnEnable()
    {
        StartCoroutine(LifeTimeDecay(_lifeTime));
    }

    void OnDisable()
    {
        _bulletTrail.Clear();
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * _speed * Time.deltaTime, Space.World);

        //TODO create a detection to disable the bullet
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
}
