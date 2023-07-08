using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

// === What is a Wave? ===
// A wave is a set of rhythmic enemy spawns that create a specific challenge for the player.

// Waves test the player's skill against certain enemies & attack patterns.
// Skills to test: counting shots, observing weakpoints, racking up combos, accuracy/timing of shots

// Waves can also teach player's these skills by spawning simple enemy patterns that showcase a new enemy's traits.
// Concepts to teach: weakpoints, rotations, swerves, teleportation, enemy health, enemy speed, item drops

// === What is a Rest? ===
// A rest is a period of time between enemy waves in which the player can pick up items and prepare for the next round.
// Note: the period where players are visiting the mini-shop or collecting a curse are not considered "rests". They are simply disconnected from combat.

// === Floors & Levels ===
// A set of waves makes a "floor", upon finishing all waves in a floor the player can visit the mini-shop, escape the dungeon, or move to the next floor.

// A set of "floors" make a "level". Upon finishing all floors in a level, the player can choose one of three curses to keep. They can then move to the next level or escape the dungeon.


[CreateAssetMenu(fileName = "WaveData", menuName = "Level Design/Enemy Wave", order = 1)]
public class WaveData : ScriptableObject
{
    #region Designer Information
    [Header("Level Design Information (no gameplay relevance)")]
    [Tooltip("What to call this wave (not shown in-game)")]
    [SerializeField] private string _waveName;

    [Tooltip("Describe the purpose of this enemy wave. What skills does it test or teach? (not shown in-game)")]
    [TextArea(15,20)]
    [SerializeField] private string _designPurpose;
    #endregion

    #region Wave/Rest Stats
    [Header("Wave/Rest Duration")]
    [Tooltip("How long the wave will last (in seconds)")]
    [SerializeField] private float _duration = 10f;
    public float Duration => _duration;

    [Tooltip("How long the rest period will last (in seconds)")]
    [SerializeField] private float _restPeriod = 5f;
    public float RestPeriod => _restPeriod;
    
    [Tooltip("Speed multiplier of enemies in this wave")]
    [Range(0.1f, 1.5f)]
    [SerializeField] private float _speedFactor = 1f;
    public float SpeedFactor => _speedFactor;

    [Tooltip("Change in speed intensity over time (the y value is subtracted from timeBetweenSpawns))")]
    [SerializeField] private AnimationCurveReference _speedRateChange;
    public AnimationCurveReference SpeedRateChange => _speedRateChange;

    [Header("Enemy Spawns")]
    [Tooltip("What patterns of enemies will spawn in this wave")]
    [SerializeField] private List<EnemyPattern> _enemyPatterns = new List<EnemyPattern>(); 
    public List<EnemyPattern> EnemyPatterns => _enemyPatterns;

    [Tooltip("If checked, it will choose a random pattern on each spawn; otherwise, it will follow the list order")]
    [SerializeField] private bool _chooseRandomPattern = true;

    [SerializeField] private int _minEnemyCount;
    public int MinEnemyCount => _minEnemyCount;

    [SerializeField] private int _spawnCount;
    public int SpawnCount => _spawnCount;
    #endregion
}
