using UnityEngine;
using PlayerInputSystem;
using System;


public class PlayerInputTest : MonoBehaviour
{
    private Vector2 _test;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"X: {_test.x} | Y: {_test.y}");
    }

    void OnEnable()
    {
        MyInputManager.onMove += GetMovement;
    }

    void OnDisable()
    {
        MyInputManager.onMove -= GetMovement;
    }

    private void GetMovement(Vector2 axis)
    {
        _test = axis;
    }

}
