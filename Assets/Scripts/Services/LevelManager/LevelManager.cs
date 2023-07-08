using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : IService
{
    private readonly LevelManagerParams _params = Resources.Load<LevelManagerParams>("Level Design/LevelManagerParams");
    public delegate void ChangeLevel();
    public static event ChangeLevel OnLevelChange;

    
    public LevelManager() 
    {
        _params.levelIndex = 0;
    }

    ~LevelManager() {}

    // if we return to the graveyard or die, set the level index to equal 0
    public void EscapeLevel()
    {
        _params.levelIndex = 0;
        SceneManager.LoadScene("graveyard");
    }

    public void StartLevel()
    {
        _params.levelIndex = 0;
        SceneManager.LoadScene("game");
    }

    public void GoToNextLevel()
    {
        if (_params.levelIndex < _params.levels.Count - 1)
        {
            // when exiting a level, update the index + 1 if we are not at the end of all levels
            _params.levelIndex += 1;
            Level currentLevel = _params.levels[_params.levelIndex];

            // invoke the event of a level change so that all rooms in a level will respond
            if (OnLevelChange != null)
            {
                OnLevelChange();
            }
        }
        else 
        {
            Debug.Log("Reached end level");
        }
    }

    public Level GetCurrentLevel()
    {
        return _params.levels[_params.levelIndex];
    }
}
