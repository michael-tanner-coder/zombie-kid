using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState 
{
    EXPLORATION,
    COMBAT,
    IN_MENU,
    PAUSED,
}

public class GameStateManager : IService
{
    private GameState _state;
    public GameState State => _state;

    public delegate void ChangeState(GameState newState);
    public static event ChangeState OnStateChanged;

    public GameStateManager() {}

    ~GameStateManager() {}

    public void SetState(GameState newState)
    {
        _state = newState;
        OnStateChanged(newState);
    }
}
