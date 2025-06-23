using UnityEngine;
using PlayerInputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float _speed = 0.5f;
    
    Rigidbody _rigidbody;
    Vector2 _inputAxis;
    Vector3 _movePos;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        PlayerInputManager.onMove += GetMovementInput;
    }

    void OnDisable()
    {
        PlayerInputManager.onMove -= GetMovementInput;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + _movePos * Time.fixedDeltaTime * _speed);
    }

    void GetMovementInput(Vector2 axis)
    {
        _movePos = new Vector3(axis.x, 0, axis.y);
    }
}
