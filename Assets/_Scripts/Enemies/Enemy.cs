using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] int _maxHealth = 10;
    [SerializeField] float _damagePoints = 2f;
    [SerializeField] Transform _targetDestination;
    [SerializeField] float _distanceThreshold = 1f;
    Vector3 _destination;
    NavMeshAgent _agent;

    private int _currentHealth;
    public int currentHealth
    {
        get => _currentHealth;
        set => _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            //Return to pool or do something before enemy dies
        }
    }

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _destination = _agent.destination;
    }

    void OnEnable()
    {
        if (_targetDestination != null)
        {
            _destination = _targetDestination.position;
            _agent.destination = _destination;
        }
        else
        {
            Debug.LogError("Target Destination is not set for the enemy.");
        }
        currentHealth = _maxHealth;
        
    }
    void Update()
    {
        if (Vector3.Distance(this.transform.position, _targetDestination.position) > _distanceThreshold)
        {
            _destination = _targetDestination.position;
            _agent.destination = _destination;
        }   
    }


}
