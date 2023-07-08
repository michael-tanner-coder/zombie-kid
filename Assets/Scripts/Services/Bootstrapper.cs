using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public static class Bootstrapper
{
    /// <summary>
    /// This function is used to initialize a variety of services and setup the initial state of the game.
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        ServiceLocator.Initialize();
        
        //Setup services that must be attached to a GameObject
        GameObject singletonObject = new GameObject("singletonObject",
            typeof(MonoBehaviorService));
        ServiceLocator.Instance.Register(singletonObject.GetComponent<MonoBehaviorService>());
        Object.DontDestroyOnLoad(singletonObject);
        
        //Setup Services
        ServiceLocator.Instance.Register(new SaveDataManager());
        ServiceLocator.Instance.Register(new GameStateManager());
        ServiceLocator.Instance.Register(new InputManager());
        ServiceLocator.Instance.Register(new AudioManager());
        ServiceLocator.Instance.Register(new MusicManager());
        ServiceLocator.Instance.Register(new LevelManager());
    }
}


