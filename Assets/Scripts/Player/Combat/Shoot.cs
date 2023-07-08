using UnityEngine;
using ScriptableObjectArchitecture;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Vector2Variable _aimDirection;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameEvent _shootEvent;
    [SerializeField] private PlayerAttributes _player;
    [SerializeField] private IntReference _ammo;
    [SerializeField] private PlayerMoneyStore _moneyStore;

    void Awake()
    {
        InputHandler _inputHandler = ServiceLocator.Instance.Get<InputManager>().Inputs();
        _inputHandler.Shoot().performed += OnShootWrapper;
    }

    void OnShootWrapper(InputAction.CallbackContext context)
    {
        if (enabled)
        {
            OnShoot(context);
        }
    }

    void OnShoot(InputAction.CallbackContext context)
    {
        // get the ammo effect of the player's current weapon
        AmmoEffect effect = _player.CurrentWeapon.Effect;
        int ammoUsage = _player.CurrentWeapon.BaseAmmoUsage + (int) _player.AmmoUsage.CurrentValue;
        if (ammoUsage < 0) 
        {
            ammoUsage = 0;
        }
        int moneyUsage = (int) _player.MoneyUsage.CurrentValue;

        // update aiming direction with inputs and shoot when the timer reaches 1
        if (_ammo.Value >= ammoUsage && _moneyStore.OnHandMoney.Value >= moneyUsage)
        {
            float offsetAmount = 1f;
            float _aimDirectionX = context.ReadValue<Vector2>().x;
            float _aimDirectionY = context.ReadValue<Vector2>().y;
            _aimDirection.Value = new Vector2(_aimDirectionX, _aimDirectionY);

            for (float i = 0; i < _player.ShotCount.CurrentValue; i++)
            {
                float offsetX = _aimDirectionX * offsetAmount * i;
                float offsetY = _aimDirectionY * offsetAmount * i;
                SpawnProjectile(offsetX, offsetY);

                if (_player.DualFire)
                {
                    _aimDirection.Value = new Vector2(_aimDirectionX * -1, _aimDirectionY * -1);
                    SpawnProjectile(offsetX, offsetY);
                }

                _ammo.Value -= ammoUsage;
                _moneyStore.SubtractOnHandMoney(moneyUsage);
            }

            
            _shootEvent.Raise();
            ServiceLocator.Instance.Get<AudioManager>().PlaySoundFromDictionary("PlayerShoot");
        }
    }

    void SpawnProjectile(float bufferX, float bufferY)
    {
        // set spawn position
        Vector3 spawnPosition = new Vector3(transform.position.x + bufferX, transform.position.y + bufferY, transform.position.z);
        
        // spawn object
        GameObject _newProjectile = Instantiate(_projectile, spawnPosition, Quaternion.identity);

        // set rotation
        float angle = Mathf.Atan2(_aimDirection.Value.y, _aimDirection.Value.x) * Mathf.Rad2Deg - 90f;
        _newProjectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // move the new object in the aiming direction
        MoveInOwnDirection moveComponent = _newProjectile.GetComponent<MoveInOwnDirection>();
        if (moveComponent != null)
        {
            _newProjectile.GetComponent<MoveInOwnDirection>().SetDirection(_aimDirection.Value);
            moveComponent.SetSpeed(moveComponent.Speed.Value * _player.ShotSpeed.CurrentValue);
        }

        // set damage of the projectile
        DamageController damageController = _newProjectile.GetComponent<DamageController>();
        if (damageController != null)
        {
            damageController.SetDamage(_player.Damage.CurrentValue);
        }

        // set ammo type of bullet
        Bullet bullet = _newProjectile.GetComponent<Bullet>();
        if (bullet != null && _player.CurrentWeapon.Effect != null)
        {
            AmmoType currentAmmoType = _player.CurrentWeapon.Effect.Type;
            bullet.SetAmmoType(currentAmmoType);

            if (bullet.AmmoType == AmmoType.PIERCE)
            {
                bullet.CanBeDestoyedByEnemies = false;
            }
        }

        // change sprite
        SpriteRenderer _renderer = _newProjectile.GetComponent<SpriteRenderer>();
        if (_renderer != null)
        {
            _renderer.sprite = _player.CurrentWeapon.EquipIcon;
        }
    }
}
