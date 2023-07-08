using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

[CreateAssetMenu(fileName = "PlayerAttributes", menuName = "Player/PlayerAttributes", order = 1)]
public class PlayerAttributes : AttributeSet, IResetOnExitPlay
{
    #region Combat
    [Header("COMBAT STATS (READ ONLY)")]
    [SerializeField] private ModifiableAttribute _movementSpeed = default(ModifiableAttribute);
    public ModifiableAttribute MovementSpeed => _movementSpeed;

    [SerializeField] private ModifiableAttribute _firingRate = default(ModifiableAttribute);
    public ModifiableAttribute FiringRate => _firingRate;

    [SerializeField] private ModifiableAttribute _damage = default(ModifiableAttribute);
    public ModifiableAttribute Damage => _damage;

    [SerializeField] private ModifiableAttribute _shotCount = default(ModifiableAttribute);
    public ModifiableAttribute ShotCount => _shotCount;

    [SerializeField] private ModifiableAttribute _ammoCap = default(ModifiableAttribute);
    public ModifiableAttribute AmmoCap => _ammoCap;

    [SerializeField] private ModifiableAttribute _ammoUsage = default(ModifiableAttribute);
    public ModifiableAttribute AmmoUsage => _ammoUsage;

    [SerializeField] private ModifiableAttribute _moneyUsage = default(ModifiableAttribute);
    public ModifiableAttribute MoneyUsage => _moneyUsage;

    [SerializeField] private ModifiableAttribute _shotSpeed = default(ModifiableAttribute);
    public ModifiableAttribute ShotSpeed => _shotSpeed;

    [SerializeField] private WeaponData _currentWeapon = default(WeaponData);
    public WeaponData CurrentWeapon => _currentWeapon;

    [SerializeField] private List<WeaponData> _weaponList = new List<WeaponData>();
    public List<WeaponData> WeaponList => _weaponList;

    [SerializeField] private bool _dualFire;
    public bool DualFire => _dualFire;

    [SerializeField] private IntVariable _currentAmmo;

    [SerializeField] private CurseList _curseSlots;
    public CurseList CurseSlots => _curseSlots;

    [Header("STAT TUNING")]
    [Tooltip("How quickly the player can move around in the Half-Skull state")]
    [Range(0f, 90f)]
    [SerializeField] private float _baseMovementSpeed;

    [Tooltip("How quickly the player can shoot in the Full-Skull state (shots per second)")]
    [Range(0f, 16f)]
    [SerializeField] private float _baseFiringRate;

    [Tooltip("How much damage is dealt to enemies per shot")]
    [Range(0, 4)]
    [SerializeField] private int _baseDamage;

    [Tooltip("How many rounds are fired on each shot")]
    [Range(0, 3)]
    [SerializeField] private int _baseShotCount;

    [Tooltip("Maximum amount of ammo player can have at once")]
    [Range(0, 18)]
    [SerializeField] private int _baseAmmoCap;

    [Tooltip("How much additional ammo the player will use on each shot")]
    [Range(0, 3)]
    [SerializeField] private int _baseAmmoUsage;

    [Tooltip("How much additional ammo the player will use on each shot")]
    [Range(0, 4)]
    [SerializeField] private float _baseShotSpeed;
    #endregion

    #region Graphics
    [Header("GRAPHICS")]
    [SerializeField] private Sprite _idleAnimation;
    public Sprite IdleAnimation => _idleAnimation;

    [SerializeField] private Sprite _deathAnimation;
    public Sprite DeathAnimation => _deathAnimation;
    
    [SerializeField] private Sprite _walkAnimation;
    public Sprite WalkAnimation => _walkAnimation;

    [SerializeField] private Sprite _shootAnimation;
    public Sprite ShootAnimation => _shootAnimation;
    #endregion

    #region Sounds
    [Header("SOUNDS")]
    [SerializeField] private AudioClip _walkSound;
    public AudioClip WalkSound => _walkSound;
    #endregion

    #region Methods
    void OnValidate()
    {
        SetBaseValuesOnValidate();
        
        foreach(ModifiableAttribute attribute in _attributes)
        {
            attribute.CalculateValue();
        }
    }

    public void ResetAttributeList()
    {
        _attributes.Clear();

        _attributes.Add(_movementSpeed);
        _attributes.Add(_firingRate);
        _attributes.Add(_damage);
        _attributes.Add(_shotCount);
        _attributes.Add(_ammoCap);
        _attributes.Add(_ammoUsage);
        _attributes.Add(_moneyUsage);
        _attributes.Add(_shotSpeed);
        _dualFire = false;
        _curseSlots.Clear();

        InitAttributes();
    }

    public void ResetOnExitPlay()
    {
        ResetAttributeList();
    }

    public void OnEnable()
    {
        ResetAttributeList();
    }

    public void ApplyModifier(Modifier mod)
    {
        switch(mod.TargetAttribute)
        {
            case AttributeType.MOVEMENT_SPEED:
                _movementSpeed.AddModifier(mod);
                break;

            case AttributeType.DAMAGE:
                _firingRate.AddModifier(mod);
                break;

            case AttributeType.SHOT_SPEED:
                _shotSpeed.AddModifier(mod);
                break;

            case AttributeType.SHOT_COUNT:
                _shotCount.AddModifier(mod);
                break;

            case AttributeType.AMMO_CAP:
                _ammoCap.AddModifier(mod);
                _currentAmmo.Value = (int) _ammoCap.CurrentValue;
                break;

            case AttributeType.AMMO_USAGE:
                _ammoUsage.AddModifier(mod);
                break;

            case AttributeType.MONEY_USAGE:
                _moneyUsage.AddModifier(mod);
                break;

            case AttributeType.DUAL_FIRE:
                _dualFire = true;
                break;
        }
    }

    public void RemoveModifier(Modifier mod)
    {
        switch(mod.TargetAttribute)
        {
            case AttributeType.MOVEMENT_SPEED:
                _movementSpeed.RemoveModifier(mod);
                break;

            case AttributeType.DAMAGE:
                _damage.RemoveModifier(mod);
                break;

            case AttributeType.SHOT_SPEED:
                _shotSpeed.RemoveModifier(mod);
                break;

            case AttributeType.SHOT_COUNT:
                _shotCount.RemoveModifier(mod);
                break;

            case AttributeType.AMMO_CAP:
                _ammoCap.RemoveModifier(mod);
                break;

            case AttributeType.AMMO_USAGE:
                _ammoUsage.RemoveModifier(mod);
                break;

            case AttributeType.MONEY_USAGE:
                _moneyUsage.RemoveModifier(mod);
                break;
        
            case AttributeType.DUAL_FIRE:
                _dualFire = false;
                break;
        }
    }

    public void SetCurrentWeapon(WeaponData weapon)
    {
        _currentWeapon = weapon;
    }

    public void SetBaseValuesOnValidate() 
    {
        _movementSpeed.SetBaseValue(_baseMovementSpeed);
        _damage.SetBaseValue((float) _baseDamage);
        _shotCount.SetBaseValue((float) _baseShotCount);
        _ammoCap.SetBaseValue((float) _baseAmmoCap);
        _ammoUsage.SetBaseValue((float) _baseAmmoUsage);
        _moneyUsage.SetBaseValue(0f);
        _dualFire = false;
        _shotSpeed.SetBaseValue(_baseShotSpeed);
    }
    #endregion
}
