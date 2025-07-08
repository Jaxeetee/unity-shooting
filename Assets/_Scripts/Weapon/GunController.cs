using UnityEngine;
using PlayerInputSystem;

public class GunController : MonoBehaviour
{
    [SerializeField] Transform _hand;
    [SerializeField] GameObject _primaryWeapon;
    [SerializeField] GameObject _secondaryWeapon;
    bool _isPrimaryActive;

    void Start()
    {
        _primaryWeapon = Instantiate(_primaryWeapon);
        _primaryWeapon.transform.SetParent(_hand);
        _primaryWeapon.transform.position = _hand.position;
        _primaryWeapon.transform.rotation = _hand.rotation;
        _primaryWeapon.SetActive(true);
        _isPrimaryActive = true;

        _secondaryWeapon = Instantiate(_secondaryWeapon);
        _secondaryWeapon.transform.SetParent(_hand);
        _secondaryWeapon.transform.position = _hand.position;
        _secondaryWeapon.transform.rotation = _hand.rotation;
        _secondaryWeapon.gameObject.SetActive(false);
    }
    void OnEnable()
    {
        PlayerInputManager.onShoot += ShootInput;
        PlayerInputManager.onReload += ReloadInput;
        PlayerInputManager.onSwitch += SwitchInput;
    }

    void OnDisable()
    {
        PlayerInputManager.onShoot -= ShootInput;
        PlayerInputManager.onReload -= ReloadInput;
        PlayerInputManager.onSwitch -= SwitchInput;
    }

    void ChangeWeapon(float value)
    {
        if (_primaryWeapon == null || _secondaryWeapon == null) return;

        if (value > 0)
        {
            if (_isPrimaryActive)
            {
                _primaryWeapon.SetActive(false);
                _secondaryWeapon.SetActive(true);
                _isPrimaryActive = !_isPrimaryActive;
            }
            else
            {
                _primaryWeapon.SetActive(true);
                _secondaryWeapon.SetActive(false);
                _isPrimaryActive = !_isPrimaryActive;
            }
        }
    }

    void ShootInput(float value)
    {
        //! I need to make it so that it will work flawlessly on different equipped weapons
        if (value == 1)
        {
            Gun gun = _primaryWeapon.GetComponent<Gun>();
            gun.OnTriggerHold();
        }
        else
        {
            Gun gun = _primaryWeapon.GetComponent<Gun>();
            gun.OnTriggerReleased();
        }

    }

    void ReloadInput(float value)
    {
        Gun gun = _primaryWeapon.GetComponent<Gun>();
        gun.Reload();
    }

    void SwitchInput(float value)
    {
        ChangeWeapon(value);
    }
}
