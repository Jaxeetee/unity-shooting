using UnityEngine;
using PlayerInputSystem;

public class Look : MonoBehaviour
{
    Vector3 _mousePos; //* Input Mouse Position
    Camera _mainCam;
    Vector3 _direction; //* Direction of where the player looks at
    float angle;

    [SerializeField] private float _rotationSpeed = 15f;

    [SerializeField] LayerMask _layerMask;

    void Start()
    {
        _mainCam = Camera.main;
    }

    void OnEnable()
    {
        MyInputManager.onLook += GetMousePosition;
    }

    void OnDisable()
    {
        MyInputManager.onLook -= GetMousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"mouse pos(X:{_mousePos.x}, Y:{_mousePos.y}, Z:{_mousePos.z})");
        AimAtMousePosition();
        
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = _mainCam.ScreenPointToRay(_mousePos);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, _layerMask))
        {
            Debug.DrawRay(transform.position,hitInfo.point);
            return (success: true, position: hitInfo.point);
        }
        else
        {
            return (success: false, position: Vector3.zero);
        }
    }

    private void AimAtMousePosition()
    {
        var (success, position) = GetMousePosition();

        if (success)
        {
            // Calculating the direction of the mouse relative to player position
            _direction = position - transform.position;

            // Ignoring the height difference
            _direction.y = 0;

            // Make the player look at that direction
            transform.forward = _direction;
        }
        // _direction = _mainCam.ScreenToWorldPoint(_mousePos) - transform.position;
        // angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        // Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
        // transform.rotation = rotation;
    }

    void GetMousePosition(Vector2 pos) => _mousePos = new Vector3(pos.x, pos.y, 0);
}
