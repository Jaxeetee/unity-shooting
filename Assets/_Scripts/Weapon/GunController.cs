using UnityEngine;
using PlayerInputSystem;

public class GunController : MonoBehaviour
{
    void OnEnable()
    {
        PlayerInputManager.onShoot += ShootInput;
    }

    void OnDisable()
    {
        PlayerInputManager.onShoot -= ShootInput;
    }

    void ShootInput(float value)
    {

    }
}
