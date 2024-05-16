using UFramework.GameEvents;
using UnityEngine;

namespace NativeClasses
{
    public class GameStateAccessor
    {
        #region SingletonPattern
        private static readonly GameStateAccessor Instance = new GameStateAccessor();

        public static GameStateAccessor GetInstance()
        {
            return Instance;
        }
        #endregion

        #region Private Access Assets
        private readonly GameStateScriptableObject _gameStateScriptableObject;
        private readonly GameEvent _gameEventAITurnStart;
        private readonly GameEvent _gameEventPlayerTurnStart;
        private readonly GameEvent _updateUIEvent;
        private readonly PartyElementsScriptableObject _playerParty;
        private readonly PartyElementsScriptableObject _aiParty;
        #endregion
    
        private GameStateAccessor()
        { 
            _gameStateScriptableObject =
                ResourceLoader.Load<GameStateScriptableObject>("Scriptable Objects/Game Systems/GameState");
            _gameEventAITurnStart = ResourceLoader.Load<GameEvent>("Game Events/Event_AIStartTurn");
            _gameEventPlayerTurnStart = ResourceLoader.Load<GameEvent>("Game Events/Event_PlayerStartsTurn");
            _updateUIEvent = ResourceLoader.Load<GameEvent>("Game Events/Event_UpdateUI");
            _aiParty =
                ResourceLoader.Load<PartyElementsScriptableObject>(
                    "Scriptable Objects/Party Scriptable Objects/AIPartyScriptableObject");
            _playerParty =
                ResourceLoader.Load<PartyElementsScriptableObject>(
                    "Scriptable Objects/Party Scriptable Objects/PlayerPartyScriptableObject");
        }
    
        #region Data Queries on Game State

        public TurnPlayerState GetCurrentTurnPlayerState()
        {
            return _gameStateScriptableObject.GetCurrentTurnPlayerState();
        }

        public int GetTurnCount()
        {
            return _gameStateScriptableObject.GetCurrentTurn();
        }

        public float GetPlayerCountdown()
        {
            return _gameStateScriptableObject.GetPlayerCountdown();
        }

        public PartyElementsScriptableObject GetPlayerParty()
        {
            return _gameStateScriptableObject.GetPlayerParty();
        }

        public PartyElementsScriptableObject GetAIParty()
        {
            return _gameStateScriptableObject.GetAIParty();
        }
        
        #endregion
    
        #region Game State manipulation

        /**
         * //<Description>
         *  This method is responsible for ending the current turn for the player and starting the next AI turn
         * //</Description>
         */
        public void EndPlayerTurn()
        {
            
            if (!_gameStateScriptableObject.Started())
            {
                Debug.Log("Cannot end a turn without starting game first using GameStartEvent");
                return;
            }
            //Check if the current turn owner is the player or not
            if (_gameStateScriptableObject.GetCurrentTurnPlayerState() == TurnPlayerState.Player)
            {
                _gameStateScriptableObject.SetCurrentTurnPlayerState(TurnPlayerState.AI);
                _gameStateScriptableObject.ResetPlayerTimer();
                _gameEventAITurnStart.Raise();
            }
        }

        /**
         * //<Description>
         *  This method is responsible for ending the current turn for the AI and starting the next Player turn
         *  Given the AI always acts out last in each turn, we can increase the turn counter
         * //</Description>
         */
        public void EndAITurn()
        {
            if (!_gameStateScriptableObject.Started())
            {
                Debug.Log("Cannot end a turn witout satrting game first using GameStartEvent");
                return;
            }
            if (_gameStateScriptableObject.GetCurrentTurnPlayerState() == TurnPlayerState.AI)
            {
                _gameStateScriptableObject.SetCurrentTurnPlayerState(TurnPlayerState.Player);
                _gameStateScriptableObject.ResetPlayerTimer();
                _gameStateScriptableObject.TurnFinished();
                _gameEventPlayerTurnStart.Raise();
            }
        }

        /**
         * //<Author>
         *  João Gouveia
         * //</Author>
         * //<Version>
         *  0.1.0.19.4.24
         * //</Version>
         * //<Description>
         *  Resets the game
         * //</Description>
         */
        public void ResetGame()
        {
            ClearParties();
            ResetGameState(false);
        }
        
        public void InitGame()
        {
            if (_gameStateScriptableObject.Started())
            {
                Debug.Log("InitGame called after game was Started");
                return;
            }

            if (!_gameStateScriptableObject.PartiesInitialized())
            {
                Debug.Log("Cannot init a game without parties");
                return;
            }
            ResetGameState(true);
        }

        public void PlayerWon()
        {
            if (!_gameStateScriptableObject.Started())
            {
                Debug.Log("PlayerWon called before game was Started");
                return;
            }

            if (_gameStateScriptableObject.Finished())
            {
                Debug.Log("PlayerWon was called on a finished Game");
                return;
            }
            FinishGameWithWinner(TurnPlayerState.Player);
        }


        public void AIWon()
        {
            if (!_gameStateScriptableObject.Started())
            {
                Debug.Log("AIWon called before game was Started");
                return;
            }

            if (_gameStateScriptableObject.Finished())
            {
                Debug.Log("AIWon was called on a finished Game");
                return;
            }
            FinishGameWithWinner(TurnPlayerState.AI);
        }
        
        public void PlayerSelectCharacter()
        {
            //DONE: Find Selected Character, this should be done on the ui, where the player 
            //DONE: will select character, and such selected character, will have a tag PlayerSelectedChar
            if (!_gameStateScriptableObject.Started())
            {
                Debug.Log("Cannot move Select character before satrting game");
                return;
            }
            if (_gameStateScriptableObject.Finished())
            {
                Debug.Log("Cannot move Select character on a finished Game");
                return;
            }
            
            GameObject checkForNull = GameObject.FindWithTag("PlayerSelectedChar");
            if (checkForNull == null)
            {
                Debug.Log("Couldn't find a selected Player character");
                return;
            }
            _gameStateScriptableObject.SetCurrentPlayerPartyMember(GameObject.FindWithTag("PlayerSelectedChar"));
        }
    
        public void AiSelectCharacter()
        {
            //DONE: Find Selected Character, this should be done on the ui, where the player 
            //DONE: will select character, and such selected character, will have a tag AISelectedChar
            if (!_gameStateScriptableObject.Started())
            {
                Debug.Log("Cannot Select character before starting game");
                return;
            }
            if (_gameStateScriptableObject.Finished())
            {
                Debug.Log("Cannot Select character on a finished Game");
                return;
            }

            GameObject checkForNull = GameObject.FindWithTag("AISelectedChar");
            if (checkForNull == null)
            {
                Debug.Log("Couldn't find a selected AI character");
                return;
            }
            _gameStateScriptableObject.SetCurrentPlayerPartyMember(GameObject.FindWithTag("AISelectedChar"));
        }

        public void InitTeams()
        {
            if (_gameStateScriptableObject.Started())
            {
                Debug.Log("Cannot init parties during started match");
                return;
            }
            //DONE: Find Selected Characters and add them to each party, this should be done on the ui, where the player 
            //DONE: will select characters, and such selected characters, will have a tag PlayerPartySelectionSelected
            //DONE: and AI(will be auto defined) will have a tag AIPartySelectionSelected
            GameObject[] selectedPlayerCharacters = GameObject.FindGameObjectsWithTag("PlayerPartySelectionSelected");
            GameObject[] selectedAICharacters = GameObject.FindGameObjectsWithTag("AIPartySelectionSelected");
            if (selectedPlayerCharacters.Length > 4 || selectedAICharacters.Length > 4)
            {
                Debug.Log("Cannot have teams with more than 4 characters");
                return;
            }
            foreach (GameObject partyMember in selectedPlayerCharacters)
            {
                _playerParty.AddCharacterToParty(partyMember);
                partyMember.tag = "";
            }
            foreach (GameObject partyMember in selectedAICharacters)
            {
                _aiParty.AddCharacterToParty(partyMember);
                partyMember.tag = "";
            }
            _gameStateScriptableObject.SetPlayerParty(_playerParty);
            _gameStateScriptableObject.SetAIParty(_aiParty);
        }
        #endregion


        #region Private inner methodos

        private void FinishGameWithWinner(TurnPlayerState winner)
        {
            _gameStateScriptableObject.SetCurrentTurnPlayerState(TurnPlayerState.None);
            _gameStateScriptableObject.ResetPlayerTimer();
            _gameStateScriptableObject.SetFinished(true);
            _gameStateScriptableObject.SetWinner(winner);
            _updateUIEvent.Raise();
        }

        private void ResetGameState(bool shouldStart)
        {
            _gameStateScriptableObject.SetCurrentTurnPlayerState(TurnPlayerState.Player);
            _gameStateScriptableObject.ResetPlayerTimer();
            _gameStateScriptableObject.ResetTurnCounter();
            _gameStateScriptableObject.SetStarted(shouldStart);
            _gameStateScriptableObject.SetFinished(false);
            _gameStateScriptableObject.SetWinner(TurnPlayerState.None);
            _gameEventPlayerTurnStart.Raise();
        }

        private void ClearParties()
        {
            _gameStateScriptableObject.ClearParties();
        }
        #endregion

    }
}
