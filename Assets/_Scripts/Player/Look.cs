using UnityEngine;
using PlayerInputSystem;

public class Look : MonoBehaviour
{
    Vector2 _mousePos; //* Input Mouse Position
    Camera _mainCam;
    Vector3 _direction; //* Direction of where the player looks at
    float angle;

    [SerializeField] GameObject _mouseGameObject; 

    [SerializeField] LayerMask _layerMask;
    void Start()
    {
        _mainCam = Camera.main;
    }

    void OnEnable()
    {
        PlayerInputManager.onLook += GetMousePosition;
    }

    void OnDisable()
    {
        PlayerInputManager.onLook -= GetMousePosition;
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
            //Making Gameobject reference points for the mouse pos in the world point for influencing camera's center point
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
