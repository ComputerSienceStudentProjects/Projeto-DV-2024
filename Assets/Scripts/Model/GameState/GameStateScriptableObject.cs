using UnityEngine;

public enum TurnPlayerState
{
    Player,
    AI,
    None
}


[CreateAssetMenu(menuName = "Scriptables/Game State Object")]
public class GameStateScriptableObject : ScriptableObject
{
    [Header("Game State Properties")] 
    [SerializeField] private int turnCount = 1;
    [SerializeField] private TurnPlayerState currentPlayerStatus = TurnPlayerState.Player;
    [SerializeField] private float currentPlayerTime = 60f;
    [SerializeField] private PartyElementsScriptableObject playerPartyObject;
    [SerializeField] private PartyElementsScriptableObject aiPartyObject;
    [SerializeField] private bool gameStarted = false;
    [SerializeField] private bool finished = false;
    [SerializeField] private TurnPlayerState whoWon = TurnPlayerState.None;

    public TurnPlayerState GetCurrentTurnPlayerState()
    {
        return currentPlayerStatus;
    }

    public float GetPlayerCountdown()
    {
        return currentPlayerTime;
    }

    public int GetCurrentTurn()
    {
        return turnCount;
    }

    public PartyElementsScriptableObject GetPlayerParty()
    {
        return playerPartyObject;
    }

    public PartyElementsScriptableObject GetAIParty()
    {
        return aiPartyObject;
    }

    public void SetCurrentTurnPlayerState(TurnPlayerState turnPlayerState)
    {
        this.currentPlayerStatus = turnPlayerState;
    }

    public void ResetPlayerTimer()
    {
        currentPlayerTime = 60f;
    }

    public void ResetTurnCounter()
    {
        turnCount = 1;
    }

    public void TurnFinished()
    {
        turnCount++;
    }

    public bool Started()
    {
        return gameStarted;
    }

    public void SetStarted(bool started)
    {
        gameStarted = started;
    }

    public void SetFinished(bool finished)
    {
        this.finished = finished;
    }

    public bool Finished()
    {
        return this.finished;
    }

    public void SetWinner(TurnPlayerState whoWon)
    {
        this.whoWon = whoWon;
    }

    public TurnPlayerState Winner()
    {
        return whoWon;
    }

    public void SetPlayerParty(PartyElementsScriptableObject playerParty)
    {
        playerPartyObject = playerParty;
    }

    public void SetAIParty(PartyElementsScriptableObject aiParty)
    {
        aiPartyObject = aiParty;
    }

    public void ClearParties()
    {
        playerPartyObject = null;
        aiPartyObject = null;
    }

    public void SetCurrentPlayerPartyMember(GameObject partyMember)
    {
        playerPartyObject.SelectPartyMember(partyMember);
    }
    
    public void SetCurrentAIPartyMember(GameObject partyMember)
    {
        aiPartyObject.SelectPartyMember(partyMember);
    }

    public bool PartiesInitialized()
    {
        return (aiPartyObject != null) && (playerPartyObject != null);
    }
}
