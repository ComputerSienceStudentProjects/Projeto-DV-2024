using System;
using UFramework.GameEvents;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [Header("Event definition")] 
    [SerializeField] private GameEvent playerTurnEndEvent;
    [SerializeField] private GameEvent aITurnEndEvent;
    [SerializeField] private GameEvent gameStartedEvent;
    [SerializeField] private GameEvent gameResetEvent;
    [SerializeField] private GameEvent playerWonEvent;
    [SerializeField] private GameEvent aIWonEvent;
    [SerializeField] private GameEvent partyInitEvent;
    [SerializeField] private GameEvent playerSelectCharacterEvent;
    [SerializeField] private GameEvent aISelectCharacterEvent;
    
#if UNITY_EDITOR
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 25), "End Player Turn"))
        {
            playerTurnEndEvent.Raise();
        }
        
        if (GUI.Button(new Rect(10, 40, 150, 25), "End AI Turn"))
        {
            aITurnEndEvent.Raise();
        }
        
        if (GUI.Button(new Rect(10, 70, 150, 25), "Start Game"))
        {
            gameStartedEvent.Raise();
        }
        
        if (GUI.Button(new Rect(10, 100, 150, 25), "Reset Game"))
        {
            gameResetEvent.Raise();
        }

        if (GUI.Button(new Rect(10, 130, 150, 25), "Test player Won Event"))
        {
            playerWonEvent.Raise();
        }
        
        if (GUI.Button(new Rect(10, 160, 150, 25), "Test AI Won Event"))
        {
            aIWonEvent.Raise();
        }

        if (GUI.Button(new Rect(10, 190, 150, 25), "Init Parties"))
        {
            partyInitEvent.Raise();
        }
        
        if (GUI.Button(new Rect(170, 10, 150, 25), "Player Select Char"))
        {
            playerSelectCharacterEvent.Raise();
        }
        
        if (GUI.Button(new Rect(170, 40, 150, 25), "Player Select Char"))
        {
            aISelectCharacterEvent.Raise();
        }
    }
    #endif
}
