using UnityEngine;
using PlayerInputSystem;

public class MousePositionGameObject : MonoBehaviour
{
    Vector2 _inputMousePos;

    void OnEnable()
    {
        MyInputManager.onLook += GetMousePosition;
    }

    void OnDisable()
    {
        MyInputManager.onLook -= GetMousePosition;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetMousePosition(Vector2 inputMousePosition) => _inputMousePos = inputMousePosition;
}
