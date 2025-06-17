using UnityEngine;
using PlayerInputSystem;

public class GunController : MonoBehaviour
{
    [SerializeField] Gun _equippedGun;

    [SerializeField] Gun[] _currentWeapons;
    void OnEnable()
    {
        PlayerInputManager.onShoot += ShootInput;
        PlayerInputManager.onReload += ReloadInput;
    }

    void OnDisable()
    {
        PlayerInputManager.onShoot -= ShootInput;
        PlayerInputManager.onReload -= ReloadInput;
    }

    void ShootInput(float value)
    {

    }

    void ReloadInput(float value)
    {

    }
}
