using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rotation 
{
    _0,
    _90,
    _180,
    _270,
}

public enum SpawnLocation 
{
    TOP, 
    BOTTOM, 
    LEFT, 
    RIGHT
}

[CreateAssetMenu(fileName = "EnemyPattern", menuName = "Level Design/Enemy Pattern", order = 2)]
public class EnemyPattern : ScriptableObject
{
    #region Enemies
    [Header("Enemy Types")]
    [Tooltip("The types of enemies in this pattern (list order determines spawn order)")]
    [SerializeField] private List<EnemyAttributes> _enemies = new List<EnemyAttributes>();
    public List<EnemyAttributes> Enemies => _enemies;
    #endregion

    #region Overrides
    [Header("Enemy Overrides")]
    [Tooltip("Activate overrides for this pattern")]
    [SerializeField] private bool _useOverrides = false;
    public bool UseOverrides => _useOverrides;

    [Tooltip("Override the default enemy rotation for all enemies in this pattern (in degrees, default is 0)")]
    [SerializeField] private Rotation _enemyRotation;
    public Rotation EnemyRotation => _enemyRotation;
    
    [Tooltip("Force the spawn controller to only use a subset of the four main spawn locations")]
    [SerializeField] private List<SpawnLocation> _spawnLocations = new List<SpawnLocation>();
    public List<SpawnLocation> SpawnLocations => _spawnLocations;
    
    [Tooltip("Force enemies to drop specific items (or no items) with a 100% drop rate")]
    [SerializeField] private List<ItemData> _itemDrops = new List<ItemData>();
    public List<ItemData> ItemDrops => _itemDrops;
    #endregion
}
