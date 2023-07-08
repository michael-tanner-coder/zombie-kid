using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSaveFileHandler : MonoBehaviour
{
    public void StartNewGame()
    {
        ServiceLocator.Instance.Get<SaveDataManager>().ResetPlayerPrefs();
        SceneManager.LoadScene("game");
    }
}
