using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[CreateAssetMenu(menuName="Level Design/LevelManagerParams")]
public class LevelManagerParams : ScriptableObject
{
    [Tooltip("The total list of levels in the game")]
    public List<Level> levels;
    

    [Tooltip("Index to track which level we are currently in")]
    public int levelIndex = 0;
}
