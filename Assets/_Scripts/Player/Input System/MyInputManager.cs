using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace PlayerInputSystem
{
    public class MyInputManager : MonoBehaviour
    {
        private PlayerControls _playerControls;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _playerControls = new PlayerControls();

            _playerControls.Enable();    

            EnableActionMap(StringManager.PLAYER);
            PlayerInputs();
        }

        public void EnableActionMap(string name)
        {
            _playerControls.asset.FindActionMap(name).Enable();
        }

        public void DisableActionMap(string name)
        {
            _playerControls.asset.FindActionMap(name).Disable();
        }

        public static Action<Vector2> onMove; 
        public static Action<Vector2> onLook; 
        public static Action<float> onInteract; 
        public static Action<float> onReload; 
        public static Action<float> onShoot; 
        public static Action<float> onPause;

        void PlayerInputs()
        {
            InputActionMap inputMap = _playerControls.Player;

            inputMap.FindAction(StringManager.MOVE).performed += ctx =>
            {
                Vector2 axis = ctx.ReadValue<Vector2>();
                onMove?.Invoke(axis);
            };

            inputMap.FindAction(StringManager.MOVE).canceled += ctx =>
            {
                Vector2 axis = ctx.ReadValue<Vector2>();
                onMove?.Invoke(axis);
            };

            inputMap.FindAction(StringManager.LOOK).performed += ctx =>
            {
                Vector2 axis = ctx.ReadValue<Vector2>();
                onLook?.Invoke(axis);
            };

            inputMap.FindAction(StringManager.LOOK).canceled += ctx =>
            {
                Vector2 axis = ctx.ReadValue<Vector2>();
                onLook?.Invoke(axis);
            };

            inputMap.FindAction(StringManager.INTERACT).performed += ctx =>
            {
                float value = ctx.ReadValue<float>();
                onInteract?.Invoke(value);
            };

            inputMap.FindAction(StringManager.INTERACT).canceled += ctx =>
            {
                float value = ctx.ReadValue<float>();
                onInteract?.Invoke(value);
            };

            inputMap.FindAction(StringManager.RELOAD).performed += ctx =>
            {
                float value = ctx.ReadValue<float>();
                onReload?.Invoke(value);
            };

            inputMap.FindAction(StringManager.RELOAD).canceled += ctx =>
            {
                float value = ctx.ReadValue<float>();
                onReload?.Invoke(value);
            };

            inputMap.FindAction(StringManager.SHOOT).started += ctx =>
            {
                float value = ctx.ReadValue<float>();
                onShoot?.Invoke(value);
            };

            inputMap.FindAction(StringManager.SHOOT).performed += ctx =>
            {
                float value = ctx.ReadValue<float>();
                onShoot?.Invoke(value);
            };

            inputMap.FindAction(StringManager.SHOOT).canceled += ctx =>
            {
                float value = ctx.ReadValue<float>();
                onShoot?.Invoke(value);
            };

            inputMap.FindAction(StringManager.PAUSE).performed += ctx =>
            {
                float value = ctx.ReadValue<float>();
                onPause?.Invoke(value);
            };

            inputMap.FindAction(StringManager.PAUSE).canceled += ctx =>
            {
                float value = ctx.ReadValue<float>();
                onPause?.Invoke(value);
            };






            
        }

        
    }
}
