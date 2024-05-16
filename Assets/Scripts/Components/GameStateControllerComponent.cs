using NativeClasses;
using UnityEngine;


public class GameStateControllerComponent : MonoBehaviour
{
    private GameStateAccessor _gameStateAccessor;
    private void Awake()
    {
        _gameStateAccessor = GameStateAccessor.GetInstance();
    }

    #region Turn Events
        public void OnPlayerEndTurnEvent()
        {
            _gameStateAccessor.EndPlayerTurn();
        }
        
        public void OnAIEndEvent()
        {
            _gameStateAccessor.EndAITurn();
        }
    #endregion
    

    public void OnPlayerSelected()
    {
        _gameStateAccessor.PlayerSelectCharacter();
    }
    
    public void OnAISelected()
    {
        _gameStateAccessor.AiSelectCharacter();
    }
    
    public void OnPartyInitEvent()
    {
        _gameStateAccessor.InitTeams();
    }
    

    public void OnPlayerWonEvent()
    {
        _gameStateAccessor.PlayerWon();
    }

    public void OnAIWon()
    {
        _gameStateAccessor.AIWon();
    }
    
    public void OnGameStartEvent()
    {
        _gameStateAccessor.InitGame();
    }


    public void OnGameRestartEvent()
    {
        _gameStateAccessor.ResetGame();
    }
}
