using UnityEngine;
using PlayerInputSystem;

public class Look : MonoBehaviour
{
    Vector2 _mousePos; //* Input Mouse Position
    Camera _mainCam;
    Vector3 _direction; //* Direction of where the player looks at
    float angle;

    [SerializeField] GameObject _mouseGameObject; 

    [SerializeField] float _rotationSpeed = 15f;

    [SerializeField] LayerMask _layerMask;

    [SerializeField] float _maxDistance = 5f;

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

    void Update()
    {
        AimAtMousePosition();   
    }


    private Vector3 MousePositionOnGround()
    {
        Ray ray = _mainCam.ScreenPointToRay(_mousePos);
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, _layerMask))
        {
            _mouseGameObject.transform.position = hitInfo.point;
            return hitInfo.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private void AimAtMousePosition()
    {
        var position = MousePositionOnGround();
            // Calculating the direction of the mouse relative to player position
        _direction = position - transform.position;

        // Ignoring the height difference
        _direction.y = 0;

        // Make the player look at that direction
        float rotation = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, rotation, 0);
        
    }

    void GetMousePosition(Vector2 pos) => _mousePos = pos;
}
