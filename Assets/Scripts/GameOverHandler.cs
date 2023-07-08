using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ScriptableObjectArchitecture;

public class GameOverHandler : MonoBehaviour
{   

    [SerializeField] private float _slowDownRate = 0.01f;
    [SerializeField] private float _gameOverTimer;
    [SerializeField] private IntVariable _gold;
    private bool _sequenceStarted = false;

    public void StartGameOverSequence()
    {
        _sequenceStarted = true;
        ServiceLocator.Instance.Get<AudioManager>().PlaySoundFromDictionary("GameOver");
    }
    
    void Update()
    {
        if (_sequenceStarted)
        {
            // lerp slow-mo
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0f, _slowDownRate);
        }

        // scene change on countdown finish
        if (Time.timeScale <= 0.02f && _sequenceStarted)
        {
            _gameOverTimer -= Time.unscaledDeltaTime;
            if (_gameOverTimer <= 0f)
            {
                Time.timeScale = 1f;
                _gold.Value = 0;
                SceneManager.LoadScene("gameover");
                _sequenceStarted = false;
            }
        }
    }
}
