using NativeClasses;
using UnityEngine;


public class GameStateControllerComponent : MonoBehaviour
{
    private GameStateService _gameStateService;
    private void Awake()
    {
        _gameStateService = GameStateService.GetInstance();
    }

    #region Turn Events
        public void OnPlayerEndTurnEvent()
        {
            _gameStateService.EndPlayerTurn();
        }
        
        public void OnAIEndEvent()
        {
            _gameStateService.EndAITurn();
        }
    #endregion
    

    public void OnPlayerSelected()
    {
        _gameStateService.PlayerSelectCharacter();
    }
    
    public void OnAISelected()
    {
        _gameStateService.AiSelectCharacter();
    }
    
    public void OnPartyInitEvent()
    {
        _gameStateService.InitTeams();
    }
    

    public void OnPlayerWonEvent()
    {
        _gameStateService.PlayerWon();
    }

    public void OnAIWon()
    {
        _gameStateService.AIWon();
    }
    
    public void OnGameStartEvent()
    {
        _gameStateService.InitGame();
    }


    public void OnGameRestartEvent()
    {
        _gameStateService.ResetGame();
    }
}
