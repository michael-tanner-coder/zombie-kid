using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class Bullet : MonoBehaviour
{
    [SerializeField] private AmmoType _ammoType = AmmoType.NONE;
    [SerializeField] private GameObject _iceBlockPrefab;
    [SerializeField] private GameObject _rockPrefab;
    public AmmoType AmmoType => _ammoType;
    public bool CanBeDestoyedByEnemies = true;

    [SerializeField] private float _timeUntilLethal = 0.5f;
    private bool _lethal = false;
    public bool Lethal => _lethal;

    public void Update()
    {
        _timeUntilLethal -= 1f * Time.deltaTime;

        if (_timeUntilLethal <= 0.0f)
        {
            _lethal = true;
            _timeUntilLethal = 0;
        }
    }

    public void SetAmmoType(AmmoType ammoType)
    {
        _ammoType = ammoType;
    }

    public void ApplyAmmoEffect(GameObject target)
    {
        switch (_ammoType)
        {
            case AmmoType.FREEZE:
                FreezeEffect(target);
                break;
            case AmmoType.STONE:
                StoneEffect(target);
                break;
            default:
                break;
        }
    }

    public void FreezeEffect(GameObject target)
    {
        // spawn a frozen version of the enemy
        GameObject frozenTarget = Instantiate(gameObject, target.transform.position, Quaternion.identity);
        GameObject iceBlock = Instantiate(_iceBlockPrefab, target.transform.position, Quaternion.identity); 
        iceBlock.transform.SetParent(frozenTarget.transform);

        // set the frozen target's damage property to as high as possible
        frozenTarget.AddComponent<DamageController>();
        frozenTarget.GetComponent<DamageController>().SetDamage(10);

        // update sprite to match the target
        Sprite targetSprite = target.GetComponent<SpriteRenderer>().sprite;
        frozenTarget.GetComponent<SpriteRenderer>().sprite = targetSprite;
        frozenTarget.transform.localScale = new Vector3(1f, 1f, 1f);
        iceBlock.transform.localScale = new Vector3(1f, 1f, 1f);

        // make the frozen block indestructible by enemies
        Bullet bulletProperties = frozenTarget.GetComponent<Bullet>();
        bulletProperties.CanBeDestoyedByEnemies = false;
        bulletProperties.SetAmmoType(AmmoType.NONE);
        
        // destroy original target
        Destroy(target);
    }

    public void StoneEffect(GameObject target)
    {
        GameObject rock = Instantiate(_rockPrefab, target.transform.position, Quaternion.identity);
        
        Destroy(target);

    }
}
